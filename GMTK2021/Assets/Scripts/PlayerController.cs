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
    private Animator _anim;

    public PlayerAttackController _attack1;
    public PlayerAttackController _attack2;
    public PlayerAttackController _attack3;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpTimer = new CooldownTimer(_jumpCooldown);
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var onGround = OnGround();
        var currentVelocity = _rb.velocity;
        ApplyAttack();
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
        if (Input.GetButtonDown("Fire1") && onGround && _jumpTimer.CheckTime(Time.time))
        {
            currentVelocity = currentVelocity.ApplyY(_jumpVel);
            _jumpTimer.StartCooldown(Time.time);
            _anim.SetTrigger("Jump");
        }
        else if (onGround)
        {
            _anim.SetBool("In Air", false);
        }
        else
        {
            _anim.SetBool("In Air", true);
        }

        return currentVelocity;
    }

    private Vector2 ApplyHorizontalAxis(Vector2 currentVelocity)
    {
        var input = Input.GetAxis("Horizontal");
        if (input < 0)
        {
            _anim.SetBool("Walking", true);
            transform.localScale = transform.localScale.WithX(-1);
        }
        else if (input > 0)
        {
            _anim.SetBool("Walking", true);
            transform.localScale = transform.localScale.WithX(1);
        }
        else
        {
            _anim.SetBool("Walking", false);
        }
        
        currentVelocity = currentVelocity.WithX(input * _speed);
        
        return currentVelocity;
    }

    private void ApplyAttack()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _anim.SetTrigger("Attack");
        }
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

    public void BeginAttack1()
    {
        _attack1.Activate();
        foreach(var shadow in _shadows)
        {
            if (shadow != null)
            {
                shadow.BeginAttack1();
            }
        }
    }

    public void EndAttack1()
    {
        _attack1.Deactivate();
        foreach (var shadow in _shadows)
        {
            if (shadow != null)
            {
                shadow.EndAttack1();
            }
        }
    }

    public void BeginAttack2()
    {
        _attack2.Activate();
        foreach (var shadow in _shadows)
        {
            if (shadow != null)
            {
                shadow.BeginAttack2();
            }
        }
    }

    public void EndAttack2()
    {
        _attack2.Deactivate();
        foreach (var shadow in _shadows)
        {
            if (shadow != null)
            {
                shadow.EndAttack2();
            }
        }
    }

    public void BeginAttack3()
    {
        _attack3.Activate();
        foreach (var shadow in _shadows)
        {
            if (shadow != null)
            {
                shadow.BeginAttack3();
            }
        }
    }

    public void EndAttack3()
    {
        _attack3.Deactivate();
        foreach (var shadow in _shadows)
        {
            if (shadow != null)
            {
                shadow.EndAttack3();
            }
        }
    }
}
