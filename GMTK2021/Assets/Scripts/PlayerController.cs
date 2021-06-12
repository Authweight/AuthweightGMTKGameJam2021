using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject _healthUi;
    public PlayerAttackController _attack1;
    public PlayerAttackController _attack2;
    public PlayerAttackController _attack3;
    public GameObject BloodSpatter;

    private Rigidbody2D _rb;
    private float _speed = 5;
    private float _jumpCooldown = .2f;
    private float _attackCooldown = 1.0f;
    private float _jumpVel = 15;
    private CooldownTimer _jumpTimer;
    private CooldownTimer _attackTimer;
    private List<PlayerShadowController> _shadows = new List<PlayerShadowController>();
    private Animator _anim;
    private bool _onGround;
    private const int _maxHealth = 20;
    private int _currentHealth = _maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpTimer = new CooldownTimer(_jumpCooldown);
        _attackTimer = new CooldownTimer(_attackCooldown);
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _onGround = OnGround();
        if (_onGround)
        {
            _rb.velocity = _rb.velocity.WithFloorY(0);
            _anim.SetBool("In Air", false);
        }
        else
        {
            _anim.SetBool("In Air", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var currentVelocity = _rb.velocity;
        ApplyAttack();
        currentVelocity = ApplyHorizontalAxis(currentVelocity);
        currentVelocity = ApplyJump(currentVelocity);

        _rb.velocity = currentVelocity;
    }

    private Vector2 ApplyJump(Vector2 currentVelocity)
    {
        if (Input.GetButtonDown("Fire1") && _onGround && _jumpTimer.CheckTime(Time.time))
        {
            currentVelocity = currentVelocity.ApplyY(_jumpVel);
            _jumpTimer.StartCooldown(Time.time);
            _anim.SetTrigger("Jump");
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
        if (_attackTimer.CheckTime(Time.time) && Input.GetButtonDown("Fire2"))
        {
            _anim.SetTrigger("Attack");
        }
    }

    bool OnGround()
    {
        _shadows = _shadows.Where(x => x != null).ToList();
        return 
            Physics2D.Raycast(_rb.position, Vector2.down, 0.1f, layerMask: LayerMask.GetMask("Ground", "Platform"))
            || _shadows.Any(x => x.OnGround());
    }

    public void Hurt(int damage)
    {
        BloodSpatter.GetComponent<ParticleSystem>().Play();
        _currentHealth -= damage;
        _healthUi.GetComponent<Text>().text = $"Health: {_currentHealth}";

        if (_currentHealth <= 0)
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    public void RegisterShadow(PlayerShadowController shadow)
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
        _attackTimer.StartCooldown(Time.time);
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
