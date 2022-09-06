using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentPlatformPos : MonoBehaviour
{
    [HideInInspector] public Vector2 curPlatformPos = new Vector2(0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(curPlatformPos);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Platform")){
            curPlatformPos.x = other.transform.position.x;
            curPlatformPos.y = other.transform.position.y+1.0f;
        }
    }
}
