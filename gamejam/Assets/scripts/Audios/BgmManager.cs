using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField] public AudioSource backgroundMusic1;
    [SerializeField] public AudioSource backgroundMusic2;
    public float timer;
    private bool isPlaying1;
    private bool isPlaying2;
    private void Start() {
        isPlaying1 = true;
        timer = backgroundMusic1.time;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 195.5f && isPlaying1) {
            backgroundMusic2.time = 48f;
            backgroundMusic2.Play();
            isPlaying1 = false;
            isPlaying2 = true;
        }
        if (timer >= 343f && isPlaying2) {
            backgroundMusic1.time = 48f;
            backgroundMusic1.Play();
            timer = 48f;
            isPlaying1 = true;
            isPlaying2 = false;
        }
    }
}
