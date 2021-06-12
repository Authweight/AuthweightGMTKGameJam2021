using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    private Transform _lightSource;
    private Transform _occluder;
    private SpriteRenderer _render;
    private SpriteRenderer _occlusionRender;
    private float _floorHeight = -2.78f;

    private Rigidbody2D _rb;

    public PlayerAttackController _attack1;
    public PlayerAttackController _attack2;
    public PlayerAttackController _attack3;

    public void SetReferences(Transform lightsource, Transform occluder)
    {
        _lightSource = lightsource;
        _occluder = occluder;
        _occlusionRender = occluder.GetComponent<SpriteRenderer>();
        var player = occluder.GetComponent<PlayerController>();
        if (player != null)
        {
            player.RegisterShadow(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _render = GetComponent<SpriteRenderer>();
        var floor = Physics2D.Raycast(transform.position, Vector2.down, 100.0f, LayerMask.GetMask("Ground"));
        _floorHeight = floor.point.y;
    }

    // Update is called once per frame
    void Update()
    {
        SetSprite();
        SetTransform();
    }

    public void SetTransform()
    {
        var pointingVector = _occluder.position - _lightSource.position.WithY(_floorHeight);
        var scale = ScalingCalculation(pointingVector.magnitude);
        transform.localScale = _occluder.transform.localScale * scale;
        transform.position = _occluder.position + pointingVector / 2;

        var transparencyPercent = Mathf.Min(1.0f / scale, 90);
        _render.color = new Color(0, 0, 0, transparencyPercent);
    }

    public void SetSprite()
    {
        var newSprite = _occlusionRender.sprite;
        _render.sprite = newSprite;        
    }

    public float ScalingCalculation(float distanceFromLightSource)
    {
        return 1 + distanceFromLightSource * 0.1f;
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
