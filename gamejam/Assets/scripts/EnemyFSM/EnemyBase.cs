using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private int HP;

    public int getHP() {
        return HP;
    }

    public void damage(int damage) {
        HP -= damage;
    }
}
