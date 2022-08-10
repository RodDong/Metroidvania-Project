using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "player") {
            player.GetComponent<Health>().Recover();
        }
    }
}
