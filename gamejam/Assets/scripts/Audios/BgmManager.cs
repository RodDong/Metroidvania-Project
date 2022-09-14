using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField] public AudioSource backgroundMusic1;
    [SerializeField] public AudioSource backgroundMusic2;
    private float timer;
    private void Start() {
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 195.7f && backgroundMusic1.time >= 195.7f) {
            backgroundMusic1.Stop();
            backgroundMusic1.time = 0f;
            backgroundMusic2.time = 48f;
            backgroundMusic2.Play();
        }
        if (timer >= 343.4f && backgroundMusic2.time >= 195.7f) {
            backgroundMusic1.time = 48f;
            backgroundMusic1.Play();
            timer = 0f;
        }
    }
}
