using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmuku : MonoBehaviour
{
    private GameObject target;
    private float speed = 10.0f;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Invoke("destroy", 3.0f);
    }

    private void Update() {
        changeDirection();
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void changeDirection() {
        float xDiff = transform.position.x - target.transform.position.x;
        float yDiff = transform.position.y - target.transform.position.y;
        float degrees = Mathf.Atan2(yDiff, xDiff) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, degrees);
    }

    private void destroy() {
        Destroy(this.gameObject);
    }
}
