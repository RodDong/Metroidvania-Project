using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal2 : MonoBehaviour, IDataManager
{
    private bool isOnPortal;
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isOnPortal = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isOnPortal = false;
    }

    private void Update() {
        if (isOnPortal && Input.GetKeyDown(KeyCode.X)) {
            player.GetComponent<movement>().position = new Vector3(-16.58f, -7.01f, 0);
            player.GetComponent<PotionManager>().potionMaxCount = 4;
            DataManager.instance.SaveGame();
            SceneManager.LoadScene("Map3");
        }
    }

    public void LoadData(GameData data) {}

    public void SaveData(ref GameData data) {
        data.potionMaxCount = 4;
    }
}
