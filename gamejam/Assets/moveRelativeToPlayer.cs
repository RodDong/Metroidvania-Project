using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRelativeToPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody2D playerRb;
    private Animator attackAnimation;
    Quaternion playerRotation;
    [SerializeField]Vector3 offsetR = new Vector3(2.5f,1.5f,0f);
    [SerializeField]Vector3 offsetL = new Vector3(-2.5f,1.5f,0f);
    [SerializeField] int rotation = 0;
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
            attackAnimation.SetTrigger("attack2");
        }

        if(!m.isRight){
            gameObject.transform.position = playerRb.transform.position + offsetL;
            gameObject.transform.localRotation = Quaternion.Euler(0,180,-rotation);
        }else{
            gameObject.transform.position = playerRb.transform.position + offsetR;
            gameObject.transform.localRotation = Quaternion.Euler(0,0,-rotation);
        }
        

    }

}
