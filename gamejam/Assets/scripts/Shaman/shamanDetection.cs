using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shamanDetection : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject enemy;
    public bool hasTarget;
    bool isRight;
    public Vector3 position;
    private movement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        hasTarget = false;
        player = GameObject.FindGameObjectWithTag("player");
        playerMovement = player.GetComponent<movement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget) {
            player.GetComponent<PlayerStatus>().isDetected = true;
        }
        isRight = (position.x - enemy.transform.position.x) > 0;
        if(playerMovement.makeSound && Mathf.Abs(player.transform.position.x - enemy.transform.position.x)<10.0f){
            position = player.transform.position;
            hasTarget = true;
            Invoke("loseTarget",0.5f);
        }
    }

    void loseTarget()
    {
        hasTarget = false;
    }
}
