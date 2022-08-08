using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : EnemyBase
{
    [SerializeField]
    float deathDuration;
    [SerializeField]
    GameObject player;
    Color c;

    private void Start() {
        c = this.gameObject.GetComponent<Renderer>().material.color;
    }
    private void Update() { 
        ProcessDeath();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "attackArea") {
            ProcessDamage();
        }
    }
    
    private void ProcessDamage() {
        if(this.getHP() > 0){
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(1f,0.6f,0.6f,1f));
            Invoke("resetColor", 0.15f);
        }
        damage(FindObjectOfType<movement>().playerDamage);
    }

    private void ProcessDeath() {
        if (this.getHP() <= 0) {
            if (this.gameObject.tag != "enemy_mouse") {
                gameObject.GetComponent<Animator>().SetTrigger("death");
            }
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            Invoke("destroyEnemy", deathDuration);
        }
    }

    private void destroyEnemy(){
        Destroy(this.gameObject);
    }

    private void resetColor(){
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
    }

    
}
