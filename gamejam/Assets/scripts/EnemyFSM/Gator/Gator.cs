using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : MonoBehaviour
{
    float attackRange = 10f;
    bool attackCoolDown = false;
    GameObject player;
    GameObject arrowObj;
    private Animator attackAnimation;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrowContainer;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        attackAnimation = gameObject.GetComponent<Animator>();
        isRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        float xDistance = player.transform.position.x - gameObject.transform.position.x;
        float yDistance = player.transform.position.y - gameObject.transform.position.y;
        float phi = xDistance / distance;

        //update isRight
        if (player.transform.position.x - gameObject.transform.position.x > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }

        rotateRelativeToPlayer();

        if (distance < attackRange)
        {
            //Debug.Log(gameObject.transform.rotation);
            attackAnimation.SetTrigger("attack");
            if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("aiming") && !attackCoolDown)
            {
                instantiateArrow(xDistance, yDistance);
                attackCoolDown = true;
            }
            if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
            {
                attackCoolDown = false;
            }
        }
    }

    void instantiateArrow(float x, float y)
    {
        float arrow_rotation = Mathf.Atan(y / x) * Mathf.Rad2Deg;
        if (isRight)
        {
            arrow_rotation += 180f;
        }
        Quaternion arrow_quaternion = new Quaternion();
        arrow_quaternion.eulerAngles = new Vector3(0, 0, arrow_rotation);
        //arrowObj = Instantiate(arrow, gameObject.transform.position, arrow_quaternion);
        arrowObj = ObjectPool.Instance.Spawn(gameObject.transform.position, arrow_quaternion);
        arrow.layer = LayerMask.NameToLayer("Projectile");
        //arrowObj.transform.SetParent(arrowContainer.transform);
        arrowObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(x, y, 0) * 600 / (Mathf.Sqrt(x * x + y * y)));
    }

    void rotateRelativeToPlayer()
    {
        if (isRight)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
