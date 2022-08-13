using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
public class BossMovement : MonoBehaviour
{
    Rigidbody2D bossRigidBody;
    Vector3 solverPos1, solverPos2;
    [SerializeField]
    float jumpDelay = 0.2f;
    
    void Start()
    {
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.collider.tag == "ground"){
            bossRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            gameObject.GetComponentInParent<Animator>().SetTrigger("jump");
            Invoke("Jump", jumpDelay);
        }
        
    }

    void Jump(){
        bossRigidBody.constraints = RigidbodyConstraints2D.None;
        bossRigidBody.AddForce(Vector2.up*20, ForceMode2D.Impulse);
        bossRigidBody.AddForce(Vector2.left*30, ForceMode2D.Impulse);
    }
}
