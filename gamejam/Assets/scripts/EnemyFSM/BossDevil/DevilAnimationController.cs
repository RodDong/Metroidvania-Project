using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DevilAnimationController : MonoBehaviour
{
    private bool hasPlayed;
    private bool isPlaying;
    private GameObject player;
    [SerializeField] CinemachineVirtualCamera bossCM;
    [SerializeField] GameObject devil;
    [SerializeField] Animator devilAnimator;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] Animator endingAnimator;
    [SerializeField] GameObject quitButton;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void Update() {
        if (!devil) {
            Invoke("PlayEnding", 3.0f);
        }
        if (endingAnimator.GetCurrentAnimatorStateInfo(0).IsName("ending")
        && endingAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
            quitButton.SetActive(true);
        }
    }

    private void PlayEnding() {
        endingAnimator.Play("ending");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!hasPlayed && other.tag == "player") {
            ProcessAnimation();
            hasPlayed = true;
        } else if(other.tag == "player" && !isPlaying && devil != null) {
            if (!backgroundMusic.isPlaying) backgroundMusic.Play();
            devil.SetActive(true);
        }
    }

    private void ProcessAnimation() {
        isPlaying = true;
        bossCM.Follow = null;
        bossCM.GetComponent<Animator>().Play("CamMove");
        devilAnimator.Play("intro");
        Invoke("EndAnimation", 4.5f);
    }

    private void EndAnimation() {
        isPlaying = false;
        bossCM.Follow = player.transform;
        backgroundMusic.Play();
        devil.transform.parent = null;
        devil.transform.eulerAngles = new Vector3(0, 0, 0);
        Destroy(devilAnimator.gameObject);
        devil.GetComponent<SpriteRenderer>().enabled = true;
        devil.GetComponent<BoxCollider2D>().enabled = true;
        devil.GetComponent<Devil>().enabled = true;
        devil.GetComponent<EnemyDamage>().enabled = true;
    }
}
