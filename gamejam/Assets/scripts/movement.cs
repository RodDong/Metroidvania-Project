using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    [SerializeField] public Slider cdSlider;
    [SerializeField] public GameObject attackArea;
    // if transition > 0, move from attack1 to attack2
    [HideInInspector] public float transition;
    // if duration <= 0, move to idle state
    [HideInInspector] public float duration;
    // if coolDown > 0, player cannot attack
    [HideInInspector] public float coolDown;
    //game Objects
    [HideInInspector] public Vector3 position;
    [HideInInspector] public bool isRight;
    [HideInInspector] bool isFalling;
    [HideInInspector] public bool makeSound, isOnRoad;
    //Jump vars
    [HideInInspector] public bool canJump = false;
    [HideInInspector] public int playerDamage;
    //attack vars
    [HideInInspector] public bool attacking = false;
    [HideInInspector] public Collider2D attackCollider;
    public Animator attackAnimator;
    float jump_init_v = 20f;
    [SerializeField]
    Rigidbody2D rb;
    // FSM components
    private State currentState;

    //Game initialization
    void Start()
    {
        currentState = new IdleState();
        //initialize AttackArea collider
        attackCollider = attackArea.GetComponent<Collider2D>();
        attackCollider.enabled = false;
        cdSlider.maxValue = 1f;
        cdSlider.value = 1f;

        //initialize player rigidbody 
        rb = GetComponent<Rigidbody2D>();
        isRight = true;

        // Initialize player attack animation
        attackAnimator = GameObject.FindGameObjectWithTag("attackAnimator").GetComponent<Animator>();
        playerDamage = 10;

        //Initialize makeSound
        isFalling = false;
        makeSound = false;
    }

    //Update frames in game
    void Update()
    {
        transition -= Time.deltaTime;
        duration -= Time.deltaTime;
        coolDown -= Time.deltaTime;
        if (coolDown > 0)
        {
            if (cdSlider.value + Time.deltaTime < 1f)
            {
                cdSlider.value += Time.deltaTime;
            }
            else
            {
                cdSlider.value = 1f;
            }
        }
        currentState.Execute(this);

        attacking = false;
        position = rb.transform.position;
        rb.freezeRotation = true;
        processInput();
        processAttack();
        if (rb.velocity.y < -3)
        {
            isFalling = true;
        }
    }

    public void ChangeState(State state)
    {
        currentState = state;
    }

    //Collision handler for bool canJump
    private void OnCollisionEnter2D(Collision2D other)
    {
        //update isOnRoad boolean
        if (other.gameObject.tag == "road")
        {
            isOnRoad = true;
        }
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "OneWayPlatform")
        {
            isOnRoad = false;
        }

        //update makeSound and is Falling
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "OneWayPlatform" || other.gameObject.tag == "road")
        {
            canJump = true;
            if (isFalling)
            {
                makeSound = true;
                isFalling = false;
                Invoke("disableSound", 0.1f);
            }
        }
    }

    //process movements via inputs 
    void processInput()
    {
        //Process player movements (Space for jump, A & D for horizontal movements)
        Vector2 v = rb.velocity;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            v.y = jump_init_v;
            canJump = false;
        }

        v.x = Input.GetAxisRaw("Horizontal") * 10f;
        rb.velocity = v;

        //Player rotation based on input 
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isRight)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            isRight = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isRight)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            isRight = true;
        }
    }

    //Process left mouse click for player attack
    void processAttack()
    {
        if (coolDown > 0)
        {
            playerDamage = 10;
        }
        if (attackAnimator.GetCurrentAnimatorStateInfo(0).IsName("attackAnimation1"))
        {
            playerDamage = 15;
        }
    }

    public void enableAttackCollider()
    {
        attacking = true;
        makeSound = true;
        attackCollider.enabled = true;
        attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        Invoke("disableAttackCollider", 0.15f);
    }

    public void disableAttackCollider()
    {
        attackArea.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        attackCollider.enabled = false;
        makeSound = false;
    }

    void disableSound()
    {
        makeSound = false;
    }

    /// <summary>
    /// 玩家被击退
    /// </summary>
    /// <param name="force"></param>
    public void playerRepulse(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}

public abstract class State
{
    public abstract void Execute(movement player);
}

public class IdleState : State
{
    public override void Execute(movement player)
    {
        if (Input.GetMouseButtonDown(0) && player.coolDown <= 0)
        {
            // move to attack 1
            player.attackAnimator.Play("attackAnimation");
            player.duration = 0.25f;
            player.transition = 0.5f;
            player.cdSlider.value = 0.5f;
            player.enableAttackCollider();
            player.ChangeState(new AttackState1());
        }
    }
}

public class AttackState1 : State
{
    public override void Execute(movement player)
    {
        if (Input.GetMouseButtonDown(0) && player.transition > 0 && player.duration <= 0)
        {
            player.duration = 0.25f;
            player.enableAttackCollider();
            player.cdSlider.value = 0f;
            player.attackAnimator.Play("attackAnimation1");
            player.ChangeState(new AttackState2());
        }
        else if (player.transition <= 0)
        {
            player.coolDown = 1f;
            player.cdSlider.value = 0f;
            player.attackAnimator.Play("Idle");
            player.ChangeState(new IdleState());
        }
    }
}

public class AttackState2 : State
{
    public override void Execute(movement player)
    {
        if (player.duration <= 0)
        {
            player.attackAnimator.Play("Idle");
            player.coolDown = 1f;
            player.ChangeState(new IdleState());
        }
    }
}