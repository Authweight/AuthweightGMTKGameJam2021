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

    public override void SetReferences(Transform lightsource, Transform occluder)
    {
        base.SetReferences(lightsource, occluder);
        var player = occluder.GetComponent<PlayerController>();
        if (player != null)
        {
            player.RegisterShadow(this);
        }
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
