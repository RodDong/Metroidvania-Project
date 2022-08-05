using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusVar : MonoBehaviour
{
    
    Animator animation_attack;
    public static bool isCoolDown{get; set;}
    // Start is called before the first frame update
    void Start()
    {
        animation_attack = GameObject.FindGameObjectWithTag("animation_attack").GetComponent<Animator>();
        isCoolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(animation_attack.GetCurrentAnimatorStateInfo(0).IsName("coolDown") || animation_attack.GetCurrentAnimatorStateInfo(0).IsName("coolDown1")){
            isCoolDown = true;
        }else{
            isCoolDown = false;
        }
    }
}
