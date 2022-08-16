using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    //game Objects
    [SerializeField] GameObject attackArea;

    public Vector3 position;
    public bool isRight;
    bool isFalling;
    public bool makeSound, isOnRoad;
    //Jump vars
    public bool canJump = false;
    [HideInInspector]public int playerDamage;
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

        //Initialize makeSound
        isFalling = false;
        makeSound = false;
    }

    //Update frames in game
    void Update() {
        attacking = false;
        position = rb.transform.position;
        rb.freezeRotation = true;
        processInput();
        processAttack();
        if(rb.velocity.y <-3){
            isFalling = true;
        }
    }

    //Collision handler for bool canJump
    private void OnCollisionEnter2D(Collision2D other) {
        //update isOnRoad boolean
        if(other.gameObject.tag == "road"){
            isOnRoad = true;
        }
        if(other.gameObject.tag == "ground" || other.gameObject.tag == "OneWayPlatform"){
            isOnRoad = false;
        }

        //update makeSound and is Falling
        if(other.gameObject.tag == "ground" || other.gameObject.tag == "OneWayPlatform" || other.gameObject.tag == "road") {
            canJump = true;
            if(isFalling){
                makeSound = true;
                isFalling = false;
                Invoke("disableSound", 0.1f);
            }
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
        if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("coolDown") || attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("coolDown1")) {
            playerDamage = 10;
        }
        if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("Transition1")) {
            playerDamage = 15;
        }

        if(Input.GetMouseButtonDown(0) && !attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("coolDown") 
        && !attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("coolDown1")
        && !attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("attackAnimation")
        && !attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("attackAnimation1")) {
            attacking = true;
            makeSound = true;
            attackCollider.enabled = true;
            attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            Invoke("disableAttackCollider", 0.15f);
        }
    }

    void disableAttackCollider(){
        attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        attackCollider.enabled = false;
        makeSound = false;
    }

    void disableSound(){
        makeSound = false;
    }
}