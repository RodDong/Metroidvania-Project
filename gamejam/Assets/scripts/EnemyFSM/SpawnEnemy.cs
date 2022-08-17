using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject enemiesOfScene;
    [SerializeField] GameObject wall;
    public bool isDetected, isClear, isInRoom;
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
        Debug.Log(enemiesOfScene.GetComponentsInChildren<EnemyDamage>().GetLength(0) + " " + isInRoom + " " + isDetected);
        // No enabled child object found, reset enemy health and position
        if (enemiesOfScene.GetComponentsInChildren<EnemyDamage>().GetLength(0) == 0) {
            isClear = true;
            for (int i = 0; i < enemyList.Count; i++) {
                if(enemyList[i].GetComponent<melee_gator>()!=null){
                    enemyList[i].GetComponent<melee_gator>().enabled = true;
                }
                enemyList[i].GetComponent<EnemyDamage>().setHP(enemyHPList[i]);
                enemyList[i].GetComponent<Transform>().transform.position = enemyPosList[i].position;
                enemyList[i].GetComponent<Transform>().transform.rotation = enemyPosList[i].rotation;
            }
        }else{
            isClear = false;
        }

        //后门
        if(Input.GetKeyDown(KeyCode.Q)) {
            clearArea();
        }

        wallControl();

    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        // TODO: reload enemy status
        if (other.tag == "player") {
            isInRoom = true;
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
}
