using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivatorController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.enabled = true;
        }
    }
}
