using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    //game Objects
    [SerializeField] GameObject attackArea;

    public Vector3 position;
    public bool isRight;
    //Jump vars
    public bool canJump = false;
    public int playerDamage;
    private float nextAttck;
    float jump_init_v = 20f;
    //attack vars
    public bool attacking = false;
    Collider2D attackCollider;
    Rigidbody2D rb;
    Animator attackAnimation;
    

    //Game initialization
    void Start() {

        //initialize AttackArea collider
        attackCollider = attackArea.GetComponent<Collider2D>();
        attackCollider.enabled = false;
        

        //initialize player rigidbody 
        rb = GetComponent<Rigidbody2D>();
        isRight = true;

        // Initialize player attack animation
        attackAnimation = GameObject.FindGameObjectWithTag("attackAnimator").GetComponent<Animator>();
        playerDamage = 10;
    }

    //Update frames in game
    void Update() {
        attacking = false;
        position = rb.transform.position;
        rb.freezeRotation = true;
        processInput();
        processAttack();
    }

    //Collision handler for bool canJump
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground" || other.gameObject.tag == "OneWayPlatform") {
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
    }

    //Process left mouse click for player attack
    void processAttack() {
        if(Input.GetMouseButtonDown(0) && !statusVar.isCoolDown){
            attacking = true;
            // nextAttck = Time.time + attackRate;
            attackCollider.enabled = true;
            attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            Invoke("disableAttackCollider", 0.15f);
        }

        if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("attackAnimation")) {
            playerDamage = 10;
        }
        if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("attackAnimation1")) {
            playerDamage = 15;
        }
    }

    void disableAttackCollider(){
        attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        attackCollider.enabled = false;
    }
}