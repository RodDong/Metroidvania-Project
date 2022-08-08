using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    void Start()
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        int platformLayer = LayerMask.NameToLayer("Platform");
        int noCollisionPlatformLayer = LayerMask.NameToLayer("PlatformWithoutPlayerCollision");
        int attackLayer = LayerMask.NameToLayer("AttackArea");
        int detectLayer = LayerMask.NameToLayer("DetectArea");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(enemyLayer, projectileLayer);
        Physics2D.IgnoreLayerCollision(projectileLayer, platformLayer);
        Physics2D.IgnoreLayerCollision(attackLayer, detectLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, noCollisionPlatformLayer);
    }
}
