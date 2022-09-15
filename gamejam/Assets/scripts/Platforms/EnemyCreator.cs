using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] Collider2D spawnDetector;
    [SerializeField] ObjectPool enemyPool;
    [SerializeField] ObjectPool harpoonPool;
    [SerializeField] bool willDropdown;
    [SerializeField] GameObject bossRoom;
    [SerializeField] GameObject platformParent;
    [SerializeField] Collider2D trapDoor;
    [SerializeField] Collider2D trapDoorChild;
    [SerializeField] Collider2D enemyGate;
    private GameObject enemySpawned;
    private List<Collider2D> movingPlatforms = new List<Collider2D>();

    private void Start() {
        foreach (Transform child in platformParent.transform) {
            if (child.tag == "OneWayPlatform") {
                movingPlatforms.Add(child.GetComponent<Collider2D>());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "spawnDetector" && gameObject.transform.Find("enemyOnPlatform") == null && enemySpawned == null) {
            Vector3 enemyPos = gameObject.transform.position;
            enemyPos.y += 3;
            enemySpawned = enemyPool.Spawn(enemyPos, new Quaternion());
            enemySpawned.name = "enemyOnPlatform";
            enemySpawned.GetComponent<FishManAI>().harpoonPool = harpoonPool;
            enemySpawned.GetComponent<FishManAI>().enabled = false;
            enemySpawned.GetComponent<EnemyDamage>().canDestroy = true;
            enemySpawned.transform.parent = gameObject.transform;
            Physics2D.IgnoreCollision(enemyGate, enemySpawned.GetComponent<CapsuleCollider2D>());
        }
        if (enemySpawned && willDropdown && other.tag == "dropdownDetector") {
            enemySpawned.GetComponent<FishManAI>().enabled = true;
            enemySpawned.GetComponent<FishManAI>().roamDistance = 100f;
            enemySpawned.transform.parent = bossRoom.transform;
            for (int i = 0; i < movingPlatforms.Count; i++) {
                Physics2D.IgnoreCollision(movingPlatforms[i], enemySpawned.GetComponent<CapsuleCollider2D>());
            }
            Physics2D.IgnoreCollision(enemyGate, enemySpawned.GetComponent<CapsuleCollider2D>(), false);
        }
        if (enemySpawned && !willDropdown && other.tag == "trapDoorDetector") {
            Physics2D.IgnoreCollision(enemySpawned.GetComponent<CapsuleCollider2D>(), trapDoor);
            Physics2D.IgnoreCollision(enemySpawned.GetComponent<CapsuleCollider2D>(), trapDoorChild);
        }
        if (enemySpawned && !willDropdown && other.tag == "destroyDetector") {
            GameObject.Destroy(enemySpawned);
        }
        if (enemySpawned && other.name == "BossRoom_confiner") {
            if (!willDropdown) {
                enemySpawned.GetComponent<FishManAI>().roamDistance = 0f;
            }
            enemySpawned.GetComponent<FishManAI>().enabled = true;
        }
    }
}
