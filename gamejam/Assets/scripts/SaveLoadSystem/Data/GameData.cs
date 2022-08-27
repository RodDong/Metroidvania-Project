using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // player parameters to be saved
    public Vector3 playerPosition;
    public bool canAttack;
    public bool attackSaved;
    public int potionCount;

    public GameData() {
        playerPosition = Vector3.zero;
        canAttack = false;
        attackSaved = false;
        potionCount = 1;
    }
}
