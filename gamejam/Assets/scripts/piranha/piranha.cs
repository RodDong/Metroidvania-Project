using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranha : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float force;
    private Quaternion upward = Quaternion.Euler(0, 0, 0);
    private Quaternion downward = Quaternion.Euler(180, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= startPos.y && gameObject.GetComponent<Rigidbody2D>().velocity.y<0){
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*force, ForceMode2D.Impulse);
            gameObject.transform.rotation = upward;
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0 && gameObject.transform.rotation.eulerAngles.x == 0){
            gameObject.transform.rotation = downward;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "attackArea") {
            transform.position = startPos;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
