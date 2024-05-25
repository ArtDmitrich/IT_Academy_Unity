using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    [SerializeField] private Chunk _currentChunk;
    [SerializeField] private Chunk _upChunk;
    [SerializeField] private Chunk _downChunk;

    [SerializeField] private float _offsetY;

    private List<Chunk> _chunks;

    private void Awake()
    {
        _chunks = new List<Chunk>
        {
            _currentChunk,
            _upChunk,
            _downChunk
        };
    }

    private void ReplaceChunks(Chunk chunk)
    {
        if (chunk == null || chunk == _currentChunk)
        {
            return;
        }


        if (chunk == _upChunk)
        {
            SetChunksAndOffset(_downChunk, _currentChunk, chunk, true);
        }
        else if (chunk == _downChunk)
        {
            SetChunksAndOffset(_currentChunk, _upChunk, chunk, false);
        }
    }

    private void SetChunksAndOffset(Chunk upChunk, Chunk downChunk,Chunk currentChunk, bool upChunkOffseted)
    {
        _upChunk = upChunk;
        _downChunk = downChunk;
        _currentChunk = currentChunk;

        if (upChunkOffseted)
        {
            _upChunk.transform.position = _currentChunk.transform.position + Vector3.up * _offsetY;

        }
        else
        {
            _downChunk.transform.position = _currentChunk.transform.position - Vector3.up * _offsetY;
        }
    }

    private void OnEnable()
    {
        foreach (var chunk in _chunks)
        {
            chunk.ChunkPlacedByPlayer.AddListener(ReplaceChunks);
        }
    }

    private void OnDisable()
    {
        foreach (var chunk in _chunks)
        {
            chunk.ChunkPlacedByPlayer.RemoveListener(ReplaceChunks);
        }
    }
}
