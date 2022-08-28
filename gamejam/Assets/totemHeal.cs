using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totemHeal : MonoBehaviour
{
    [HideInInspector] public float healCoolDown = 2f;
    bool heal;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        healCoolDown -= Time.deltaTime;
        if(heal && healCoolDown <= 0 ){
            player.GetComponent<Health>().Recover();
            healCoolDown = 2f;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "player"){
            heal = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "player"){
            heal = false;
        }
    }
}
