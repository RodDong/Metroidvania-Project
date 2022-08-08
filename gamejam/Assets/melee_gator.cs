using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_gator : MonoBehaviour
{
    private bool isRight;
    private float wanderSpeed = 5;
    private float attackRange = 4;

    GameObject player;
    private Animator animator;

    //ground detection vars
    [SerializeField] private float groundDetectDistance;
    [SerializeField] private Transform frontGroundDetection;

    [SerializeField] private Transform backGroundDetection;
    [SerializeField] GameObject spearCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        animator = gameObject.GetComponent<Animator>();
        isRight = false;
        //Time.timeScale = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //update isRight relative to player position 
        if (player.transform.position.x - gameObject.transform.position.x > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }

        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if(distance < attackRange){
            attack();
        }else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("melee_attack")){
            Wander();
        }

        rotateRelativeToPlayer();
    }

    void attack(){
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("melee_attack")){
            Invoke("fireSpearCollider", 0.46f);
        }
        animator.SetTrigger("attack");
    }

    void fireSpearCollider(){
        spearCollider.GetComponent<BoxCollider2D>().enabled = true;
        if(!isRight){
            spearCollider.GetComponent<Rigidbody2D>().AddForce(Vector3.left*5f, ForceMode2D.Impulse);
        }else{
            spearCollider.GetComponent<Rigidbody2D>().AddForce(Vector3.right*5f, ForceMode2D.Impulse);
        }
        Invoke("stopSpearCollider", 0.3f);
        Invoke("resetSpearCollider", 0.54f);
    }

    void resetSpearCollider(){
        spearCollider.GetComponent<BoxCollider2D>().enabled = false;
        spearCollider.GetComponent<BoxCollider2D>().transform.localPosition = 
        new Vector3(-3, spearCollider.GetComponent<BoxCollider2D>().transform.localPosition.y,0);
    }

    void stopSpearCollider(){
        spearCollider.GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
    }

    void rotateRelativeToPlayer()
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

    public void Wander()
    {
        transform.Translate(Vector2.left * wanderSpeed * Time.deltaTime);

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
