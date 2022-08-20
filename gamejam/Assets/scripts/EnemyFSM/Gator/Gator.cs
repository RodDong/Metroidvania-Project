using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : MonoBehaviour
{
    float attackRange = 30f;
    bool attackCoolDown = false;
    GameObject player;
    GameObject arrowObj;
    private Animator attackAnimation;
    // for Invoke()
    private float xDistance;
    private float yDistance;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject rangerEnemyDetection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        attackAnimation = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(rangerEnemyDetection.GetComponent<RangeEnemyDetection>().position, gameObject.transform.position);
        xDistance = rangerEnemyDetection.GetComponent<RangeEnemyDetection>().position.x - gameObject.transform.position.x;
        yDistance = rangerEnemyDetection.GetComponent<RangeEnemyDetection>().position.y - gameObject.transform.position.y;
        float phi = xDistance / distance;

        if (distance < attackRange && rangerEnemyDetection.GetComponent<RangeEnemyDetection>().hasTarget)
        {

            attackAnimation.SetTrigger("attack");
            
            if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("shooting") && !attackCoolDown)
            {
                Invoke("instantiateArrow", 0.35f);
                attackCoolDown = true;
            }
            else if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                
                attackCoolDown = false;
            }
        }
        if(attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("returning")){
            attackAnimation.ResetTrigger("attack");
        }
    }

    void instantiateArrow()
    {
        float arrow_rotation = Mathf.Atan(yDistance / xDistance) * Mathf.Rad2Deg;
        if (rangerEnemyDetection.GetComponent<RangeEnemyDetection>().isRight)
        {
            arrow_rotation += 180f;
        }
        Quaternion arrow_quaternion = new Quaternion();
        arrow_quaternion.eulerAngles = new Vector3(0, 0, arrow_rotation);
        arrowObj = ObjectPool.Instance.Spawn(gameObject.transform.position, arrow_quaternion);
        arrow.layer = LayerMask.NameToLayer("Projectile");
        arrowObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(xDistance, yDistance, 0) * 600 / (Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance)));
    }
}
