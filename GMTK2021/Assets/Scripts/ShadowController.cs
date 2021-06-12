using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    private Transform _lightSource;
    private Transform _occluder;

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
    }

    public float ScalingCalculation(float distanceFromLightSource)
    {
        return 1 + distanceFromLightSource * 0.1f;
    }
}
