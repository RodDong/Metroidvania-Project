using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheckPointSL : MonoBehaviour, IDataManager
{
    [SerializeField] bool activateSL = false;
    [SerializeField] GameObject saveMenu;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [HideInInspector] public Vector3 playerPosition;
    private bool onFire;
    private bool attackSaved;
    private Health playerHealth;
    private PotionManager playerPotion;

    private void Start() {
        playerHealth = player.GetComponent<Health>();
        playerPotion = player.GetComponent<PotionManager>();
    }

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
        if (onFire && Input.GetKeyDown(KeyCode.X) && activateSL) {
            saveMenu.GetComponent<Animator>().SetTrigger("start");

            playerHealth.health = playerHealth.maxhealth;
            for (int i = 0; i < playerHealth.hearts.Count; i++) {
                Animator heartAnimator = playerHealth.hearts[i].GetComponent<Animator>();
                heartAnimator.Play("soul_full");
            }

            playerPotion.potionCount = playerPotion.potionMaxCount;

            DataManager.instance.SaveGame();
        }

        if (!attackSaved && player.GetComponent<movement>().canAttack) {
            player.GetComponent<movement>().position = new Vector3(-10.32f, -5.26f, 0);
            attackSaved = true;
            DataManager.instance.SaveGame();
        }
    }

    public void LoadData(GameData data) {
        player.transform.position = data.playerPosition;
        player.GetComponent<movement>().canAttack = data.canAttack;
        attackSaved = data.attackSaved;
        if (GameObject.FindGameObjectWithTag("swordItem")) {
            GameObject.FindGameObjectWithTag("swordItem").SetActive(!data.canAttack);
        }
        if (GameObject.FindGameObjectWithTag("tutorial")) {
            GameObject.FindGameObjectWithTag("tutorial").SetActive(!data.canAttack);
        }
        player.GetComponent<PotionManager>().potionMaxCount = data.potionMaxCount;
        player.GetComponent<PotionManager>().potionCount = data.potionMaxCount;
        if (data.activeCamera) {
            data.activeCamera.enabled = true;
        }
        Invoke("ResetCam", 1f);
    }

    public void SaveData(ref GameData data) {
        data.playerPosition = player.GetComponent<movement>().position;
        data.canAttack = player.GetComponent<movement>().canAttack;
        data.attackSaved = attackSaved;
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