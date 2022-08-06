using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] public float destroyTime = 0.5f;

    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0f) {
            Destroy(gameObject);
        }    
    }
}
