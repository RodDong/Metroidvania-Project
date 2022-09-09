using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2EnemyController : MonoBehaviour
{
    [SerializeField]
    public GameObject enemiesOfScene;
    private List<GameObject> enemyList = new List<GameObject>();
    private List<int> enemyHPList = new List<int>();
    private List<Transform> enemyPosList = new List<Transform>();
    private bool isClear;

    private void Start() {
        for (int i = 0; i < enemiesOfScene.transform.childCount; i++) {
            GameObject enemy = enemiesOfScene.transform.GetChild(i).gameObject;
            if (enemy.GetComponent<EnemyDamage>()) {
                enemyList.Add(enemy);
                enemyHPList.Add(enemy.GetComponent<EnemyDamage>().getHP());
                enemyPosList.Add(enemy.transform);
            }
        }
    }

    private void Update() {
        if (enemiesOfScene.GetComponentsInChildren<EnemyDamage>().GetLength(0) == 0 && enemyList[0].GetComponent<EnemyDamage>().getHP() <= 0) {
            isClear = true;
            for (int i = 0; i < enemyList.Count; i++) {
                enemyList[i].GetComponent<EnemyDamage>().setHP(enemyHPList[i]);
                enemyList[i].GetComponent<Transform>().transform.position = enemyPosList[i].position;
                enemyList[i].GetComponent<Transform>().transform.rotation = enemyPosList[i].rotation;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player") {
            for (int i = 0; i < enemyList.Count; i++) {
                if (!enemyList[i].activeSelf) {
                    enemyList[i].SetActive(true);
                }
            }
        }
    }
}
