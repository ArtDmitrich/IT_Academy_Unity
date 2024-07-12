public class AudioManager : ItemManager<AudioManager>
{
    public Sound GetSound(string soundName)
    {
        var item = _poolManager.GetPooledItem(soundName);

        if (item == null)
        {
            return null;
        }
        else if(item.TryGetComponent<Sound>(out var sound))
        {
            return sound;
        }

        return null;
    }
}
