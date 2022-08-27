using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManAI : MonoBehaviour
{
    private FishState currentState;
    [SerializeField]public Animator animator;
    private float meleeAttackRange = 5f, meleeEngageRange = 20f;
    private float rangedAttackRange = 100f;
    private float wanderSpeed = 5f, flipCD = 1f;
    private float groundDetectDistance = 2f;

    [SerializeField] GameObject player;

    //ground detection vars
    [SerializeField] GameObject spearCollider;
    [SerializeField] GameObject VisionDetectArea;
    [SerializeField] GameObject SoundDetectArea;
    [SerializeField] Transform frontGroundDetection;
    [SerializeField] Transform backGroundDetection;

    bool isRight;
    public float timer = 0.0f;
    
    private fishmanDetection fishmanDetection;
    GameObject harpoonObj;
    void Start()
    {
        currentState = new Idle();
        // animator = gameObject.GetComponent<Animator>();
        fishmanDetection = SoundDetectArea.GetComponent<fishmanDetection>();
    }

    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            return;
        }

        isRight = fishmanDetection.position.x - gameObject.transform.position.x > 0;

        if (fishmanDetection.hasTarget) {
            if (Vector2.Distance(fishmanDetection.position, gameObject.transform.position) < meleeAttackRange) {
                currentState = new SwordAttack();
            } else if (Vector2.Distance(fishmanDetection.position, gameObject.transform.position) < meleeEngageRange) {
                currentState = new WalkingTowardsTarget();
            } else if (Vector2.Distance(fishmanDetection.position, gameObject.transform.position) < rangedAttackRange) {
                currentState = new HarpoonAttack();
            } else {
                currentState = new WalkingTowardsTarget();
            }
        } else {
            currentState = new Walk();
        }

        currentState.Execute(this);
    }

    public void Wander()
    {
        if (gameObject.GetComponent<EnemyDamage>().getHP() > 0) {
            
            int groundMask = 1 << 8;
            int platformMask = 1 << 10;
            int noCollisionPlatformMask = 1 << 14;
            
            gameObject.transform.Translate(Vector2.left * wanderSpeed * Time.deltaTime);
            Collider2D isFrontGround = Physics2D.Raycast(frontGroundDetection.position, Vector2.down, groundDetectDistance, groundMask | platformMask | noCollisionPlatformMask).collider,
                    isBackGround = Physics2D.Raycast(backGroundDetection.position, Vector2.down, groundDetectDistance, groundMask | platformMask | noCollisionPlatformMask).collider;
            if (isFrontGround == null)
            {
                Flip();
            }
            // if (gameObject.transform.position.x < wallList.wallPosLists[leftWall]) {
            //     Flip();
            // }
            // if (gameObject.transform.position.x > wallList.wallPosLists[rightWall]) {
            //     Flip();
            // }
            
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
    void instantiateHarpoon()
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
        harpoonObj = ObjectPool.Instance.Spawn(gameObject.transform.position, arrow_quaternion);
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
    public override async void Execute(FishManAI fish)
    {
        fish.rotateRelativeToPlayer();
        fish.animator.Play("SwordAttack");
        fish.timer += 120f/64f;

        
        
        
        Debug.Log("Sword Attack");
        
    }

}

public class Walk : FishState{
    public override void Execute(FishManAI fish)
    {
        fish.animator.Play("Walk");
        fish.Wander();

        Debug.Log("Just walking");
    }
}

public class WalkingTowardsTarget : FishState {
    public override void Execute(FishManAI fish)
    {
        fish.animator.Play("Walk");

        fish.rotateRelativeToPlayer();
        fish.Wander();
        Debug.Log("walking towards target");
    }
}

public class HarpoonAttack : FishState{
    public override void Execute(FishManAI fish)
    {
        fish.rotateRelativeToPlayer();
        fish.animator.Play("Throw");

        fish.timer += 150f/64f;

        Debug.Log("Harpoon Attack");
    }
}
