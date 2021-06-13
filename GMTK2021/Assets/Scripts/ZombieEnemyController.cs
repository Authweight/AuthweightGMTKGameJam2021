using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemyController : EnemyController
{
    public GameObject LungeHitBox;

    private float _lurchSpeed = -8;
    private Animator _anim;

    private CooldownTimer _untilNextLurch = new CooldownTimer(2.5f);

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BumpingIntoWall())
        {
            this.transform.localScale = this.transform.localScale.ReverseX();
            _rb.velocity = _rb.velocity.ReverseX();
        }

        if (_untilNextLurch.CheckTime(Time.time))
        {
            _anim.SetTrigger("Lurch");
            _untilNextLurch.StartCooldown(Time.time);
        }
    }

    public void BeginLunge()
    {
        _rb.velocity = _rb.velocity.WithX(_lurchSpeed * transform.localScale.x);
        LungeHitBox.GetComponent<HitBoxController>().enabled = true;
    }

    public void EndLunge()
    {
        _rb.velocity = _rb.velocity.WithX(0);
        LungeHitBox.GetComponent<HitBoxController>().enabled = false;
    }
}
