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
    [SerializeField] Collider2D trapDoor;
    [SerializeField] Collider2D trapDoorChild;
    private GameObject enemySpawned;

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
        }
        if (enemySpawned && willDropdown && other.tag == "dropdownDetector") {
            enemySpawned.GetComponent<FishManAI>().enabled = true;
            enemySpawned.transform.parent = bossRoom.transform;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), enemySpawned.GetComponent<CapsuleCollider2D>());
        }
        if (enemySpawned && !willDropdown && other.tag == "trapDoorDetector") {
            Physics2D.IgnoreCollision(enemySpawned.GetComponent<CapsuleCollider2D>(), trapDoor);
            Physics2D.IgnoreCollision(enemySpawned.GetComponent<CapsuleCollider2D>(), trapDoorChild);
        }
        if (enemySpawned && !willDropdown && other.tag == "destroyDetector") {
            GameObject.Destroy(enemySpawned);
        }
    }
}
