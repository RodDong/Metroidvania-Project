using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody2D rb;
    bool canJump = false;
    Vector2 force = new Vector2(0f, 20f);
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground"){
            Debug.Log(1);
            canJump = true;
        }
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump){
            
            rb.AddForce(force, ForceMode2D.Impulse);
            canJump = false;
        }
    }
}
