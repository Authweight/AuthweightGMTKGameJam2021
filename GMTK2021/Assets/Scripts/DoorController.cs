using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D[] _colliders;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        var colliderCount = _rb.attachedColliderCount;
        _colliders = new Collider2D[colliderCount];
        _rb.GetAttachedColliders(_colliders);

        Open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Shut()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        EnableAllColliders();
    }

    internal void Open()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        DisableAllColliders();
    }

    private void DisableAllColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }

    private void EnableAllColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
    }
}
