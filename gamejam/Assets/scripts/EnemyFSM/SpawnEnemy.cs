using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject player;
    [SerializeField] public GameObject enemiesOfScene;
    [SerializeField] GameObject wall;
    [HideInInspector] public bool isDetected, isClear, isInRoom;
    private EdgeCollider2D[] wallLists;
    private List<GameObject> enemyList = new List<GameObject>();
    private List<int> enemyHPList = new List<int>();
    private List<Transform> enemyPosList = new List<Transform>();
    private void Start() {
        wallLists = wall.GetComponents<EdgeCollider2D>();
        player = GameObject.FindGameObjectWithTag("player");
        for (int i = 0; i < enemiesOfScene.transform.childCount; i++) {
            GameObject enemy = enemiesOfScene.transform.GetChild(i).gameObject;
            enemyList.Add(enemy);
            enemyHPList.Add(enemy.GetComponent<EnemyDamage>().getHP());
            enemyPosList.Add(enemy.transform);
        }
    }
    private void Update() {
        isDetected = player.GetComponent<PlayerStatus>().isDetected;
        // No enabled child object found, reset enemy health and position
        if (enemiesOfScene.GetComponentsInChildren<EnemyDamage>().GetLength(0) == 0 && enemyList[0].GetComponent<EnemyDamage>().getHP() <= 0) {
            isClear = true;
            ResetEnemies();
        }

        wallControl();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player" && !isClear) {
            isInRoom = true;
            ResetEnemies();
            for (int i = 0; i < enemyList.Count; i++) {
                if (!enemyList[i].activeSelf) {
                    enemyList[i].SetActive(true);
                }
            }
        }
    }

    private void wallControl(){
        if(isDetected){
            beingDetected();
        }
        if(isClear && isInRoom){
            clearArea();
        }
    }

    public void beingDetected() {
        for (int i = 0; i < wallLists.Length; i++) {
            wallLists[i].isTrigger = false;
        }
    }

    public void clearArea() {
        player.GetComponent<PlayerStatus>().isDetected = false;
        isInRoom = false;
        for (int i = 0; i < wallLists.Length; i++) {
            wallLists[i].isTrigger = true;
        }
    }

    public void ResetEnemies() {
        for (int i = 0; i < enemyList.Count; i++) {
            if (enemyList[i].GetComponent<melee_gator>() != null) {
                enemyList[i].GetComponent<melee_gator>().enabled = true;
            }
            if (enemyList[i].GetComponent<Gator>() != null) {
                enemyList[i].GetComponent<Gator>().enabled = true;
            }
            if (enemyList[i].GetComponent<Toad>() != null){
                foreach(GameObject meleeGator in enemyList[i].GetComponent<Toad>().bossSummonGators.meleeGators){
                    meleeGator.GetComponent<EnemyDamage>().setHP(30);
                    meleeGator.GetComponent<EnemyDamage>().isDead = false;
                }
                foreach(GameObject ranger in enemyList[i].GetComponent<Toad>().bossSummonGators.rangerGators){
                    ranger.GetComponent<EnemyDamage>().setHP(25);
                    ranger.GetComponent<EnemyDamage>().isDead = false;
                }
                enemyList[i].GetComponent<Toad>().bossSummonGators.DisableAll();
                enemyList[i].GetComponent<Toad>().stateMachine.SetCurrentState(ToadIdle.Instance);
                enemyList[i].GetComponent<Toad>().ToadBone.ResetBones();
                enemyList[i].transform.localPosition = new Vector3(128.52f, -6.29f, 1);
            }
            
            enemyList[i].SetActive(false);
            enemyList[i].GetComponent<EnemyDamage>().setHP(enemyHPList[i]);
            enemyList[i].GetComponent<EnemyDamage>().isDead = false;
            // reset enemy position
            enemyList[i].GetComponent<Transform>().transform.position = enemyPosList[i].position;
            enemyList[i].GetComponent<Transform>().transform.rotation = enemyPosList[i].rotation;
            // reset enemy detection
            if (enemyList[i].GetComponent<MeleeEnemyDetection>() != null) {
                enemyList[i].GetComponent<MeleeEnemyDetection>().hasTarget = false;
            }
        }
    }
}
