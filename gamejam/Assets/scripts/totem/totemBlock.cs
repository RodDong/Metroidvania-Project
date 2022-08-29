using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totemBlock : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }


    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player"){
            player.GetComponent<movement>().isProtected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player"){
            player.GetComponent<movement>().isProtected = false;
        }
    }
}
