using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{
    Vector3 tempPos = new Vector3(0,0,0);
    Vector3 tempScale = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localScale/=0.01f;
        tempPos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(LayerMask.LayerToName(other.gameObject.layer) == "InvisibleWall" ){
            Debug.Log("destroy");
            ObjectPool.Instance.Kill(this.gameObject);
        }
    }
}
