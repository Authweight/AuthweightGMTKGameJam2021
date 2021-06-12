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

    private Rigidbody2D _rb;

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
    }

    // Update is called once per frame
    void Update()
    {
        SetSprite();
        SetTransform();
    }

    public void SetTransform()
    {
        var pointingVector = _occluder.position - _lightSource.position;
        var scale = ScalingCalculation(pointingVector.magnitude);
        transform.localScale = _occluder.transform.localScale * scale;
        transform.position = _occluder.position + pointingVector / 2;

        var transparencyPercent = Mathf.Min(1.5f / scale, 90);
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
}
