using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranha : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y <= startPos.y && gameObject.GetComponent<Rigidbody2D>().velocity.y<0){
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*force, ForceMode2D.Impulse);
        }

        if(gameObject.GetComponent<Rigidbody2D>().velocity.y>0){
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }else{
            gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
        
    }

    
}
