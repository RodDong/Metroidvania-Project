using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTransit : MonoBehaviour
{
    [SerializeField]
    GameObject currentCamera;
    [SerializeField]
    GameObject nextCamera;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player") {
            currentCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "player") {
            currentCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
    }
}
