using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrailVFX : MonoBehaviour
{
    ParticleSystem PS;

    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dustPos = player.transform.position;
        dustPos.y -= player.GetComponent<Collider2D>().bounds.size.y / 2;
        gameObject.transform.position = dustPos;
        if (!player.GetComponent<movement>().canJump || player.GetComponent<Rigidbody2D>().velocity == Vector2.zero) {
            PS.Stop();
        } else if (player.GetComponent<movement>() && PS.isStopped) {
            PS.Play();
        }
    }
}