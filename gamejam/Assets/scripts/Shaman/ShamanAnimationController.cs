using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShamanAnimationController : MonoBehaviour
{
    [SerializeField] GameObject shaman;
    [SerializeField] waterWheel waterwheel;
    [SerializeField] Animator shamanVFX;
    [SerializeField] AudioSource backgroundMusicController;
    [SerializeField] AudioSource bossMusicController;
    [SerializeField] AudioSource shamanVFXSound;
    private bool hasPlayed;
    private Animator shamanAnimator;
    private Animator CMAnimator;
    private GameObject player;
    private bool isPlaying;
    private bool hasEnd;
    private void Start() {
        shamanAnimator = shaman.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player");
    }
    void Update()
    {
        if (waterwheel.speed != 0) {
            if (!hasPlayed) {
                StartAnimation();
                hasPlayed = true;
            }
            if (shamanAnimator.GetCurrentAnimatorStateInfo(0).IsName("RaiseArm")
            && shamanAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && !isPlaying) {
                backgroundMusicController.time = 0.0f;
                backgroundMusicController.Stop();
                shamanVFX.Play("start");
                shamanVFXSound.Play();
                isPlaying = true;
            }

            if (shamanVFX.GetCurrentAnimatorStateInfo(0).IsName("start")
            && shamanVFX.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && !hasEnd) {
                hasEnd = true;
                shamanAnimator.Play("PutDown");
                Invoke("endAnimation", 0.67f);
                
            }
        }
    }

    void StartAnimation() {
        player.GetComponent<movement>().enabled = false;
        shaman.GetComponent<shamanStateMachine>().enabled = false;
        shamanAnimator.Play("RaiseArm");
    }

    void endAnimation() {
        bossMusicController.Play();
        player.GetComponent<movement>().enabled = true;
        shaman.GetComponent<shamanStateMachine>().enabled = true;
        enabled = false;
    }
}
