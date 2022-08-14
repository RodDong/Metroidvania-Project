using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
            for (int i = 0; i < colliders.Length; i++) {
                Debug.Log(colliders[i].bounds.center.x);
            }
        }
    }
}
