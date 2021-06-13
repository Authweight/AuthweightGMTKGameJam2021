using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    private Transform _lightSource;
    private Transform _occluder;
    private SpriteRenderer _render;
    private SpriteRenderer _occlusionRender;
    private float _floorHeight;

    public float _discountRate;

    public virtual void SetReferences(Transform lightsource, Transform occluder)
    {
        _lightSource = lightsource;
        _occluder = occluder;
        _occlusionRender = occluder.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
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

        var transparencyPercent = Mathf.Min(1.0f / scale, 90) * _discountRate;
        _render.color = new Color(0, 0, 0, transparencyPercent);
    }

    public void SetSprite()
    {
        var newSprite = _occlusionRender.sprite;
        _render.sprite = newSprite;
        if (_render.drawMode == SpriteDrawMode.Tiled)
        {
            _render.size = _occlusionRender.size;
        }
    }

    public float ScalingCalculation(float distanceFromLightSource)
    {
        return 1 + distanceFromLightSource * 0.1f;
    }
}
