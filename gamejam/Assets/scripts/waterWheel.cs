using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterWheel : MonoBehaviour
{
    public Vector3 angle;
    [SerializeField] public float speed;
    void Start()
    {
        angle = new Vector3(0, 0, 0);
        
    }

    void Update()
    {
        if (speed != 0) {
            angle += new Vector3(0, 0, speed);
            gameObject.transform.eulerAngles = angle;
        }
    }
}
