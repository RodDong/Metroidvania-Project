using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour, IDataManager
{
    public GameObject deathMenu;
    [SerializeField] GameObject player;
    [SerializeField] GameObject wall;
    private EdgeCollider2D[] wallLists;

    private void Start() {
        wallLists = wall.GetComponents<EdgeCollider2D>();
    }
    public void Resume() {
        // reset player health
        player.GetComponent<Health>().resetHealth();

        // reset wall status
        for (int i = 0; i < wallLists.Length; i++) {
            wallLists[i].isTrigger = true;
        }

        // reset player isdetected to false
        player.GetComponent<PlayerStatus>().isDetected = false;

        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        DataManager.instance.LoadGame();
    }

    public void Quit() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadData(GameData data) {
        GameObject.FindGameObjectWithTag("player").transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data) {
        data.playerPosition = GameObject.FindGameObjectWithTag("player").GetComponent<movement>().position;
    }
}
