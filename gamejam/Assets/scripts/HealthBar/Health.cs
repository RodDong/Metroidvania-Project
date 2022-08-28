using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] ParticleSystem bloodPS;
    [SerializeField] AudioSource audioSource;
    private AudioClip gettingHitSound;
    private AudioClip deathSound;
    // health bar elements
    public int health;
    public int numHearts;
    public List<GameObject> hearts;
    // cooldown of immunity after being damaged
    public float immunityCooldown;
    public GameObject deathMenu;
    public bool isDead;

    private void Start() {
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, false);
        gettingHitSound = audioSource.GetComponent<PlayerAudio>().hurt;
        deathSound = audioSource.GetComponent<PlayerAudio>().deathMusic;
    }

    private void Update() {
        if (health <= 0 && !isDead) {
            ProcessDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || other.gameObject.layer == LayerMask.NameToLayer("Mouse")) {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile")) {
            TakeDamage();
        }
    }

    void ProcessDeath() {
        isDead = true;
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        audioSource.PlayOneShot(deathSound);
    }

    private void TakeDamage() {
        // update health
        health -= 1;
        bloodPS.Play();
        audioSource.PlayOneShot(gettingHitSound);
        // update hearts
        for (int i = hearts.Count - 1; i >= 0; i--) {
            Animator heartAnimator = hearts[i].GetComponent<Animator>();
            if (heartAnimator.GetCurrentAnimatorStateInfo(0).IsName("soul_full")) {
                heartAnimator.SetTrigger("full_hurt");
                break;
            }
            if (heartAnimator.GetCurrentAnimatorStateInfo(0).IsName("soul_break")) {
                heartAnimator.SetTrigger("break_hurt");
                break;
            }
        }

        // if player died, skip immunity, return to death screen
        if (health == 0) {
            return;
        }

        // give player [x] seconds of immunity and player can pass through enemy
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        int mouseLayer = LayerMask.NameToLayer("Mouse");
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, mouseLayer);
        Invoke("immunityEnd", immunityCooldown);
    }

    public void Recover() {
        health += 1;
        for (int i = 0; i < hearts.Count; i++) {
            Animator heartAnimator = hearts[i].GetComponent<Animator>();
            if (heartAnimator.GetCurrentAnimatorStateInfo(0).IsName("soul_empty")) {
                heartAnimator.SetTrigger("empty_recover");
                break;
            }
            if (heartAnimator.GetCurrentAnimatorStateInfo(0).IsName("soul_break")) {
                heartAnimator.SetTrigger("break_recover");
                break;
            }
        }
    }

    void immunityEnd() {
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        int mouseLayer = LayerMask.NameToLayer("Mouse");
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, false);
        Physics2D.IgnoreLayerCollision(playerLayer, mouseLayer, false);
    }

    public void resetHealth() {
        isDead = false;
        health = 6;
        for (int i = 0; i < hearts.Count; i++) {
            Animator heartAnimator = hearts[i].GetComponent<Animator>();
            heartAnimator.SetTrigger("full_recover");
        }
    }
}
