using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] public float destroyTime = 1f;

    private float countDown;

    private void Start()
    {
        countDown = destroyTime;
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f)
        {
            countDown = destroyTime;
            ObjectPool.Instance.Kill(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player" || other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            countDown = destroyTime;
            ObjectPool.Instance.Kill(this.gameObject);
        }
    }
}
