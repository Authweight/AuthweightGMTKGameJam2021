using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemyController : EnemyController
{
    private Rigidbody2D _rb;
    private float _lurchSpeed = 8;

    private CooldownTimer _untilNextLurch = new CooldownTimer(2.5f);
    private CooldownTimer _lurchTime = new CooldownTimer(0.3f);

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_untilNextLurch.CheckTime(Time.time))
        {
            _rb.velocity = _rb.velocity.WithX(_lurchSpeed * transform.localScale.x);
            _untilNextLurch.StartCooldown(Time.time);
            _lurchTime.StartCooldown(Time.time);
        }

        if (_lurchTime.CheckTime(Time.time))
        {
            _rb.velocity = _rb.velocity.WithX(0);
        }
    }
}
