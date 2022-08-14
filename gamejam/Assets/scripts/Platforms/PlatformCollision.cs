using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerOneWayPlatform>().isDropping && 
        player.transform.position.y - player.transform.localScale.y / 2 >
        gameObject.transform.position.y) {
            gameObject.layer = LayerMask.NameToLayer("Platform");
        } else {
            gameObject.layer = LayerMask.NameToLayer("PlatformWithoutPlayerCollision");
        }
    }
}
