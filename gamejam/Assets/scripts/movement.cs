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

    }

    //Update frames in game
    void Update() {
        position = rb.transform.position;
        rb.freezeRotation = true;
        processInput();
        updateSwordPos();


    }

    //Collision handler for bool canJump
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground") {
            canJump = true;
        }
    }

    

    //update swords' position relative to the player
    void updateSwordPos(){
        /*foreach(GameObject sword in swords){
            Vector2 swordPos = sword.transform.position;
            swordPos.x = rb.transform.position.x + 2;
            swordPos.y = rb.transform.position.y + 2;
            sword.transform.position = swordPos;
        }*/

        
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

        //Process left mouse click for player attack
        if(Input.GetMouseButton(0)){
            attackCollider.enabled = true;
            Invoke("disableAttackCollider", 10);
        }
    }

    void disableAttackCollider(){
        attackCollider.enabled = false;
    }
}
