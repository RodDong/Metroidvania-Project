using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toad : MonoBehaviour
{
    StateMachine<Toad> stateMachine;
    public Animator animator;

    public TMP_Text debugText;
    [HideInInspector] public EnemyDamage enemyHealth;

    int maxHP = 250;

    const int maxRange = 300;

    [SerializeField]
    float detectRange = 200f;

    [SerializeField]
    float attackRange = 50f;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    [Tooltip("Toad所能跳到的最高高度, 设置在镜头边界之上")]
    public float maxJumpHeight;

    [SerializeField]
    [Tooltip("跳跃CD")]
    public float jumpCoolDownTime;

    [SerializeField]
    [Tooltip("击退玩家的force大小")]
    float repulseForce = 5f;

    [SerializeField]
    GameObject ranger;

    [SerializeField]
    GameObject melee;

    [SerializeField]
    public Collider2D coll;
    [SerializeField]
    public Collider2D tongueCol;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    List<Transform> rangerSummonPositionList = new List<Transform>();

    [SerializeField]
    List<Transform> meleeSummonPositionList = new List<Transform>();

    public bool isRising, isFalling;

    Vector3 landingPoint;

    bool hasSummonRangers = false;
    bool hasSummonMelees = false;

    GameObject player;

    movement playerMovement;

    bool canJump = true;


    #region Initialize

    public Toad() : base()
    {
        stateMachine = new StateMachine<Toad>(this);
        stateMachine.SetCurrentState(ToadIdle.Instance);
    }

    void Start()
    {
        enemyHealth = this.GetComponent<EnemyDamage>();
        maxHP = enemyHealth.getHP();
        player = GameObject.FindGameObjectWithTag("player");
        playerMovement = player.GetComponent<movement>();
        ToadIdle.Instance.Enter(this);
        tongueCol.enabled = false;
    }

    void Update()
    {
        stateMachine.StateMachineUpdate();
        if (enemyHealth.getHP() <= 3 * maxHP / 4 && !hasSummonRangers)
        {
            hasSummonRangers = true;
            // ToadRoar?
            stateMachine.SetGlobalState(ToadRoar.Instance);
            SummonRangers();
        }
        if (enemyHealth.getHP() <= maxHP / 2 && !hasSummonMelees)
        {
            hasSummonMelees = true;
            stateMachine.SetGlobalState(ToadRoar.Instance);
            SummonMelees();
        }
    }

    public StateMachine<Toad> GetStateMachine()
    {
        return stateMachine;
    }

    #endregion

    /*
 1.BOSS AI:
HP: ~20刀 ~250
一阶段:
    a.吐舌头 b.跳
    Update() {
    if(position in range && hasTarget):
        a
    else if hasTarget:
        b
    else:
        idle
    }
    固定动作:
    if (hp < 3/4):
        summon 2 rangers (uppder platform)
if (hp < 1/2):
固定动作:
    归位到中间, summon 2 melee
二阶段:
    a.固定位置跳5下 b.吐舌头 c.跳
    Update() {
    if(position in range && hasTarget):
        c
    else if hasTarget:
        b
    else if (!hasTarget && detectCounter = 3 && !coolDown):
        a
    else:
        idle
    }
*/

    /// <summary>
    /// 判断应该换成哪个状态，并对stateMachine进行切换
    /// </summary>
    public void ChangeState()
    {
        if (stateMachine.GlobalState() != null)
        {
            stateMachine.SetGlobalState(null);
            return;
        }

        float playerDistance = PlayerDistance();

        // Stage 01
        if (enemyHealth.getHP() >= maxHP / 2)
        {
            if (playerDistance <= attackRange && playerMovement.makeSound == true)
            {
                if ((gameObject.transform.eulerAngles.y > 90 && player.transform.position.x >= transform.position.x)
                || (gameObject.transform.eulerAngles.y < 90 && player.transform.position.x <= transform.position.x))
                {
                    invokeAttack();
                }
                else
                {

                    Invoke("invokeAttack", 1f);
                }

            }
            else if (playerDistance <= detectRange && canJump && playerMovement.makeSound == true)
            {
                stateMachine.ChangeState(ToadJump.Instance);
            }
            else
            {
                stateMachine.ChangeState(ToadIdle.Instance);
            }
        }
        // Stage 02
        else if (enemyHealth.getHP() > 0)
        {
            if (playerDistance <= attackRange)
            {
                if ((gameObject.transform.eulerAngles.y > 90 && player.transform.position.x >= transform.position.x)
                || (gameObject.transform.eulerAngles.y < 90 && player.transform.position.x <= transform.position.x))
                {
                    invokeAttack();
                }
                else
                {

                    Invoke("invokeAttack", 1f);
                }
            }
            else if (playerDistance <= detectRange && canJump)
            {
                stateMachine.ChangeState(ToadJump.Instance);
            }
            else
            {
                stateMachine.ChangeState(ToadIdle.Instance);
            }
        }
        // Die
        else if (enemyHealth.getHP() <= 0)
        {
            stateMachine.ChangeState(ToadDeath.Instance);
        }
    }

    /// <summary>
    /// 返回玩家与boss之间的距离
    /// </summary>
    /// <returns>玩家与boss之间的距离</returns>
    public float PlayerDistance()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
            if (player == null)
            {
                return maxRange;
            }
        }

        return (player.transform.position - transform.position).magnitude;

    }

    public void SummonRangers()
    {
        Debug.Log("Summon Rangers!");
        for (int i = 0; i < rangerSummonPositionList.Count; i++)
        {
            Transform tf = rangerSummonPositionList[i];
            Instantiate(ranger, tf.position, tf.rotation);
        }
    }

    public void SummonMelees()
    {
        Debug.Log("Summon Melees!");
        for (int i = 0; i < meleeSummonPositionList.Count; i++)
        {
            Transform tf = meleeSummonPositionList[i];
            Instantiate(melee, tf.position, tf.rotation);
        }
    }

    public void Jump()
    {
        Debug.Log("Is Rising!");

        isRising = true;
        coll.enabled = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 跳跃后触碰到最高点才启用，将Toad移至玩家上空
    /// </summary>
    public void TitanFall()
    {
        Debug.Log("Is Falling!");
        isRising = false;
        isFalling = true;

        landingPoint = player.transform.position;
        // 移至player上空
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        rb.velocity = new Vector3(0, -rb.velocity.y, 0);
    }

    /// <summary>
    /// 只有当Toad和落地点之间没有阻挡，才重新启用Toad的碰撞体
    /// </summary>
    public void EnableColliderWhileFalling()
    {
        Vector2 dir = landingPoint - transform.position;
        RaycastHit2D info = Physics2D.Raycast(transform.position, dir, dir.magnitude, 1 << LayerMask.NameToLayer("Ground"));
        if (info.collider == null && dir.magnitude <= 3f)// || !info.collider.CompareTag("ground"))
        {
            Debug.Log("Enable Collider! " + transform.position + landingPoint);
            coll.enabled = true;
        }
    }

    /// <summary>
    /// 跳回地面触发
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ground") && isFalling)
        {
            isFalling = false;
            canJump = false;
            Invoke("SetCanJump2True", jumpCoolDownTime);
        }
        else if (other.collider.CompareTag("player") && isFalling)
        {
            Debug.Log("Fall on player! Add force" + transform.right * repulseForce);
            playerMovement.playerRepulse(transform.right * repulseForce);
            isFalling = false;
            canJump = false;
            Invoke("SetCanJump2True", jumpCoolDownTime);
        }
    }

    public void Flip()
    {
        if (player.transform.position.x <= transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void SetCanJump2True()
    {
        canJump = true;
    }

    void disableTongue()
    {
        tongueCol.enabled = false;
    }

    void invokeAttack()
    {
        stateMachine.ChangeState(ToadAttack.Instance);
    }

}
