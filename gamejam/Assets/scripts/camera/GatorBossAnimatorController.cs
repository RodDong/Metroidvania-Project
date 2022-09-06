using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GatorBossAnimatorController : MonoBehaviour
{
    [SerializeField] SpawnEnemy enemyManager;
    [SerializeField] CinemachineVirtualCamera bossCM;
    [SerializeField] GameObject bossToad;
    [SerializeField] GameObject player;
    [SerializeField] BgmManager backgroundMusicController;
    [SerializeField] AudioSource bossMusicController;
    private bool hasWatched;
    private bool start;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player" && !enemyManager.isClear && !hasWatched) {
            backgroundMusicController.backgroundMusic1.time = 0f;
            backgroundMusicController.backgroundMusic1.Stop();
            backgroundMusicController.backgroundMusic2.time = 0f;
            backgroundMusicController.backgroundMusic2.Stop();
            ProcessAnimation();
            hasWatched = true;
        }
    }

    void ProcessAnimation() {
        bossCM.GetComponent<Animator>().Play("map1bossCM");
        bossToad.GetComponent<Toad>().enabled = false;
        player.GetComponent<movement>().enabled = false;
        bossToad.transform.eulerAngles = new Vector3(0, 180, 0);
        Invoke("ProcessBossAnimation", 3f);
    }

    void ProcessBossAnimation() {
        // disable boss AI
        // flip boss
        // then play roar
        // then switch camera to follow player (could be done in animation?)
        bossToad.transform.eulerAngles = new Vector3(0, 0, 0);
        Invoke("BossRoarAnimation", 1f);
    }

    void BossRoarAnimation() {
        Animator bossAnimator = bossToad.GetComponent<Animator>();
        bossToad.GetComponent<Animator>().Play("Roar");
        bossToad.GetComponent<AudioSource>().Play();
        Invoke("EndBossAnimation", 1.5f);
    }

    void EndBossAnimation() {
        bossCM.Follow = player.transform;
        bossToad.GetComponent<Toad>().enabled = true;
        player.GetComponent<movement>().enabled = true;
        bossMusicController.time = 0f;
        bossMusicController.Play();
        enabled = false;
    }
}
