using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
public class BossMovement : MonoBehaviour
{
    Rigidbody2D bossRigidBody;
    [SerializeField] GameObject Boss;
    Vector3 solverPos1, solverPos2;
    public Vector3 position;
    float jumpDelay = 0.4f;
    float jumpDuration = 2.44f;
    float forceY = 20f;
    float forceX;
    float deltaX;
    public bool isFacingRight => Mathf.Abs(transform.eulerAngles.y) > 90;
    void Start()
    {
        
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(isFacingRight);

    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.collider.tag == "ground"){
            Flip();
            bossRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            forceX = deltaX/jumpDuration;

        }
    }

    void Update()
    {
        if(Boss.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            gameObject.GetComponentInParent<Animator>().SetTrigger("jump");
            Invoke("Jump", jumpDelay);
        }
        
    }

    void Jump(){
        bossRigidBody.constraints = RigidbodyConstraints2D.None;
        bossRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        bossRigidBody.AddForce(Vector2.up*forceY, ForceMode2D.Impulse);
        bossRigidBody.AddForce(Vector2.left, ForceMode2D.Impulse);
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
}
