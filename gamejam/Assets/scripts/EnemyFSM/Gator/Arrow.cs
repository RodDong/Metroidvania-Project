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

    private void OnCollisionEnter2D(Collision2D other)
    {
        countDown = destroyTime;
        ObjectPool.Instance.Kill(this.gameObject);
    }
}
