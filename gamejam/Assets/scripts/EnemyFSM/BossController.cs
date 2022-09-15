using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject boss;
    private int bossHP;
    private Transform bossPos;
    public bool isClear;

    private void Start() {
        bossHP = boss.GetComponent<EnemyDamage>().getHP();
        bossPos = boss.transform;
    }

    public void ResetBoss() {
        boss.GetComponent<EnemyDamage>().setHP(bossHP);
        boss.GetComponent<EnemyDamage>().isDead = false;
        boss.GetComponent<Transform>().transform.position = bossPos.position;
        boss.GetComponent<Transform>().transform.rotation = bossPos.rotation;
        boss.GetComponent<shamanStateMachine>().shamanMusic.Stop();
        boss.SetActive(false);
    }
}
