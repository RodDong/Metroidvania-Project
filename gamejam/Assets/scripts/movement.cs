using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    Rigidbody2D rb;
    public Vector3 position;
    GameObject[] swords;
    
    public bool canJump = false;
    float jump_init_v = 20f;
    
    void Start() {
        swords = GameObject.FindGameObjectsWithTag("sword");
        
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground") {
            canJump = true;
        }
    }


    void Update() {
        position = rb.transform.position;
        rb.freezeRotation = true;
        processInput();
        updateSwordPos();
    }

    void updateSwordPos(){
        foreach(GameObject sword in swords){
            Vector2 swordPos = sword.transform.position;
            swordPos.x = rb.transform.position.x + 2;
            swordPos.y = rb.transform.position.y + 2;
            sword.transform.position = swordPos;

        }
    }

    void processInput() {
        //Process player movement
        Vector2 v = rb.velocity;
        if(Input.GetKeyDown(KeyCode.Space) && canJump) {
            v.y = jump_init_v;
            canJump = false;
        }
        v.x = Input.GetAxisRaw("Horizontal") * 10f;
        rb.velocity = v;

        //Process Player attack
        if(Input.GetMouseButton(0)){
            Debug.Log(rb.transform.position.x+" "+rb.transform.position.y);
            foreach(GameObject sword in swords){
                sword.transform.RotateAround(new Vector2(0,0), Vector3.forward, 100*Time.deltaTime);
            }
        }
        
        
    }
}
