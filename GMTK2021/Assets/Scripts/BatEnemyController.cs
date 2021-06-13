using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemyController : EnemyController
{
    public Vector2 FlightVector;
    private float speed = 8;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BumpingIntoWall())
        {
            FlightVector = Quaternion.Euler(0, 0, 90) * -FlightVector;            
        }

        _rb.velocity = FlightVector.normalized * speed;
        if (_rb.velocity.x > 0)
        {
            transform.localScale = transform.localScale.WithX(-Mathf.Abs(transform.localScale.x));
        }
        else
        {
            transform.localScale = transform.localScale.WithX(Mathf.Abs(transform.localScale.x));
        }
    }
}
