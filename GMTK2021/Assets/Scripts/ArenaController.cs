using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public DoorController[] Doors;
    public GameObject[] Waves;

    bool _active = false;
    private int _waveIndex = 0;
    private int _killableCount = 0;
    private int _deadCount = 0;
    private GameObject _currentWave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_active && _deadCount >= _killableCount)
        {
            if (_waveIndex < Waves.Length)
            {
                SpawnWave();
            }
            else
            {
                foreach(var door in Doors)
                {
                    door.Open();
                }
            }
        }
    }

    void SpawnWave()
    {
        var toSpawn = Waves[_waveIndex++];
        Destroy(_currentWave);

        _currentWave = Instantiate(toSpawn, transform.position, transform.rotation);

        var killables = _currentWave.GetComponentsInChildren<KillableController>();
        _killableCount = killables.Length;
        _deadCount = 0;
        foreach (var killable in killables)
        {
            killable.RegisterArena(this);
        }        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null && !_active)
        {
            _active = true;
            foreach(var door in Doors)
            {
                door.Shut();
            }
        }
    }

    internal void NotifyDeath()
    {
        _deadCount++;
    }
}
