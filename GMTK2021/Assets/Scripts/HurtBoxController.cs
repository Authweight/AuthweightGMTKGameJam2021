using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoxController : MonoBehaviour
{
    public int Damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Hurt(Damage);
        }
    }
}
