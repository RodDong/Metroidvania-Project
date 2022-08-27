using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totemBlock : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player"){
            player.GetComponent<movement>().isProtected = true;
            player.GetComponent<movement>().canJump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player"){
            player.GetComponent<movement>().isProtected = false;
        }
    }
}
