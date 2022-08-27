using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionManager : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] TMP_Text potionText;
    public int potionCount;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && potionCount != 0) {
            playerHealth.Recover();
            playerHealth.Invoke("Recover", 0.1f);
            potionCount--;
        }
        potionText.SetText(potionCount.ToString());
    }
}
