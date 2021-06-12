using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var occluder = collision.GetComponent<OccluderController>();
        if (occluder != null)
        {
            var newShadow = Instantiate(occluder.Shadow);
            newShadow.SetReferences(transform, occluder.transform);
            newShadow.SetTransform(transform.position, occluder.transform.position);
        }
    }
}
