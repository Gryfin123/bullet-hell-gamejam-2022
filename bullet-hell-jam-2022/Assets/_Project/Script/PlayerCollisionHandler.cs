using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;
using BulletFury.Data;

public class PlayerCollisionHandler : MonoBehaviour
{
    public void OnEnemyBulletCollision(BulletContainer container, BulletCollider collider)
    {
        Debug.Log("I was hit!");
    }
}
