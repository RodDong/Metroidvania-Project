using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject prefab;
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    public int defaultCount = 10;
    public int maxCount = 50;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        GameObject obj;
        for (int i = 0; i < defaultCount; i++)
        {
            obj = Instantiate(prefab, this.transform);
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(prefab, position, rotation, this.transform);
        }
        return obj;
    }

    public void Kill(GameObject obj)
    {
        if (objectPool.Count <= maxCount)
        {
            if (!objectPool.Contains(obj))
            {
                objectPool.Enqueue(obj);
            }
            obj.SetActive(false);
        }
        else
        {
            Destroy(obj);
        }
    }

}
