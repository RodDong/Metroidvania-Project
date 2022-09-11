using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : EnemyBase
{
    [SerializeField]
    GameObject enemyParent;
    [SerializeField]
    float deathDuration;
    [SerializeField]
    GameObject player;
    [SerializeField]
    public bool canDestroy;
    [HideInInspector] public float originHP;
    Color c;
    Renderer spriteRenderer;
    public bool isDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        spriteRenderer = this.gameObject.GetComponent<Renderer>();
        if (spriteRenderer != null)
        {
            c = spriteRenderer.material.color;
        }
        originHP = getHP();
    }
    private void Update()
    {
        ProcessDeath();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "attackArea" && this.gameObject.layer != LayerMask.NameToLayer("projectile"))
        {
            ProcessDamage();
        }
    }

    private void ProcessDamage()
    {
        if (this.getHP() > 0)
        {
            if (spriteRenderer != null)
                spriteRenderer.material.SetColor("_Color", new Color(1f, 0.6f, 0.6f, 1f));
            Invoke("resetColor", 0.15f);
        }
        damage(player.GetComponent<movement>().playerDamage);
    }

    private void ProcessDeath()
    {
        if (this.getHP() <= 0 && !isDead)
        {
            if (this.gameObject.tag != "enemy_mouse" && this.gameObject.tag!="Boss1")
            {
                gameObject.GetComponent<Animator>().SetTrigger("death");
            }
            if (GetComponent<FishManAI>() != null) {
                GetComponent<FishManAI>().enabled = false;
            }
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            Invoke("disableEnemy", deathDuration);
            isDead = true;
        }
    }

    private void disableEnemy()
    {
        if (!canDestroy) {
            enemyParent.gameObject.SetActive(false);
        } else {
            GameObject.Destroy(enemyParent);
        }
    }

    private void resetColor()
    {
        if (spriteRenderer != null)
            spriteRenderer.material.SetColor("_Color", c);
    }
}
