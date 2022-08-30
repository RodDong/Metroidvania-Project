using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] Collider2D spawnDetector;
    [SerializeField] ObjectPool enemyPool;
    [SerializeField] ObjectPool harpoonPool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("triggered " + other.tag);
        if (other.tag == "spawnDetector" && gameObject.transform.Find("enemyOnPlatform") == null) {
            
            Vector3 enemyPos = gameObject.transform.position;
            enemyPos.y += 3;
            GameObject enemy = enemyPool.Spawn(enemyPos, new Quaternion());
            enemy.name = "enemyOnPlatform";
            enemy.GetComponent<FishManAI>().harpoonPool = harpoonPool;
            enemy.transform.parent = gameObject.transform;
        }
    }
}
