using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] SpawnEnemy roomStatus;
    [SerializeField] PlatformEffector2D effector;
    void Update()
    {
        if (roomStatus.enemiesOfScene.GetComponentsInChildren<EnemyDamage>().GetLength(0) == 0) {
            if (Input.GetKeyDown(KeyCode.S)) {
                effector.rotationalOffset = 180f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            effector.rotationalOffset = 0f;
        }
    }
}
