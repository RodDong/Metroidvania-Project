using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : MonoBehaviour
{
    float attackRange = 30f;
    GameObject player;
    GameObject arrowObj;
    private Animator attackAnimation;
    // for Invoke()
    private float xDistance;
    private float yDistance;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject rangerEnemyDetection;
    private float timer = 0f;
    [SerializeField] float attackCD;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        attackAnimation = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            return;
        } 

        float distance = Vector3.Distance(rangerEnemyDetection.GetComponent<RangeEnemyDetection>().position, gameObject.transform.position);
        xDistance = rangerEnemyDetection.GetComponent<RangeEnemyDetection>().position.x - gameObject.transform.position.x;
        yDistance = rangerEnemyDetection.GetComponent<RangeEnemyDetection>().position.y - gameObject.transform.position.y;
        float phi = xDistance / distance;

        if (distance < attackRange && rangerEnemyDetection.GetComponent<RangeEnemyDetection>().hasTarget)
        {

            attackAnimation.SetTrigger("attack");
            timer += attackCD;
            Invoke("instantiateArrow", 0.35f);
            
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
