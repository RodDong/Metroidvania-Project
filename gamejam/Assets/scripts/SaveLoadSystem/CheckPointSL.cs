using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheckPointSL : MonoBehaviour, IDataManager
{
    [SerializeField] bool activateSL = false;
    [SerializeField] GameObject saveMenu;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [HideInInspector] public Vector3 playerPosition;
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
    }

    public void LoadData(GameData data) {
        player.transform.position = data.playerPosition;
        player.GetComponent<movement>().canAttack = data.canAttack;
        attackSaved = data.attackSaved;
        if (GameObject.FindGameObjectWithTag("swordItem")) {
            GameObject.FindGameObjectWithTag("swordItem").SetActive(!data.canAttack);
        }
        if (GameObject.FindGameObjectWithTag("tutorial")) {
            GameObject.FindGameObjectWithTag("tutorial").SetActive(!data.canAttack);
        }
        player.GetComponent<PotionManager>().potionMaxCount = data.potionMaxCount;
        if (data.activeCamera) {
            data.activeCamera.enabled = true;
        }
    }

    public void SaveData(ref GameData data) {
        data.playerPosition = player.GetComponent<movement>().position;
        data.canAttack = player.GetComponent<movement>().canAttack;
        data.attackSaved = attackSaved;
        SpawnEnemy[] rooms = FindObjectsOfType<SpawnEnemy>();
        for (int i = 0; i < rooms.Length; i++) {
            rooms[i].isClear = false;
        }
        data.potionMaxCount = player.GetComponent<PotionManager>().potionMaxCount;
        data.activeCamera = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}