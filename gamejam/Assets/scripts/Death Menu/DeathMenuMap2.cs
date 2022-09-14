using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DeathMenuMap2 : MonoBehaviour, IDataManager
{
    public GameObject deathMenu;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] BgmManager bgmManager;
    private Map2EnemyController[] enemySpawnControllers;
    private BossController bosscontroller;

    private void Start() {
        enemySpawnControllers = GameObject.FindObjectsOfType<Map2EnemyController>();
        bosscontroller = GameObject.FindObjectOfType<BossController>();
    }
    public void Resume() {
        // change blend mode to cut
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        // reset player health
        player.GetComponent<Health>().resetHealth();

        // resume music
        // bgmManager.backgroundMusic1.time = 0f;
        // bgmManager.backgroundMusic2.time = 0f;
        // bgmManager.backgroundMusic1.Play();

        // reset player isdetected to false
        player.GetComponent<PlayerStatus>().isDetected = false;

        foreach (Map2EnemyController enemySpawner in enemySpawnControllers) {
            // To reset isClear after player died, uncomment this:
            // enemySpawner.isClear = false;
            enemySpawner.ResetEnemies();
        }

        bosscontroller.ResetBoss();

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
