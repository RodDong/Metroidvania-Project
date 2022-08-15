using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSL : MonoBehaviour, IDataManager
{
    [SerializeField] bool activateSL = false;
    [SerializeField] GameObject saveMenu;
    public Vector3 playerPosition;
    private bool onFire;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "player") {
            onFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.transform.tag == "player") {
            onFire = false;
        }
    }

    void Update()
    {
        playerPosition = gameObject.transform.position;
        if (Input.GetKeyDown(KeyCode.L) && activateSL) {
            DataManager.instance.LoadGame();
        }
        if (onFire && Input.GetKeyDown(KeyCode.E) && activateSL) {
            saveMenu.GetComponent<Animator>().SetTrigger("start");
            DataManager.instance.SaveGame();
        }
    }

    public void LoadData(GameData data) {
        GameObject.FindGameObjectWithTag("player").transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data) {
        data.playerPosition = GameObject.FindGameObjectWithTag("player").GetComponent<movement>().position;
    }
}
