using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _speed = 5;
    private float _heightFromGround = .6f;
    private float _jumpCooldown = .2f;
    private float _jumpVel = 13;
    private CooldownTimer _jumpTimer;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpTimer = new CooldownTimer(_jumpCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        var currentVelocity = _rb.velocity;
        currentVelocity = ApplyHorizontalAxis(currentVelocity);
        currentVelocity = ApplyJump(currentVelocity);

        _rb.velocity = currentVelocity;
    }

    private Vector2 ApplyJump(Vector2 currentVelocity)
    {
        if (Input.GetAxis("Fire1") > 0 && OnGround() && _jumpTimer.CheckTime(Time.time))
        {
            currentVelocity = currentVelocity.ApplyY(_jumpVel);
            _jumpTimer.StartCooldown(Time.time);
        }

        return currentVelocity;
    }

    private Vector2 ApplyHorizontalAxis(Vector2 currentVelocity)
    {
        var input = Input.GetAxis("Horizontal");
        if (input < 0)
        {
            transform.localScale = transform.localScale.WithX(-1);
        }

        if (input > 0)
        {
            transform.localScale = transform.localScale.WithX(1);
        }
        
        currentVelocity = currentVelocity.WithX(input * _speed);
        
        return currentVelocity;
    }

    bool OnGround()
    {
        return Physics2D.Raycast(_rb.position, Vector2.down, _heightFromGround, layerMask: LayerMask.GetMask("Ground"));
    }
}
