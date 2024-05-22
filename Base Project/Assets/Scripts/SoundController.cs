using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    private AudioSource _source;
    private AudioSource Source { get { return _source = _source ?? GetComponent<AudioSource>(); } }

    public void PlaySound()
    {
        Source.Play();
    }
        
}
