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
    float offset = 0f;
    private float wanderSpeed = 5;
    bool isRight;
    public bool hasTarget;
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
        Debug.Log(hasTarget);
        if(hasTarget){
            rotateRelativeToPlayer();
            if(enemy.transform.position.x - position.x > offset){
                Debug.Log("wander");
                Wander();
            }
        }
        if(!hasTarget){
            Wander();
        }
        
        
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if((other.tag == "player" ||  other.tag == "attackArea") && other.GetComponent<movement>().makeSound){
            position = other.transform.position;
            hasTarget = true;
            Invoke("loseTarget",0.8f);
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

        Collider2D isFrontGround = Physics2D.Raycast(frontGroundDetection.position, Vector2.down, groundDetectDistance).collider,
                   isBackGround = Physics2D.Raycast(backGroundDetection.position, Vector2.down, groundDetectDistance).collider;


        if (isFrontGround == null)
        {
            Flip();
        } 
    }

    private void Flip()
    {
        if (isRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

}
