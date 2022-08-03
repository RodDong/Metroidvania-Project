using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "enemy_mouse") {
            TakeDamage();
        }
    }

    void TakeDamage() {
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
    }
}
