using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
public class BossMovement : MonoBehaviour
{
    Rigidbody2D bossRigidBody;
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject BossJumpDetectionArea, BossAttackDetectionArea;
    Vector3 solverPos1, solverPos2;
    public Vector3 position;
    float jumpDelay = 0.4f;
    float jumpDuration = 2.44f;
    float forceY = 20f;
    float forceX;
    float deltaX;
    public bool isFacingRight => Mathf.Abs(transform.eulerAngles.y) > 90;
    bool isRight;
    bool jumpHasTarget, attackHasTarget;
    void Start()
    {
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();
        forceY*=bossRigidBody.mass;

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.collider.tag == "ground"){
            bossRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void Update()
    {
        if(!Boss.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            return;
        }
        jumpHasTarget = BossJumpDetectionArea.GetComponent<BossDetectionJump>().hasTarget;
        attackHasTarget = BossAttackDetectionArea.GetComponent<BossDetectionAttack>().hasTarget;
        
        if(attackHasTarget || jumpHasTarget){
            deltaX = BossJumpDetectionArea.GetComponent<BossDetectionJump>().position.x - BossJumpDetectionArea.transform.position.x;
            
            isRight = BossJumpDetectionArea.GetComponent<BossDetectionJump>().position.x - BossJumpDetectionArea.transform.position.x > 0;
        }

        if(attackHasTarget){
            rotateRelativeToPlayer();
            gameObject.GetComponentInParent<Animator>().SetTrigger("attack");
        }else if (jumpHasTarget){
            rotateRelativeToPlayer();
            gameObject.GetComponentInParent<Animator>().SetTrigger("jump");
            Invoke("Jump", jumpDelay);
        }
        
        
    }

    void Jump(){
        forceX = deltaX/jumpDuration*bossRigidBody.mass;
        bossRigidBody.constraints = RigidbodyConstraints2D.None;
        bossRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        bossRigidBody.AddForce(Vector2.up*forceY, ForceMode2D.Impulse);
        bossRigidBody.AddForce(Vector2.right * forceX, ForceMode2D.Impulse);
    }
    private void Flip()
    {
        if (!isFacingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void rotateRelativeToPlayer()
    {
        if (isRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
