using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetectionJump : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public Vector3 position;
    public bool hasTarget;
    void Start()
    {
        hasTarget = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        
        if((other.tag == "player" ||  other.tag == "attackArea") && player.GetComponent<movement>().makeSound){
            
            position = other.transform.position;
            hasTarget = true;
            Invoke("loseTarget",5f);
        }
    }

    private void loseTarget(){
        hasTarget = false;
    }

    
}
