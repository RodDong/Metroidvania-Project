using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player") {
            player.GetComponent<PlayerStatus>().isDetected = true;
        }
    }
}
