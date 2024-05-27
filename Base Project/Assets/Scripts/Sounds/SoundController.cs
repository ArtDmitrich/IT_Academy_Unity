using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundController : MonoBehaviour
{
    [SerializeField] private List<AudioSourceData> _sounds;

    private readonly List<AudioSourceData> _activeSounds = new List<AudioSourceData>();

    public void LoopingPlay(string soundName, Vector3 position)
    {
        var sound = GetSound(soundName, _sounds);

        if (sound != null && !_activeSounds.Contains(sound))
        {
            _activeSounds.Add(sound);
            sound.AudioSource.loop = true;
            sound.AudioSource.transform.position = position;
            sound.AudioSource.Play();
        }
    }
    public void OneShotPlay(string soundName, Vector3 position)
    {
        var sound = GetSound(soundName, _sounds);

        if (sound != null)
        {
            sound.AudioSource.transform.position = position;
            sound.AudioSource.Play();
        }        
    }

    public void Pause(string soundName)
    {
        var sound = GetSound(soundName, _activeSounds);

        if (sound != null)
        {
            _activeSounds.Remove(sound);
            sound.AudioSource.Pause();
        }        
    }

    private AudioSourceData GetSound(string soundName, List<AudioSourceData> sounds)
    {
        if (sounds.Count == 0)
        {
            return null;
        }

        foreach (var sound in sounds)
        {
            if (sound.Title == soundName)
            {
                return sound;
            }
        }

        return null;
    }
}
