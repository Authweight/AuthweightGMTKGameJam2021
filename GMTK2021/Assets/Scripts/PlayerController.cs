using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _speed = 5;
    private float _jumpCooldown = .2f;
    private float _jumpVel = 15;
    private CooldownTimer _jumpTimer;
    private List<ShadowController> _shadows = new List<ShadowController>();

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpTimer = new CooldownTimer(_jumpCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        var onGround = OnGround();
        var currentVelocity = _rb.velocity;
        currentVelocity = ApplyHorizontalAxis(currentVelocity);
        currentVelocity = ApplyJump(currentVelocity, onGround);

        if (onGround)
        {
            currentVelocity = currentVelocity.WithFloorY(0);
        }

        _rb.velocity = currentVelocity;
    }

    private Vector2 ApplyJump(Vector2 currentVelocity, bool onGround)
    {
        if (Input.GetAxis("Fire1") > 0 && onGround && _jumpTimer.CheckTime(Time.time))
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
        _shadows = _shadows.Where(x => x != null).ToList();
        return 
            Physics2D.Raycast(_rb.position, Vector2.down, 0.1f, layerMask: LayerMask.GetMask("Ground"))
            || _shadows.Any(x => x.OnGround());
    }

    public void Hurt()
    {
        Debug.Log("Hurt");
    }

    public void RegisterShadow(ShadowController shadow)
    {
        _shadows.Add(shadow);
    }
}
