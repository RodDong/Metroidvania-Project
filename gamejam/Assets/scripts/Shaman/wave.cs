using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{
    Vector3 tempPos = new Vector3(0,0,0);
    Vector3 tempScale = new Vector3(0,0,0);
    void Update()
    {
        float temp = gameObject.GetComponent<Renderer>().bounds.size.y;
        transform.localScale /= 1.005f;
        tempPos = transform.position;
        tempPos.y -= (temp - gameObject.GetComponent<Renderer>().bounds.size.y)/2;
        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(LayerMask.LayerToName(other.gameObject.layer) == "InvisibleWall" ){
            Debug.Log("destroy");
            ObjectPool.Instance.Kill(this.gameObject);
        }
    }
}
