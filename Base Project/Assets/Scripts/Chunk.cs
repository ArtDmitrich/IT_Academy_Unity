using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    public UnityEvent<Chunk> ChunkPlacedByPlayer;

    [SerializeField] private List<TriggerZoneController> _enterancesInChunk;

    private void ChunkPlaced()
    {
        ChunkPlacedByPlayer?.Invoke(this);
    }

    private void OnEnable()
    {
        foreach (var entrance in _enterancesInChunk)
        {
            entrance.ZoneTriggered.AddListener(ChunkPlaced);
        }
    }

    private void OnDisable()
    {
        foreach (var entrance in _enterancesInChunk)
        {
            entrance.ZoneTriggered.RemoveListener(ChunkPlaced);
        }
    }
}
