using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public bool canAttack;
    public bool attackSaved;

    public GameData() {
        playerPosition = Vector3.zero;
        canAttack = false;
        attackSaved = false;
    }
}
