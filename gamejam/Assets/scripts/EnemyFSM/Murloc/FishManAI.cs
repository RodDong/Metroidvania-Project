using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManAI : MonoBehaviour
{
    private FishState currentState;
    [SerializeField] public Animator animator;
    private float meleeAttackRange = 3f, meleeEngageRange = 7f;
    private float rangedAttackRange = 100f;
    private float wanderSpeed = 5f, flipCD = 1f;
    private float groundDetectDistance = 2f;
    private bool isProtected = false;
    [SerializeField] public float roamDistance;
    private Vector2 initialPos;

    [SerializeField] GameObject player;

    //ground detection vars
    [SerializeField] GameObject VisionDetectArea;
    [SerializeField] GameObject SoundDetectArea;
    [SerializeField] Transform frontGroundDetection;
    [SerializeField] Transform backGroundDetection;
    [SerializeField] Collider2D daggerCollider;
    [SerializeField] public ObjectPool harpoonPool;

    bool isRight;
    public float timer = 0.0f;
    
    private fishmanDetection fishmanDetection;
    GameObject harpoonObj;
    void Start()
    {
        currentState = new Idle();
        // animator = gameObject.GetComponent<Animator>();
        fishmanDetection = SoundDetectArea.GetComponent<fishmanDetection>();
        initialPos = gameObject.transform.position;
    }

    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            return;
        }

        isRight = fishmanDetection.position.x - gameObject.transform.position.x > 0;
        // roamDistance can be used to limit how far the fish can walk from its starting pos.
        if (fishmanDetection.hasTarget) {
            if (Mathf.Abs(fishmanDetection.position.x - gameObject.transform.position.x) < meleeAttackRange) {
                currentState = new SwordAttack();
            } else if (Mathf.Abs(fishmanDetection.position.x - gameObject.transform.position.x) < meleeEngageRange) {
                currentState = new WalkingTowardsTarget();
            } else if (Vector2.Distance(fishmanDetection.position, gameObject.transform.position) < rangedAttackRange && !isProtected) {
                currentState = new HarpoonAttack();
            } else {
                currentState = new WalkingTowardsTarget();
            }
        } else {
            currentState = new Walk();
        }

        currentState.Execute(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Block")){
            isProtected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Block")){
            isProtected = false;
        }
    }


    public void Wander()
    {
        if (gameObject.GetComponent<EnemyDamage>().getHP() > 0) {
            int groundMask = 1 << LayerMask.NameToLayer("Ground");
            int platformMask = 1 << LayerMask.NameToLayer("Platform");
            int noCollisionPlatformMask = 1 << LayerMask.NameToLayer("PlatformWithoutPlayerCollision");
            
            gameObject.transform.Translate(Vector2.left * wanderSpeed * Time.deltaTime);
            Collider2D isFrontGround = Physics2D.Raycast(frontGroundDetection.position, Vector2.down, groundDetectDistance, groundMask | platformMask | noCollisionPlatformMask).collider,
                    isBackGround = Physics2D.Raycast(backGroundDetection.position, Vector2.down, groundDetectDistance, groundMask | platformMask | noCollisionPlatformMask).collider;
            if (isFrontGround == null)
            {
                Flip();
            } 
            if ( Mathf.Abs(gameObject.transform.position.x - initialPos.x) > roamDistance) {
                Flip();
            }
        }
    }

    private void Flip()
    {
        if(timer <= 0){
            bool isFacingRight = Mathf.Abs(transform.eulerAngles.y) < 90;
            timer = flipCD;
            if (isFacingRight)
            {
                gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            gameObject.transform.position -= (transform.right);
        }        
    }

    public void rotateRelativeToPlayer()
    {
        if (isRight)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void instantiateHarpoon()
    {
        var xDistance = fishmanDetection.position.x - gameObject.transform.position.x;
        var yDistance = fishmanDetection.position.y - gameObject.transform.position.y;
        float arrow_rotation = Mathf.Atan(yDistance / xDistance) * Mathf.Rad2Deg;
        if (isRight)
        {
            arrow_rotation += 180f;
        }
        Quaternion arrow_quaternion = new Quaternion();
        arrow_quaternion.eulerAngles = new Vector3(0, 0, arrow_rotation);
        harpoonObj = harpoonPool.Spawn(gameObject.transform.position, arrow_quaternion);
        harpoonObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(xDistance, yDistance, 0) * 600 / (Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance)));
    }

}
public abstract class FishState{
    public abstract void Execute(FishManAI fish);
}

public class Idle : FishState{
    public override void Execute(FishManAI fish)
    {
        fish.animator.Play("Idle");
    }
}

public class SwordAttack : FishState{
    public override void Execute(FishManAI fish)
    {
        fish.rotateRelativeToPlayer();
        fish.animator.Play("SwordAttack");
        fish.timer += 120f/64f;
        
    }

}

public class Walk : FishState{
    public override void Execute(FishManAI fish)
    {
        fish.animator.Play("Walk");
        fish.Wander();
    }
}

public class WalkingTowardsTarget : FishState {
    public override void Execute(FishManAI fish)
    {
        fish.animator.Play("Walk");

        fish.rotateRelativeToPlayer();
        fish.Wander();
    }
}

public class HarpoonAttack : FishState{
    public override void Execute(FishManAI fish)
    {
        fish.rotateRelativeToPlayer();
        fish.animator.Play("Throw");
        fish.Invoke("instantiateHarpoon", 1.2f);
        fish.timer += 150f/64f;
    }
}
