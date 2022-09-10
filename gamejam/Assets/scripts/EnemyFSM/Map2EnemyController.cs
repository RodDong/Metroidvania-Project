using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2EnemyController : MonoBehaviour
{
    [SerializeField]
    public GameObject enemiesOfScene;
    private List<GameObject> fishmanList = new List<GameObject>();
    private List<int> fishmanHPList = new List<int>();
    private List<Transform> fishmanPosList = new List<Transform>();
    private bool isClear;

    private void Start() {
        for (int i = 0; i < enemiesOfScene.transform.childCount; i++) {
            GameObject enemy = enemiesOfScene.transform.GetChild(i).gameObject;
            if (enemy.GetComponent<EnemyDamage>()) {
                fishmanList.Add(enemy);
                fishmanHPList.Add(enemy.GetComponent<EnemyDamage>().getHP());
                fishmanPosList.Add(enemy.transform);
            }
        }
    }

    private void Update() {
        if (enemiesOfScene.GetComponentsInChildren<EnemyDamage>().GetLength(0) == 0
        && fishmanList.Count != 0
        && fishmanList[0].GetComponent<EnemyDamage>().getHP() <= 0) {
            isClear = true;
            for (int i = 0; i < fishmanList.Count; i++) {
                fishmanList[i].GetComponent<EnemyDamage>().setHP(fishmanHPList[i]);
                fishmanList[i].GetComponent<EnemyDamage>().isDead = false;
                fishmanList[i].GetComponent<Transform>().transform.position = fishmanPosList[i].position;
                fishmanList[i].GetComponent<Transform>().transform.rotation = fishmanPosList[i].rotation;
                fishmanList[i].GetComponent<FishmanBone>().ResetBones();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player") {
            for (int i = 0; i < fishmanList.Count; i++) {
                if (!fishmanList[i].activeSelf) {
                    fishmanList[i].GetComponent<FishManAI>().enabled = true;
                    fishmanList[i].SetActive(true);
                }
            }
        }
    }
}
