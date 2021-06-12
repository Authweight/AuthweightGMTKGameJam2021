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

    private PlayerController _player;

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
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layerMask: LayerMask.GetMask("Shadow Ground"));
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
}
