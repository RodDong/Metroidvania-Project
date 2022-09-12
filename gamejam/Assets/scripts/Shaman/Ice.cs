using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset() {
        transform.position = initialPos;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("destroy");
        reset();
        gameObject.SetActive(false);
    }
}
