using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thorn : MonoBehaviour
{
    float spawnGap = 0.05f;
    float duration = 1.0f;
    int index = 0;
    List<Transform> thorns = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform){  
            thorns.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(duration < -1){
            gameObject.SetActive(false);
        }
        if(spawnGap<=0){
            spawnGap = 0.1f;
            if(thorns[index].gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.25f && duration>=0){
                thorns[index].gameObject.SetActive(true);
            }else{
                thorns[index].gameObject.SetActive(false);
            }
            index = index < thorns.Count-1 ? index+1 : 0;
        }
        spawnGap-=Time.deltaTime;
        duration-=Time.deltaTime;

    }

    public void ResetAnimation(){
        index = 0;
        spawnGap = 0.1f;
        duration = 1.0f;
    }

}
