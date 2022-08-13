using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
public class BossMovement : MonoBehaviour
{
    Rigidbody2D bossRigidBody;
    [SerializeField] Transform limbSolver1 , limbSolver2;
    Vector3 solverPos1, solverPos2;
    
    // Start is called before the first frame update
    void Start()
    {
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();
        solverPos1 = limbSolver1.localPosition;
        solverPos2 = limbSolver2.localPosition;

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "ground"){
            Debug.Log(limbSolver1.localPosition);
            limbSolver1.localPosition = solverPos1;
            limbSolver2.localPosition = solverPos2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            gameObject.GetComponentInParent<Animator>().SetTrigger("jump");
            bossRigidBody.AddForce(Vector2.up*20, ForceMode2D.Impulse);
            bossRigidBody.AddForce(Vector2.left*20, ForceMode2D.Impulse);
            
            
        }
        // if(Input.GetKeyDown(KeyCode.A)){
        //     gameObject.transform.Translate(Vector3.left);
        // }
        
    }
}
