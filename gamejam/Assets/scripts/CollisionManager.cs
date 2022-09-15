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
        int mouseLayer = LayerMask.NameToLayer("Mouse");
        int invisibleWallLayer = LayerMask.NameToLayer("InvisibleWall");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        Physics2D.IgnoreLayerCollision(projectileLayer, playerLayer, false);

        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(enemyLayer, projectileLayer);
        Physics2D.IgnoreLayerCollision(enemyLayer, mouseLayer);
        Physics2D.IgnoreLayerCollision(projectileLayer, platformLayer);
        Physics2D.IgnoreLayerCollision(projectileLayer, noCollisionPlatformLayer);
        Physics2D.IgnoreLayerCollision(attackLayer, detectLayer);
        Physics2D.IgnoreLayerCollision(attackLayer, projectileLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, noCollisionPlatformLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, attackLayer);
        Physics2D.IgnoreLayerCollision(noCollisionPlatformLayer, noCollisionPlatformLayer);
        Physics2D.IgnoreLayerCollision(noCollisionPlatformLayer, platformLayer);
        Physics2D.IgnoreLayerCollision(platformLayer, platformLayer);
        //Physics2D.IgnoreLayerCollision(invisibleWallLayer, enemyLayer);
    }
}
