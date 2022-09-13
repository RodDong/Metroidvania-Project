using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class lever : MonoBehaviour, IDataManager
{
    [SerializeField] bool activateSL = false;
    [SerializeField] GameObject saveMenu;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [HideInInspector] public Vector3 playerPosition;
    private Health playerHealth;
    private PotionManager playerPotion;
    [SerializeField] public GameObject player;
    [SerializeField] GameObject trapdoor;
    [SerializeField] Map2EnemyController roomStatus;
    private bool inRange;
    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (inRange && Input.GetKeyDown(KeyCode.X)) {
            saveMenu.GetComponent<Animator>().SetTrigger("start");

            DataManager.instance.SaveGame();

            timer = 0.2f;
            gameObject.GetComponent<Animator>().Play("lever");
            trapdoor.GetComponent<trapdoor>().isOpen = true;
        }
        if (timer <= 0) {
            gameObject.GetComponent<Animator>().Play("Idle");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player" && roomStatus.isClear) {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "player" && roomStatus.isClear) {
            inRange = false;
        }   
    }

    public void LoadData(GameData data) {
        player.transform.position = data.playerPosition;
        player.GetComponent<movement>().canAttack = data.canAttack;
        if (GameObject.FindGameObjectWithTag("swordItem")) {
            GameObject.FindGameObjectWithTag("swordItem").SetActive(!data.canAttack);
        }
        if (GameObject.FindGameObjectWithTag("tutorial")) {
            GameObject.FindGameObjectWithTag("tutorial").SetActive(!data.canAttack);
        }
        player.GetComponent<PotionManager>().potionMaxCount = data.potionMaxCount;
        if (data.activeCamera) {
            data.activeCamera.enabled = true;
        }
        Invoke("ResetCam", 1f);
    }

    public void SaveData(ref GameData data) {
        data.playerPosition = player.GetComponent<movement>().position;
        data.canAttack = player.GetComponent<movement>().canAttack;
        SpawnEnemy[] rooms = FindObjectsOfType<SpawnEnemy>();
        for (int i = 0; i < rooms.Length; i++) {
            rooms[i].isClear = false;
        }
        data.potionMaxCount = player.GetComponent<PotionManager>().potionMaxCount;
        data.activeCamera = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void ResetCam() {
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
    }
}
