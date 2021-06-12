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


    private int _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaxHealth;
        _deathTimer = new CooldownTimer(.5f);
    }

    // Update is called once per frame
    void Update()
    {
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
        _dying = true;
        _deathTimer.StartCooldown(Time.time);        
    }
}
