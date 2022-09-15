using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DeathMenuFinalBoss : MonoBehaviour, IDataManager
{
    public GameObject deathMenu;
    GameObject player;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] AudioSource backgroundMusic1;
    [SerializeField] AudioSource deathMusic;
    private SpawnEnemy[] enemySpawnControllers;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("player");
        enemySpawnControllers = GameObject.FindObjectsOfType<SpawnEnemy>();
    }

    private void Update() {
        if (player.GetComponent<Health>().health <= 0) {
            if (backgroundMusic1.isPlaying) {
                backgroundMusic1.Stop();
            }
        }
    }

    public void Resume() {
        // change blend mode to cut
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        // reset player health
        player.GetComponent<Health>().resetHealth();

        // resume music
        if (deathMusic.isPlaying) {
            deathMusic.Stop();
        }

        // reset player isdetected to false
        player.GetComponent<PlayerStatus>().isDetected = false;

        foreach (SpawnEnemy enemySpawner in enemySpawnControllers) {
            // To reset isClear after player died, uncomment this:
            // enemySpawner.isClear = false;
            enemySpawner.ResetEnemies();
        }

        deathMenu.SetActive(false);
        DataManager.instance.LoadGame();
        Time.timeScale = 1f;

        // change blend mode back to ease in out
        Invoke("ResetCam", 1f);
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

    private void ResetCam() {
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
    }
}
