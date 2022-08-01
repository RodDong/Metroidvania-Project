using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    Vector3 offset = new Vector3(0f, 0f, -10f);
    float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
    }
}
