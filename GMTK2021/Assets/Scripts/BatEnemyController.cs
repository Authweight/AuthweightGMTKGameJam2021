using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemyController : EnemyController
{
    private Rigidbody2D _rb;
    private float speed = 8;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector2.left * speed;
    }
}
