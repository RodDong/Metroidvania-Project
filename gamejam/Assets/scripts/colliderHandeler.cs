using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderHandeler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "attackArea"){
            Debug.Log("hit");
        }
    }
}
