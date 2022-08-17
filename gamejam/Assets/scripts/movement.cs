using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class movement : MonoBehaviour
{
    // FSM components
    private State currentState;
    public float transition;
    public float duration;
    public float coolDown;
    //game Objects
    [SerializeField] public GameObject attackArea;

    public Vector3 position;
    public bool isRight;
    bool isFalling;
    public bool makeSound, isOnRoad;
    //Jump vars
    public bool canJump = false;
    [HideInInspector] public int playerDamage;
    float jump_init_v = 20f;
    //attack vars
    public bool attacking = false;
    [HideInInspector] public Collider2D attackCollider;
    Rigidbody2D rb;
    [HideInInspector] public Animator attackAnimation;

    //Game initialization
    void Start() {
        currentState = new IdleState();
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
        transition -= Time.deltaTime;
        duration -= Time.deltaTime;
        coolDown -= Time.deltaTime;
        currentState.Execute(this);

        attacking = false;
        position = rb.transform.position;
        rb.freezeRotation = true;
        processInput();
        processAttack();
        if(rb.velocity.y <-3){
            isFalling = true;
        }
    }

    public void ChangeState(State state) {
        currentState = state;
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
    }

    public void enableAttackCollider() {
        attacking = true;
        makeSound = true;
        attackCollider.enabled = true;
        attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        Invoke("disableAttackCollider", 0.15f);
    }

    public void disableAttackCollider() {
        attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        attackCollider.enabled = false;
        makeSound = false;
    }

    void disableSound(){
        makeSound = false;
    }
}

public abstract class State {
    public abstract void Execute(movement player);
}

public class IdleState : State {
    public override void Execute(movement player) {
        if (Input.GetMouseButtonDown(0) && player.coolDown <= 0) {
            // move to attack 1
            player.attackAnimation.Play("attackAnimation");
            player.duration = 0.25f;
            player.transition = 0.5f;
            player.enableAttackCollider();
            player.ChangeState(new AttackState1());
        }
    }
}

public class AttackState1 : State { 
    public override void Execute(movement player) {
        if (Input.GetMouseButtonDown(0) && player.transition > 0 && player.duration <= 0) {
            player.duration = 0.25f;
            player.enableAttackCollider();
            player.attackAnimation.Play("attackAnimation1");
            player.ChangeState(new AttackState2());
        } else if (player.transition <= 0) {
            player.attackAnimation.Play("Idle");
            player.ChangeState(new IdleState());
        } else if (player.duration <= 0) {
            player.attackAnimation.Play("Idle");
        }
    }
}

public class AttackState2 : State {
    public override void Execute(movement player) {
        if (player.duration <= 0) {
            player.attackAnimation.Play("Idle");
            player.coolDown = 1f;
            player.ChangeState(new IdleState());
        }
    }
}