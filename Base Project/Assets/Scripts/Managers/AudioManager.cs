public class AudioManager : ItemManager<AudioManager>
{
    public Sound GetSound(string soundName)
    {
        return _poolManager.GetPooledItem<Sound>(soundName);
    }
}
