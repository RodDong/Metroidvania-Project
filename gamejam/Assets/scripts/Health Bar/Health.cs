using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // health bar elements
    public int health;
    public int numHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    // cooldown of immunity after being damaged
    public float immunityCooldown;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Projectile")) {
            TakeDamage();
        }
    }

    void TakeDamage() {
        // update hearts
        for (int i = 0; i < hearts.Length; i++) {
            if (hearts[i].sprite.name == "heart_half") {
                hearts[i].sprite = emptyHeart;
                break;
            }
            if (hearts[i].sprite.name == "heart_full") {
                hearts[i].sprite = halfHeart;
                break;
            }
        }

        // give player [x] seconds of immunity and player can pass through enemy
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer);
        Invoke("immunityEnd", immunityCooldown);
    }

    void immunityEnd() {
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, false);
    }
}
