using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : MonoBehaviour
{
    float attackRange = 15f;
    bool attackCoolDown = false;
    GameObject player;
    GameObject arrowObj;
    private Animator attackAnimation;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrowContainer;
    Quaternion rotation;
    Vector3 temp_rotation;
    Vector3 angle;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        attackAnimation = gameObject.GetComponent<Animator>();
        rotation = gameObject.transform.rotation;
        isRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRight) {
            temp_rotation = gameObject.transform.eulerAngles;
            temp_rotation.y += 180;
            rotation.eulerAngles = temp_rotation;
        } else {
            rotation = gameObject.transform.rotation;
        }
        angle = rotation.eulerAngles;
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        float xDistance = player.transform.position.x - gameObject.transform.position.x;
        float yDistance = player.transform.position.y - gameObject.transform.position.y;
        float phi = xDistance / distance;

        //update isRight
        if(player.transform.position.x - gameObject.transform.position.x > 0){
            isRight = true;
        }else{
            isRight = false;
        }

        rotateRelativeToPlayer();
        
        if (distance < attackRange)
        {
            angle.z += Mathf.Acos(phi)*Mathf.Rad2Deg;
            rotation.eulerAngles = angle;
            Debug.Log(rotation.eulerAngles);
            //Debug.Log(gameObject.transform.rotation);
            attackAnimation.SetTrigger("attack");
            if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("aiming") && !attackCoolDown) {
                instantiateArrow(xDistance, yDistance);
                attackCoolDown = true;
            }
            if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("shooting")) {
                attackCoolDown = false;
            }
        }
    }

    void instantiateArrow(float x, float y){
        arrowObj = Instantiate(arrow, gameObject.transform.position, rotation);
        arrowObj.transform.SetParent(arrowContainer.transform);
        arrowObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(x, y, 0)*50);
    }

    void rotateRelativeToPlayer(){
        if(isRight){
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }else{
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
