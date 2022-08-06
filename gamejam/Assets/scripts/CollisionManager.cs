using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    void Start()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        int platformLayer = LayerMask.NameToLayer("Platform");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(enemyLayer, projectileLayer);
        Physics2D.IgnoreLayerCollision(projectileLayer, platformLayer);
    }
}