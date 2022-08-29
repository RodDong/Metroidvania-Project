using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixRotation : MonoBehaviour
{
    Quaternion defaultRotation;

    private void Awake() {
        defaultRotation = gameObject.GetComponent<Transform>().rotation;
    }
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.GetComponent<Transform>().rotation= defaultRotation;
    }
}
