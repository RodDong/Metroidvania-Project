using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLMap3 : MonoBehaviour, IDataManager
{
    [SerializeField] GameObject player;
    public void LoadData(GameData data) {
        player.GetComponent<PotionManager>().potionMaxCount = data.potionMaxCount;
        player.GetComponent<PotionManager>().potionCount = data.potionMaxCount;
    }

    public void SaveData(ref GameData data) {}
}
