using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mouse : EnemyBase
{
    private StateMachine<Mouse> stateMachine;

    public Animator animator;

    public TMP_Text debugText;

    #region Wander

    [SerializeField]
    private float wanderSpeed;

    [SerializeField]
    private float groundDetectDistance;

    [SerializeField]
    private Transform frontGroundDetection;

    [SerializeField]
    private Transform backGroundDetection;

    public bool isFacingRight => Mathf.Abs(transform.eulerAngles.y) < 90;

    /// <summary>
    /// 两边都检测不到地面
    /// </summary>
    private bool noGroundOnBothSides;

    #endregion

    public Mouse() : base()
    {
        stateMachine = new StateMachine<Mouse>(this);
        stateMachine.SetCurrentState(MouseWander.Instance);
    }

    private void Start()
    {
        MouseWander.Instance.Enter(this);
    }

    private void Update()
    {
        stateMachine.StateMachineUpdate();
    }

    public StateMachine<Mouse> GetStateMachine()
    {
        return stateMachine;
    }


    #region Wander

    public void Wander()
    {
        transform.Translate(Vector2.right * wanderSpeed * Time.deltaTime);

        Collider2D isFrontGround = Physics2D.Raycast(frontGroundDetection.position, Vector2.down, groundDetectDistance).collider,
                   isBackGround = Physics2D.Raycast(backGroundDetection.position, Vector2.down, groundDetectDistance).collider;

        noGroundOnBothSides = (isFrontGround == null) && (isBackGround == null);

        if (isFrontGround == null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (isFacingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// 是否可以继续游荡
    /// </summary>
    /// <returns></returns>
    public bool CanWander()
    {
        return !noGroundOnBothSides;
    }

    #endregion

}