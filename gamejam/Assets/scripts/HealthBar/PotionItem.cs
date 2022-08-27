using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    [SerializeField] PotionManager playerPotion;
    [SerializeField] int potionNum;
    private void Start() {
        if (potionNum <= playerPotion.potionMaxCount) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player") {
            playerPotion.potionMaxCount += 1;
            playerPotion.potionCount += 1;
            Destroy(this.gameObject);
        }
    }
}
