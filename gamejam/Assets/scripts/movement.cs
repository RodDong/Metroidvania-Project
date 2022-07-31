using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody2D rb;
    bool canJump = false;
    
    Vector2 vertical = new Vector2(0f, 20f);
    Vector2 horizontal = new Vector2(0f, 0f);
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
        processInput();
        
    }

    void processInput(){
        if(Input.GetKeyDown(KeyCode.Space) && canJump){
            vertical.x = rb.velocity.x;
            rb.velocity = vertical;
            canJump = false;
        }
        float xInput = Input.GetAxisRaw("Horizontal");
        horizontal.x = xInput*10f;
        horizontal.y = rb.velocity.y;
        rb.velocity = horizontal;
    }
}
