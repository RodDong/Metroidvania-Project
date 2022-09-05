using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishmanDetection : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] private float groundDetectDistance;
    [SerializeField] private Transform frontGroundDetection;

    [SerializeField] private Transform backGroundDetection;
    [SerializeField] private GameObject wall;
    [SerializeField] private int leftWall;
    [SerializeField] private int rightWall;
    public bool hasTarget;
    public bool isFacingRight => Mathf.Abs(transform.eulerAngles.y) < 90;
    public Vector3 position;
    private float wanderSpeed = 5f, flipCD = 1f;
    private WallList wallList;
    //float offset = 2f;
    bool isRight;
    float timer = 0.0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        hasTarget = false;
//        wallList = wall.GetComponent<WallList>();
    }

    void Update()
    {
        
        if (timer >= 0) {
            timer -= Time.deltaTime;
        }
        if (hasTarget) {
            player.GetComponent<PlayerStatus>().isDetected = true;
        }
        if (position.x - enemy.transform.position.x > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }

        // if (hasTarget) {
        //     if(Mathf.Abs(enemy.transform.position.x - position.x) >= offset){
        //         rotateRelativeToPlayer();
        //         Wander();
        //     }
        // }
        // else if (!hasTarget && !enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("melee_attack")) {
        //     Wander();
        // }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if((other.tag == "player" ||  other.tag == "attackArea") && other.GetComponent<movement>().makeSound) {
            position = other.transform.position;
            hasTarget = true;
            Invoke("loseTarget",3f);
        }
    }

    void rotateRelativeToPlayer()
    {
        if (isRight)
        {
            enemy.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            enemy.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void loseTarget()
    {
        hasTarget = false;
    }

    public void Wander()
    {
        if (enemy.GetComponent<EnemyDamage>().getHP() > 0) {
            
            int groundMask = 1 << 8;
            int platformMask = 1 << 10;
            int noCollisionPlatformMask = 1 << 14;
            
            enemy.transform.Translate(Vector2.left * wanderSpeed * Time.deltaTime);
            Collider2D isFrontGround = Physics2D.Raycast(frontGroundDetection.position, Vector2.down, groundDetectDistance, groundMask | platformMask | noCollisionPlatformMask).collider,
                    isBackGround = Physics2D.Raycast(backGroundDetection.position, Vector2.down, groundDetectDistance, groundMask | platformMask | noCollisionPlatformMask).collider;
            if (isFrontGround == null)
            {
                Flip();
            }
            if (enemy.transform.position.x < wallList.wallPosLists[leftWall]) {
                Flip();
            }
            if (enemy.transform.position.x > wallList.wallPosLists[rightWall]) {
                Flip();
            }
            
        }
    }

    private void Flip()
    {
        if(timer <= 0){
            timer = flipCD;
            if (isFacingRight)
            {
                enemy.transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                enemy.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }        
    }
}
