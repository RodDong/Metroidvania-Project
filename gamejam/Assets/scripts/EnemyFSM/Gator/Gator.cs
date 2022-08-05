using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : MonoBehaviour
{
    float attackRange= 15f;
    GameObject player;
    GameObject arrowObj;
    private Animator attackAnimation;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrowContainer;
    Quaternion rotation;
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
        angle = rotation.eulerAngles;
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        float xDistance = player.transform.position.x - gameObject.transform.position.x;
        float theta = xDistance / distance;

        //update isRight
        if(player.transform.position.x - gameObject.transform.position.x > 0){
            isRight = true;
        }else{
            isRight = false;
        }

        rotateRelativeToPlayer();
        
        if (distance < attackRange)
        {
            angle.z += Mathf.Acos(theta)*Mathf.Rad2Deg + 180;
            rotation.eulerAngles = angle;
            Debug.Log(rotation.eulerAngles);
            Debug.Log(gameObject.transform.rotation);
            attackAnimation.SetTrigger("attack");
            if(arrowContainer.transform.childCount != 0){
                //Invoke("instantiateArrow", 1f);
            }else{
                arrowObj = Instantiate(arrow, gameObject.transform.position, rotation);
                arrowObj.transform.SetParent(arrowContainer.transform);
            }
            
        }
    }

    void instantiateArrow(){
        arrowObj = Instantiate(arrow, gameObject.transform.position, rotation);
        arrowObj.transform.SetParent(arrowContainer.transform);
    }

    void rotateRelativeToPlayer(){
        if(isRight){
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }else{
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
