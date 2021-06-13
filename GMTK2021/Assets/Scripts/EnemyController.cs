using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D _rb;

    private void Start()
    {

    }

    protected bool BumpingIntoWall()
    {
        var origin = (Vector2)transform.position + (_rb.velocity.normalized * 0.8f);
        return Physics2D.Raycast(origin, _rb.velocity.normalized, 0.5f, LayerMask.GetMask("Platform", "Ground"));
    }
}
