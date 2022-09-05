using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLever : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject waterWheel;
    private bool inRange;
    private float timer;
    bool Spawn = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }


    void Update()
    {
        timer -= Time.deltaTime;
        
        if(inRange && Input.GetKeyDown(KeyCode.X)){
            timer = 0.2f;
            gameObject.GetComponent<Animator>().Play("bossLever");
            waterWheel.GetComponent<waterWheel>().speed = -0.025f;
            Spawn = true;
        }
        if(timer <= 0){
            gameObject.GetComponent<Animator>().Play("Idle");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player"){
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "player"){
            inRange = false;
        }   
    }
}
