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
        boss.transform.localPosition = new Vector3(3.75f, 23.9f, 1f);
        boss.transform.rotation = bossPos.rotation;
        boss.GetComponent<shamanStateMachine>().shamanMusic.Stop();
        boss.GetComponent<shamanStateMachine>().curState = new shamanIdle();
        boss.GetComponent<shamanStateMachine>().shamanBone.ResetBones();
        boss.SetActive(false);
    }
}
