using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] private float groundDetectDistance;
    [SerializeField] private Transform frontGroundDetection;

    [SerializeField] private Transform backGroundDetection;
    public Vector3 position;
    float offset = 3f;
    private float wanderSpeed = 5f;
    bool isRight;
    public bool hasTarget;
    public bool isFacingRight => Mathf.Abs(transform.eulerAngles.y) < 90;
    void Start()
    {
        isRight = true;
        hasTarget = false;
    }

    void Update()
    {
        if (position.x - enemy.transform.position.x > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        if(hasTarget){
            
            if(Mathf.Abs(enemy.transform.position.x - position.x) >= offset){
                rotateRelativeToPlayer();
                Wander();
            }
        }
        if(!hasTarget && !enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("melee_attack")){
            Wander();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if((other.tag == "player" ||  other.tag == "attackArea") && other.GetComponent<movement>().makeSound){
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

    void loseTarget(){
        hasTarget = false;
    }

    public void Wander()
    {
        enemy.transform.Translate(Vector2.left * wanderSpeed * Time.deltaTime);
        int groundMask = 1 << 8;
        Collider2D isFrontGround = Physics2D.Raycast(frontGroundDetection.position, Vector2.down, groundDetectDistance, groundMask).collider,
                   isBackGround = Physics2D.Raycast(backGroundDetection.position, Vector2.down, groundDetectDistance, groundMask).collider;
        if (isFrontGround == null)
        {
            Flip();
        }
    }

    private void Flip()
    {
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
