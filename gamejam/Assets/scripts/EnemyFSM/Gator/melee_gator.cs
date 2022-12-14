using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_gator : MonoBehaviour
{
    private float attackRange = 5f;

    GameObject player;
    private Animator animator;

    //ground detection vars
    [SerializeField] GameObject spearCollider;
    [SerializeField] GameObject VisionDetectArea;
    [SerializeField] GameObject SoundDetectArea;
    // Start is called before the first frame update
    void Start()
    {
        resetSpearCollider();
        player = GameObject.FindGameObjectWithTag("player");
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frames
    void Update()
    {

        float xDistance = Mathf.Abs(SoundDetectArea.GetComponent<MeleeEnemyDetection>().position.x - gameObject.transform.position.x);
        float yDistance = SoundDetectArea.GetComponent<MeleeEnemyDetection>().position.y - gameObject.transform.position.y;
        
        if(xDistance < attackRange && SoundDetectArea.GetComponent<MeleeEnemyDetection>().hasTarget && yDistance < 7){
            attack();
        }else if(yDistance > 7 && SoundDetectArea.GetComponent<MeleeEnemyDetection>().hasTarget){
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
    }

    void attack(){
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("melee_attack")){
            Invoke("fireSpearCollider", 0.46f);
        }
        animator.SetTrigger("attack");
    }

    void fireSpearCollider(){
        spearCollider.GetComponent<BoxCollider2D>().enabled = true;
        if(SoundDetectArea.GetComponent<MeleeEnemyDetection>().isFacingRight){
            spearCollider.GetComponent<Rigidbody2D>().AddForce(Vector2.left*5f, ForceMode2D.Impulse);
        }else{
            spearCollider.GetComponent<Rigidbody2D>().AddForce(Vector2.right*5f, ForceMode2D.Impulse);
        }
        Invoke("stopSpearCollider", 0.3f);
        Invoke("resetSpearCollider", 0.54f);
    }

    void resetSpearCollider(){
        spearCollider.GetComponent<BoxCollider2D>().enabled = false;
        spearCollider.GetComponent<BoxCollider2D>().transform.localPosition = 
        new Vector3(-3, spearCollider.GetComponent<BoxCollider2D>().transform.localPosition.y,0);
    }

    void stopSpearCollider(){
        spearCollider.GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
    }

}
