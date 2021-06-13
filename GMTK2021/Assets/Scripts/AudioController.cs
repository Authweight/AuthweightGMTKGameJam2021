using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private Dictionary<string, AudioSource> _clips;

    private void Start()
    {
        _clips = GetComponentsInChildren<AudioSource>().ToDictionary(x => x.name, x => x);
    }

    public void PlayClip(string clip)
    {
        _clips[clip].Play();
    }
}
