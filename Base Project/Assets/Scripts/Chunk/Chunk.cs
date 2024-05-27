using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    public UnityEvent<Chunk> ChunkPlacedByPlayer;

    [SerializeField] private List<string> _oneShotSoundNames;
    [SerializeField] private List<string> _loopingSoundNames;
    [SerializeField] private List<Transform> _spotsForOneShotSounds;

    [SerializeField] private float _minTimeToOneShotSound;
    [SerializeField] private float _maxTimeToOneShotSound;

    [SerializeField] private List<TriggerZoneController> _enterancesInChunk;

    private SoundController SoundController { get { return _soundController = _soundController ?? GetComponent<SoundController>(); } }

    private SoundController _soundController;
    private float _timer;

    private void OnEnable()
    {
        foreach (var entrance in _enterancesInChunk)
        {
            entrance.ZoneTriggered.AddListener(ChunkPlaced);
        }

        LoopingPlay();

        _timer = Random.Range(_minTimeToOneShotSound, _maxTimeToOneShotSound);
    }

    private void OnDisable()
    {
        foreach (var entrance in _enterancesInChunk)
        {
            entrance.ZoneTriggered.RemoveListener(ChunkPlaced);
        }
    }

    private void Update()
    {
        if (_timer <= 0f)
        {
            _timer = Random.Range(_minTimeToOneShotSound, _maxTimeToOneShotSound);
            OneShotPlay();
        }

        _timer -= Time.deltaTime;
    }

    private void ChunkPlaced()
    {
        ChunkPlacedByPlayer?.Invoke(this);

        LoopingPlay();
    }

    private void OneShotPlay()
    {
        var indexOfSound = Random.Range(0, _oneShotSoundNames.Count);
        var indexOfSpot = Random.Range(0, _spotsForOneShotSounds.Count);
        SoundController.OneShotPlay(_oneShotSoundNames[indexOfSound], _spotsForOneShotSounds[indexOfSpot].position);
    }

    private void LoopingPlay()
    {
        var index = Random.Range(0, _loopingSoundNames.Count);

        SoundController.LoopingPlay(_loopingSoundNames[index], transform.position);
    }
}
