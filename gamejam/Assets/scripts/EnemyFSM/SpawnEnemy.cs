using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemiesOfScene;
    private List<GameObject> enemyList = new List<GameObject>();
    private List<int> enemyHPList = new List<int>();
    private List<Vector3> enemyPosList = new List<Vector3>();
    private void Start() {
        for (int i = 0; i < enemiesOfScene.transform.childCount; i++) {
            GameObject enemy = enemiesOfScene.transform.GetChild(i).gameObject;
            enemyList.Add(enemy);
            enemyHPList.Add(enemy.GetComponent<EnemyDamage>().getHP());
            enemyPosList.Add(enemy.transform.position);
        }
    }
    private void Update() {
        // No enabled child object found, reset enemy health and position
        if (enemiesOfScene.GetComponentsInChildren<melee_gator>().GetLength(0) == 0) {
            for (int i = 0; i < enemyList.Count; i++) {
                enemyList[i].GetComponent<EnemyDamage>().setHP(enemyHPList[i]);
                enemyList[i].GetComponent<Transform>().position = enemyPosList[i];
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // TODO: reload enemy status
        if (other.tag == "player") {
            for (int i = 0; i < enemyList.Count; i++) {
                if (!enemyList[i].activeSelf) {
                    enemyList[i].SetActive(true);
                }
            }
        }
    }
}
