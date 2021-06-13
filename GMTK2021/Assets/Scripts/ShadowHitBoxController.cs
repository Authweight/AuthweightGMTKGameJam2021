using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHitBoxController : MonoBehaviour
{
    public int Damage;

    private HashSet<int> _currentlyColliding = new HashSet<int>();

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
        var playerShadow = collision.gameObject.GetComponent<PlayerShadowController>();
        if (playerShadow != null && !_currentlyColliding.Contains(playerShadow.GetInstanceID()))
        {
            _currentlyColliding.Add(playerShadow.GetInstanceID());
            playerShadow.Hurt(Damage);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var playerShadow = collision.gameObject.GetComponent<PlayerShadowController>();
        if (playerShadow != null && _currentlyColliding.Contains(playerShadow.GetInstanceID()))
        {
            _currentlyColliding.Remove(playerShadow.GetInstanceID());
        }
    }
}
