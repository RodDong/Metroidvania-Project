using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSL : MonoBehaviour, IDataManager
{
    [SerializeField] bool activateSL = false;
    [SerializeField] GameObject saveMenu;
    [SerializeField] float loadHeight;
    public Vector3 playerPosition;
    private bool onFire;
    private bool attackSaved;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "player") {
            onFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.transform.tag == "player") {
            onFire = false;
        }
    }

    void Update()
    {
        playerPosition = gameObject.transform.position;
        if (onFire && Input.GetKeyDown(KeyCode.X) && activateSL) {
            saveMenu.GetComponent<Animator>().SetTrigger("start");
            DataManager.instance.SaveGame();
        }
        if (GameObject.FindGameObjectWithTag("player").GetComponent<movement>().canAttack && !attackSaved) {
            DataManager.instance.SaveGame();
            attackSaved = true;
        }
    }

    public void LoadData(GameData data) {
        GameObject.FindGameObjectWithTag("player").transform.position = data.playerPosition;
        GameObject.FindGameObjectWithTag("player").GetComponent<movement>().canAttack = data.canAttack;
        attackSaved = data.attackSaved;
        if (GameObject.FindGameObjectWithTag("swordItem")) {
            GameObject.FindGameObjectWithTag("swordItem").SetActive(!data.canAttack);
        }
        if (GameObject.FindGameObjectWithTag("tutorial")) {
            GameObject.FindGameObjectWithTag("tutorial").SetActive(!data.canAttack);
        }
    }

    public void SaveData(ref GameData data) {
        Vector3 temp = GameObject.FindGameObjectWithTag("player").GetComponent<movement>().position;
        data.playerPosition = new Vector3(temp.x, loadHeight, temp.y);
        data.canAttack = GameObject.FindGameObjectWithTag("player").GetComponent<movement>().canAttack;
        data.attackSaved = attackSaved;
        SpawnEnemy[] rooms = FindObjectsOfType<SpawnEnemy>();
        for (int i = 0; i < rooms.Length; i++) {
            rooms[i].isClear = false;
        }
    }
}
