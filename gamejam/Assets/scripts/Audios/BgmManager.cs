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
        if (timer >= 227.7 && backgroundMusic1.time >= 227.7f) {
            backgroundMusic2.time = 80f;
            backgroundMusic2.Play();
        }
        if (timer >= 375.7f && backgroundMusic2.time >= 227.7f) {
            backgroundMusic1.time = 80f;
            backgroundMusic1.Play();
            timer = 0f;
        }
    }
}
