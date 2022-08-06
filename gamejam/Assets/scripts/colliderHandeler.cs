using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderHandeler : MonoBehaviour
{
    Rigidbody2D enemy;
    movement m;

    void Start()
    {
        enemy = gameObject.GetComponent<Rigidbody2D>();
        m = GameObject.FindGameObjectWithTag("player").GetComponent<movement>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        bool isPushed = false;
        if(other.gameObject.tag == "attackArea"){
            if(m.isRight && !isPushed){
                enemy.AddForce(new Vector2(10,0), ForceMode2D.Impulse);
                isPushed = true;
            }else if(!m.isRight && !isPushed){
                enemy.AddForce(new Vector2(-10,0), ForceMode2D.Impulse);
                isPushed = true;
            }
            
        }
    }
}
