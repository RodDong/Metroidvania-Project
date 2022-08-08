using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    public Vector3 position;
    bool isRight;
    public bool hasTarget;
    void Start()
    {
        isRight = true;
        hasTarget = false;
    }

    void Update()
    {
        if (position.x - enemy.transform.position.x > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        rotateRelativeToPlayer();
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "player" ||  other.tag == "attackArea" && other.GetComponent<movement>().makeSound){
            position = other.transform.position;
            hasTarget = true;
            Invoke("loseTarget",1f);
        }
        Debug.Log(hasTarget);
    }

    void rotateRelativeToPlayer()
    {
        if (isRight)
        {
            enemy.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            enemy.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void loseTarget(){
        hasTarget = false;
    }

}
