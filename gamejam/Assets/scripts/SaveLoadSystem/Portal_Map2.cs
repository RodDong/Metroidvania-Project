using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Map2 : MonoBehaviour
{
    private bool isOnPortal;
    private void OnTriggerEnter2D(Collider2D other) {
        isOnPortal = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isOnPortal = false;
    }

    private void Update() {
        if (isOnPortal && Input.GetKeyDown(KeyCode.X)) {
            SceneManager.LoadScene("Map3");
        }
    }
}
