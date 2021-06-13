using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangleController : MonoBehaviour
{
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var multiplier = 0;
        if (collision.gameObject.tag == "Player")
        {
            multiplier = 200;
        }

        if (collision.gameObject.tag == "Player hitbox")
        {
            multiplier = 300;
        }

        _rb.AddForce(((transform.position - collision.gameObject.transform.position).normalized) * multiplier);
    }
}
