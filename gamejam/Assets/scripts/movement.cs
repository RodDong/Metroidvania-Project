using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    Rigidbody2D rb;
    public bool canJump = false;
    float jump_init_v = 20f;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground") {
            canJump = true;
        }
    }


    void Update() {
        rb.freezeRotation = true;
        processInput();
    }

    void processInput() {
        Vector2 v = rb.velocity;
        if(Input.GetKeyDown(KeyCode.Space) && canJump) {
            v.y = jump_init_v;
            canJump = false;
        }
        v.x = Input.GetAxisRaw("Horizontal") * 10f;
        rb.velocity = v;
        
    }
}
