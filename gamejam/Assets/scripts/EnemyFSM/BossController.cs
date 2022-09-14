using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    public GameObject enemiesOfScene;
    private List<GameObject> bossList = new List<GameObject>();
    private List<int> bossHPList = new List<int>();
    private List<Transform> bossPosList = new List<Transform>();
    public bool isClear;

    private void Start() {
        for (int i = 0; i < enemiesOfScene.transform.childCount; i++) {
            GameObject enemy = enemiesOfScene.transform.GetChild(i).gameObject;
            if (enemy.GetComponent<EnemyDamage>()) {
                bossList.Add(enemy);
                bossHPList.Add(enemy.GetComponent<EnemyDamage>().getHP());
                bossPosList.Add(enemy.transform);
            }
        }
    }

    private void Update() {
        if (enemiesOfScene.GetComponentsInChildren<EnemyDamage>().GetLength(0) == 0
        && bossList.Count != 0
        && bossList[0].GetComponent<EnemyDamage>().getHP() <= 0) {
            isClear = true;
            ResetEnemies();
            for (int i = 0; i < bossList.Count; i++) {
                if (bossList[i].GetComponent<shamanStateMachine>() != null){
                    bossList[i].GetComponent<shamanStateMachine>().curState = new shamanIdle();
                    bossList[i].GetComponent<shamanStateMachine>().shamanBone.ResetBones();
                    bossList[i].SetActive(false);
                }
                bossList[i].GetComponent<FishmanBone>().ResetBones();
            }
        }
    }

    public void ResetEnemies() {
        for (int i = 0; i < bossList.Count; i++) {
                bossList[i].GetComponent<EnemyDamage>().setHP(bossHPList[i]);
                bossList[i].GetComponent<EnemyDamage>().isDead = false;
                bossList[i].GetComponent<Transform>().transform.position = bossPosList[i].position;
                bossList[i].GetComponent<Transform>().transform.rotation = bossPosList[i].rotation;
        }
    }
}
