using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] GameObject wall;
    public bool isDetected;
    private EdgeCollider2D[] wallLists;

    private void Start() {
        wallLists = wall.GetComponents<EdgeCollider2D>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            clearArea();
        }
    }

    public void beingDetected() {
        isDetected = true;
        for (int i = 0; i < wallLists.Length; i++) {
            wallLists[i].isTrigger = false;
        }
    }

    public void clearArea() {
        isDetected = false;
        for (int i = 0; i < wallLists.Length; i++) {
            wallLists[i].isTrigger = true;
        }
    }
}
