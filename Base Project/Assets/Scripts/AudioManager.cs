using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioSourceData> _audioSources;

    public void PlaySound(string soundName, Vector3 position)
    {
        for (int i = 0; i < _audioSources.Count; i++)
        {
            if (_audioSources[i].Key == soundName)
            {
                var sound = _audioSources[i].Value;
                sound.transform.position = position;
                sound.Play();
            }
        }
    }
}
