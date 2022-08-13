using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] GameObject wall;
    public bool isDetected;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            clearArea();
        }
    }

    public void beingDetected() {
        isDetected = true;
        wall.SetActive(true);
    }

    public void clearArea() {
        isDetected = false;
        wall.SetActive(false);
    }
}
