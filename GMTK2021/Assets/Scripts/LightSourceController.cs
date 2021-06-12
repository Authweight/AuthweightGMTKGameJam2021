using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{
    private Dictionary<int, ShadowController> _references = new Dictionary<int, ShadowController>();

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
            var id = occluder.GetInstanceID();
            _references[id] = newShadow;
            newShadow.SetReferences(transform, occluder.transform);
            newShadow.SetTransform(transform.position, occluder.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var occluder = collision.GetComponent<OccluderController>();
        if (occluder != null)
        {
            var id = occluder.GetInstanceID();
            if (_references.ContainsKey(id))
            {
                Destroy(_references[id].gameObject);
                _references.Remove(id);
            }
        }
    }
}
