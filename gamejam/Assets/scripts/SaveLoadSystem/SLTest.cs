using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLTest : MonoBehaviour, IDataManager
{
    [SerializeField] bool activateSL = false;
    public Vector3 playerPosition;
    void Update()
    {
        playerPosition = gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.S) && activateSL) {
            DataManager.instance.SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L) && activateSL) {
            DataManager.instance.LoadGame();
        }
    }

    public void LoadData(GameData data) {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data) {
        data.playerPosition = this.playerPosition;
    }
    
}
