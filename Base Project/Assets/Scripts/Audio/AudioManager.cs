using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound GetSound(string soundName)
    {
        var item = PoolsManager.Instance.GetPooledItem(soundName);

        if (item.TryGetComponent<Sound>(out var sound))
        {
            return sound;
        }

        return null;
    }
}
