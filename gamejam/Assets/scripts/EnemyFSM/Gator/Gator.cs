using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : MonoBehaviour
{
    float attackRange= 15f;
    GameObject player;
    private Animator attackAnimation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        attackAnimation = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
 
        if (distance < attackRange)
        {
            Debug.Log("attack");
            attackAnimation.SetTrigger("attack");
        }
    }
}
