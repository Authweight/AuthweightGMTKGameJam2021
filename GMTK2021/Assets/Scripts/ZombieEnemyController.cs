using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemyController : EnemyController
{
    public GameObject LungeHitBox;

    private Rigidbody2D _rb;
    private float _lurchSpeed = 8;
    private Animator _anim;

    private CooldownTimer _untilNextLurch = new CooldownTimer(2.5f);

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_untilNextLurch.CheckTime(Time.time))
        {
            _anim.SetTrigger("Lurch");
        }
    }

    public void BeginLunge()
    {
        _rb.velocity = _rb.velocity.WithX(_lurchSpeed * transform.localScale.x);
        _untilNextLurch.StartCooldown(Time.time);
        LungeHitBox.GetComponent<HitBoxController>().enabled = true;
    }

    public void EndLunge()
    {
        _rb.velocity = _rb.velocity.WithX(0);
        LungeHitBox.GetComponent<HitBoxController>().enabled = false;
    }
}
