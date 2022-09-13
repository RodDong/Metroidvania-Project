using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapdoorRoom3 : MonoBehaviour
{
    public bool isOpen;
    GameObject player;

    void Start()
    {
        isOpen = false;
        player = GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        if(isOpen && !gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Open") ){
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Animator>().Play("Open");
        }
        
        
        
    }
}
