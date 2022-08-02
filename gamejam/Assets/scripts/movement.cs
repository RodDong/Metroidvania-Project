using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    //game Objects
    Rigidbody2D rb;
    public Vector3 position;
    [SerializeField] GameObject attackArea;
    Collider2D attackCollider;

    public bool isRight;
    public bool isPushed;


    
    //Jump vars
    public bool canJump = false;
    float jump_init_v = 20f;
    
    //Game initialization
    void Start() {

        //initialize AttackArea collider
        attackCollider = attackArea.GetComponent<Collider2D>();
        attackCollider.enabled = false;

        //initialize player rigidbody 
        rb = GetComponent<Rigidbody2D>();
        isRight = true;
        isPushed = false;

    }

    //Update frames in game
    void Update() {
        position = rb.transform.position;
        rb.freezeRotation = true;
        processInput();
        Debug.Log(isPushed);

    }

    //Collision handler for bool canJump
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground") {
            canJump = true;
        }
    }

    


    //process movements via inputs 
    void processInput() {
        //Process player movements (Space for jump, A & D for horizontal movements)
        Vector2 v = rb.velocity;
        if(Input.GetKeyDown(KeyCode.Space) && canJump) {
            v.y = jump_init_v;
            canJump = false;
        }
        v.x = Input.GetAxisRaw("Horizontal") * 10f;
        rb.velocity = v;

        //Player rotation based on input 
        if(Input.GetKeyDown(KeyCode.A)){
            if(isRight){
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            isRight = false;
        }

        if(Input.GetKeyDown(KeyCode.D)){
            if(!isRight){
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            
            isRight = true;
        }

        //Process left mouse click for player attack
        if(Input.GetMouseButton(0)){
            attackCollider.enabled = true;
            isPushed = true;
            Invoke("disableAttackCollider", 0.1f);
        }
    }

    void disableAttackCollider(){
        attackCollider.enabled = false;
    }
}
