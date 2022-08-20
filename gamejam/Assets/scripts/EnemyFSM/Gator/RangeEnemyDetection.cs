using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyDetection : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    public Vector3 position;
    float offset = 1f;
    public bool isRight;
    public bool hasTarget;
    public bool isFacingRight => Mathf.Abs(transform.eulerAngles.y) < 90;
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

        if (hasTarget) {
            if(Mathf.Abs(enemy.transform.position.x - position.x) >= offset){
                rotateRelativeToPlayer();
            }
        
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if((other.tag == "player" ||  other.tag == "attackArea") && other.GetComponent<movement>().makeSound){
            position = other.transform.position;
            hasTarget = true;
            Invoke("loseTarget",2f);
        }

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
