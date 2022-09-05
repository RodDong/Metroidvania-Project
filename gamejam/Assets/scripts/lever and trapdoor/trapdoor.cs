using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapdoor : MonoBehaviour
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
        if(player.transform.position.y > transform.position.y + 6 && isOpen && !gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close")){
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<Animator>().Play("Close");
            isOpen = false;
        }
        if(isOpen && !gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Open")){
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Animator>().Play("Open");
        }
        
    }
}
