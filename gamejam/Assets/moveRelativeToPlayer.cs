using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRelativeToPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody2D playerRb;
    private Animator attackAnimation;
    Quaternion playerRotation;
    Vector3 offsetR = new Vector3(2.5f,1f,0f);
    Vector3 offsetL = new Vector3(-2.5f,1f,0f);
    movement m;
    
    // Start is called before the first frame update
    void Start()
    {
        attackAnimation = gameObject.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRotation = playerRb.transform.localRotation;
        m = GameObject.FindGameObjectWithTag("player").GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m.attacking){
            attackAnimation.SetTrigger("attack");
        }

        if(!m.isRight){
            gameObject.transform.position = playerRb.transform.position + offsetL;
            gameObject.transform.localRotation = Quaternion.Euler(0,180,-70);
        }else{
            gameObject.transform.position = playerRb.transform.position + offsetR;
            gameObject.transform.localRotation = Quaternion.Euler(0,0,-70);
        }
        

    }

}
