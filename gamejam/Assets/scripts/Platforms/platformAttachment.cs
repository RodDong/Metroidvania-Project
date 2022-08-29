using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformAttachment : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "player"){
            Debug.Log(other.gameObject.tag);
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.transform.parent = null;
    }

}
