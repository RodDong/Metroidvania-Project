using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallList : MonoBehaviour
{
    private EdgeCollider2D[] wallLists;
    public List<float> wallPosLists;
    // Start is called before the first frame update
    void Start()
    {
        wallLists = this.GetComponents<EdgeCollider2D>();
        for (int i = 0; i < wallLists.Length; i++) {
            wallPosLists.Add(wallLists[i].bounds.center.x);
        }
    }
}
