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

    private Rigidbody2D _rb;

    public void SetReferences(Transform lightsource, Transform occluder)
    {
        _lightSource = lightsource;
        _occluder = occluder;
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
        SetTransform(_lightSource.position, _occluder.position);
    }

    public void SetTransform(Vector2 lightSource, Vector2 occluder)
    {
        var pointingVector = occluder - lightSource;
        var scale = ScalingCalculation(pointingVector.magnitude);
        transform.localScale = Vector3.one * scale;
        transform.position = occluder + (pointingVector - (Vector2.down * (scale / 2))) / 2;

        var transparencyPercent = Mathf.Min(1.5f / scale, 90);
        Debug.Log(transparencyPercent);
        _render.color = _render.color.WithTransparency(transparencyPercent);
    }

    public float ScalingCalculation(float distanceFromLightSource)
    {
        return 1 + distanceFromLightSource * 0.1f;
    }
}
