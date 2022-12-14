using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mouse : MonoBehaviour
{
    private StateMachine<Mouse> stateMachine;

    public Animator animator;

    #region Wander

    [SerializeField]
    private float wanderSpeed;

    [SerializeField]
    private float groundDetectDistance;

    [SerializeField]
    private Transform frontGroundDetection;

    [SerializeField]
    private Transform backGroundDetection;
    [SerializeField] private GameObject wall;
    [SerializeField] private int leftWall;
    [SerializeField] private int rightWall;
    private WallList wallList;

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
        try {
            wallList = wall.GetComponent<WallList>();
        } catch (UnassignedReferenceException e) {
            Debug.Log(e.Message);
        }
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

        int groundMask = 1 << LayerMask.NameToLayer("Ground");
        int platformMask = 1 << LayerMask.NameToLayer("Platform");
        int noCollisionPlatformMask = 1 << LayerMask.NameToLayer("PlatformWithoutPlayerCollision");
        Collider2D frontRayCollider = 
                    Physics2D.Raycast(frontGroundDetection.position, 
                        Vector2.down, 
                        groundDetectDistance, 
                        groundMask | platformMask | noCollisionPlatformMask).collider,
                   backRayCollider = 
                    Physics2D.Raycast(
                        backGroundDetection.position, 
                        Vector2.down, 
                        groundDetectDistance, 
                        groundMask | platformMask | noCollisionPlatformMask).collider;

        noGroundOnBothSides = frontRayCollider == null && backRayCollider == null;
        if (frontRayCollider == null)
        {
            Flip();
        }
        if (wallList != null && (frontGroundDetection.transform.position.x < wallList.wallPosLists[leftWall] 
        || frontGroundDetection.transform.position.x > wallList.wallPosLists[rightWall])) {
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
