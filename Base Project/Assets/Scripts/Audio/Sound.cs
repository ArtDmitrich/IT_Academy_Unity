using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource _source;
    private PooledItem _pooledItem;

    private float _soundDuration;

    public void Play()
    {
        _source.Play();
        StartCoroutine(DeactivateFromTime(_soundDuration));
    }

    private IEnumerator DeactivateFromTime(float time)
    {
        yield return new WaitForSeconds(time);

        _pooledItem.Release();
    }

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _pooledItem = GetComponent<PooledItem>();
    }

    private void Start()
    {
        _soundDuration = _source.clip.length;
    }
}
