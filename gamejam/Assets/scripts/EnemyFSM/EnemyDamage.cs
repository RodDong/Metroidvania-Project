using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : EnemyBase
{
    [SerializeField]
    float deathDuration;
    [SerializeField]
    GameObject player;
    private void Update() {
        
        ProcessDeath();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "attackArea") {
            ProcessDamage();
        }
    }
    
    private void ProcessDamage() {
        damage(FindObjectOfType<movement>().playerDamage);
    }

    private void ProcessDeath() {
        if (getHP() <= 0) {
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
}
