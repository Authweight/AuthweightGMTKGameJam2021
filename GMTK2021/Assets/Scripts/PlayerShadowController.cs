using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowController : ShadowController
{
    public PlayerAttackController _attack1;
    public PlayerAttackController _attack2;
    public PlayerAttackController _attack3;
    public PlayerAttackController _airAttack;

    private PlayerController _player;
    private bool _onGround;
    private bool _lastOnGround;

    private void FixedUpdate()
    {
        _lastOnGround = _onGround;
        _onGround = CalculateOnGround();
    }

    public override void SetReferences(Transform lightsource, Transform occluder)
    {
        base.SetReferences(lightsource, occluder);
        _player = occluder.GetComponent<PlayerController>();
        _player.RegisterShadow(this);
    }

    internal void Hurt(int damage)
    {
        _player.Hurt(damage);
    }

    public bool OnGround()
    {
        return _lastOnGround || _onGround;
    }

    private bool CalculateOnGround()
    {
        var rightPos = transform.position + transform.right * 0.2f * transform.localScale.x;
        var leftPos = transform.position - transform.right * 0.2f * transform.localScale.x;
        return Physics2D.Raycast(transform.position, Vector2.down, 0.2f, layerMask: LayerMask.GetMask("Shadow Ground"))
            || Physics2D.Raycast(rightPos, Vector2.down, 0.2f, layerMask: LayerMask.GetMask("Shadow Ground"))
            || Physics2D.Raycast(leftPos, Vector2.down, 0.2f, layerMask: LayerMask.GetMask("Shadow Ground"));
    }

    public void BeginAttack1()
    {
        _attack1.Activate();
    }

    public void EndAttack1()
    {
        _attack1.Deactivate();
    }

    public void BeginAttack2()
    {
        _attack2.Activate();
    }

    public void EndAttack2()
    {
        _attack2.Deactivate();
    }

    public void BeginAttack3()
    {
        _attack3.Activate();
    }

    public void EndAttack3()
    {
        _attack3.Deactivate();
    }

    internal void BeginAirAttack()
    {
        _airAttack.Activate();
    }

    internal void EndAirAttack()
    {
        _airAttack.Deactivate();
    }

    private Vector3 LocalRight()
    {
        return (transform.right * transform.localScale.x).normalized;
    }
}
