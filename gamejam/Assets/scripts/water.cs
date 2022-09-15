using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectWithTag("player").GetComponent<Health>().isInWater = false;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "player"){
            other.gameObject.GetComponent<Health>().isInWater = true;
            other.gameObject.GetComponent<Health>().TakeDamage();
            other.transform.position = other.gameObject.GetComponent<currentPlatformPos>().curPlatformPos;
        }
    }
}
