using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var killable = collision.GetComponent<KillableController>();
        if (killable != null)
        {
            killable.Hit();
        }
    }

    public void Activate()
    {
        _collider.enabled = true;
    }

    internal void Deactivate()
    {
        _collider.enabled = false;
    }
}
