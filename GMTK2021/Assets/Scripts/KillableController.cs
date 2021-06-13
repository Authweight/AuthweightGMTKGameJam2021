using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableController : MonoBehaviour
{
    public int MaxHealth;
    public GameObject HitParticles;
    
    private bool _dying;
    private CooldownTimer _deathTimer;
    private Rigidbody2D _rb;
    public ArenaController _arena;


    private int _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaxHealth;
        _deathTimer = new CooldownTimer(.5f);
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Die();
        }

        if (_dying && _deathTimer.CheckTime(Time.time))
        {
            Destroy(this.gameObject);
        }
    }

    public void Hit(int damage)
    {
        HitParticles.GetComponent<ParticleSystem>().Play();
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        DisableAllColliders();

        GetComponent<SpriteRenderer>().enabled = false;

        _dying = true;
        _deathTimer.StartCooldown(Time.time);
        if (_arena != null)
        {
            _arena.NotifyDeath();
        }
    }

    public void DisableAllColliders()
    {
        var colliderCount = _rb.attachedColliderCount;
        var colliders = new Collider2D[colliderCount];
        _rb.GetAttachedColliders(colliders);

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void RegisterArena(ArenaController arena)
    {
        _arena = arena;
    }
}
