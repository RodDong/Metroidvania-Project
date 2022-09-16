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
    [SerializeField] AudioSource backgroundMusicController;
    [SerializeField] AudioSource bossMusicController;
    [SerializeField] AudioSource deathMusic;
    [SerializeField] GameObject waterWheel;
    [SerializeField] GameObject iceplatform1, iceplatform2;
    private Map2EnemyController[] enemySpawnControllers;
    private BossController bosscontroller;

    private void Start() {
        enemySpawnControllers = GameObject.FindObjectsOfType<Map2EnemyController>();
        bosscontroller = GameObject.FindObjectOfType<BossController>();
    }

    private void Update() {
        if (player.GetComponent<Health>().health <= 0) {
            if (bossMusicController.isPlaying) {
                bossMusicController.Stop();
            }
            if (backgroundMusicController.isPlaying) {
                backgroundMusicController.Stop();
            }
            waterWheel.GetComponent<waterWheel>().speed = 0;
            waterWheel.transform.eulerAngles = new Vector3(0, 0, 0);
            GameObject[] fishmanArray = GameObject.FindGameObjectsWithTag("fishman");
            foreach (var fishman in fishmanArray) {
                fishman.GetComponent<EnemyDamage>().disableEnemy();
            }
        }
    }

    public void Resume() {
        // change blend mode to cut
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        // reset player health
        player.GetComponent<Health>().resetHealth();
        player.GetComponent<PotionManager>().potionCount = player.GetComponent<PotionManager>().potionMaxCount;
        // resume music
        if (deathMusic.isPlaying) {
            deathMusic.Stop();
        }
        backgroundMusicController.time = 0f;
        backgroundMusicController.Play();

        // reset player isdetected to false
        player.GetComponent<PlayerStatus>().isDetected = false;

        foreach (Map2EnemyController enemySpawner in enemySpawnControllers) {
            // To reset isClear after player died, uncomment this:
            // enemySpawner.isClear = false;
            iceplatform1.SetActive(false);
            iceplatform2.SetActive(false);
            enemySpawner.ResetEnemies();
        }
        bosscontroller.ResetBoss();

        waterWheel.GetComponent<waterWheel>().angle = Vector3.zero;

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
