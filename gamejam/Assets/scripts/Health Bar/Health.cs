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
        // update health
        health -= 1;
        // update hearts
        for (int i = 0; i < hearts.Length; i++) {
            if (hearts[i].sprite.name == "soul_break") {
                hearts[i].sprite = emptyHeart;
                break;
            }
            if (hearts[i].sprite.name == "soul_full") {
                hearts[i].sprite = halfHeart;
                break;
            }
        }

        // if player died, skip immunity, return to death screen
        if (health == 0) {}

        // give player [x] seconds of immunity and player can pass through enemy
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer);
        Invoke("immunityEnd", immunityCooldown);
    }

    public void Recover() {
        for (int i = hearts.Length - 1; i >= 0; i--) {
            if (hearts[i].sprite.name == "soul_empty") {
                hearts[i].sprite = halfHeart;
                break;
            }
            if (hearts[i].sprite.name == "soul_break") {
                hearts[i].sprite = fullHeart;
                break;
            }
        }
    }

    void immunityEnd() {
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, false);
    }
}
