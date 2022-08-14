using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    Vector3 offset = new Vector3(0f, 0f, -10f);
    float smoothTime = 0.5f;
    float verticalPanDistance = 10f;
    Vector3 velocity = Vector3.zero;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalOffset = 0;
        if (Input.GetKey(KeyCode.W)) {
            verticalOffset = verticalPanDistance;
        } 
        offset.y = verticalOffset;
        Vector3 pos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);


    }
}
