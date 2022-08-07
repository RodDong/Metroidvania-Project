using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : EnemyBase
{
    private void Update() {
        
        ProcessDeath();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "attackArea") {
            ProcessDamage();
        }
    }
    
    private void ProcessDamage() {
        Debug.Log(this.getHP());
        damage(FindObjectOfType<movement>().playerDamage);
    }

    private void ProcessDeath() {
        if (getHP() <= 0) {
            Destroy(this.gameObject);
        }
    }
}
