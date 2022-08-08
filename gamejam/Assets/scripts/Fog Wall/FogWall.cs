using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private void Update() {
        // TODO: disable used by effector in battle

        if (player.GetComponent<movement>().isRight) {
            gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 270;
        } else {
            gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 90;
        }
    }
}
