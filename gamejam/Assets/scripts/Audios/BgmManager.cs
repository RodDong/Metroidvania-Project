using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic1;
    [SerializeField] AudioSource backgroundMusic2;
    private float timer;
    private void Start() {
        timer = 220f;
        backgroundMusic1.time = 220f;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 227.7 && backgroundMusic1.time >= 227.7f) {
            backgroundMusic2.time = 80f;
            backgroundMusic2.Play();
        }
        if (timer >= 376f && backgroundMusic2.time >= 228f) {
            backgroundMusic1.time = 80f;
            backgroundMusic1.Play();
            timer = 0f;
        }
    }
}
