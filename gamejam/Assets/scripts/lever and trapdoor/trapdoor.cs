using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapdoor : MonoBehaviour
{
    public bool isOpen;
    private float timer = 1.0f;

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        if(timer <= 0){
            gameObject.GetComponent<Animator>().Play("Open");
        }
        if(isOpen && timer > 0){
            timer -= Time.deltaTime;
            gameObject.GetComponent<Animator>().Play("Trapdoor");
        }
        
    }
}
