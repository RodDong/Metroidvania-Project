using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class GameData
{
    // player parameters to be saved
    public Vector3 playerPosition;
    public bool canAttack;
    public bool attackSaved;
    public int potionMaxCount;
    public CinemachineVirtualCamera activeCamera;

    public GameData() {
        playerPosition = Vector3.zero;
        canAttack = false;
        attackSaved = false;
        potionMaxCount = 1;
        activeCamera = null;
    }
}
