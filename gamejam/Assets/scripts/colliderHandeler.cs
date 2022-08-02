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
        if(other.gameObject.tag == "attackArea"){
            //Debug.Log(m.isPushed);
            if(m.isRight && m.isPushed){
                
                enemy.AddForce(new Vector2(5,0), ForceMode2D.Impulse);
                m.isPushed = false;
            }else if(!m.isRight && m.isPushed){
                Debug.Log("Push left");
                enemy.AddForce(new Vector2(-5,0), ForceMode2D.Impulse);
                m.isPushed = false;
            }
            
        }
    }
}
