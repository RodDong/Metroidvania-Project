using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject enemiesOfScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D() {
        if (!enemiesOfScene.activeSelf) {
            enemiesOfScene.SetActive(true);
        }
    }
}
