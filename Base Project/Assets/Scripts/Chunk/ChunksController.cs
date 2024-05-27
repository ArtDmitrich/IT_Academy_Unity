using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunks;

    [SerializeField] private Chunk _centerChunk;
    [SerializeField] private Chunk _upChunk;
    [SerializeField] private Chunk _downChunk;

    [SerializeField] private float _offsetY;

    [SerializeField] private MixerController _mixerController;

    private void ReplaceChunks(Chunk chunk)
    {
        if (chunk == null || chunk == _centerChunk)
        {
            return;
        }

        if (chunk == _upChunk)
        {
            var newUpChunk = GetChunk(_upChunk, _centerChunk);

            if (newUpChunk == null)
            {
                return;
            }

            _downChunk.gameObject.SetActive(false);
            newUpChunk.gameObject.SetActive(true);

            SetChunksAndOffset(newUpChunk, _centerChunk, chunk, true);
        }
        else if (chunk == _downChunk)
        {
            var newDownChunk = GetChunk(_downChunk, _centerChunk);

            if (newDownChunk == null)
            {
                return;
            }

            _upChunk.gameObject.SetActive(false);
            newDownChunk.gameObject.SetActive(true);

            SetChunksAndOffset(_centerChunk, newDownChunk, chunk, false);
        }

        if (_mixerController != null)
        {
            _mixerController.SetSnapshot(chunk.tag);
        }
    }

    private void SetChunksAndOffset(Chunk upChunk, Chunk downChunk,Chunk currentChunk, bool upChunkOffseted)
    {
        _upChunk = upChunk;
        _downChunk = downChunk;
        _centerChunk = currentChunk;

        if (upChunkOffseted)
        {
            _upChunk.transform.position = _centerChunk.transform.position + Vector3.up * _offsetY;

        }
        else
        {
            _downChunk.transform.position = _centerChunk.transform.position - Vector3.up * _offsetY;
        }
    }

    private Chunk GetChunk(Chunk excludingChunk1,  Chunk excludingChunk2)
    {
        //any sampling logic
        foreach (var chunk in _chunks)
        {
            if (chunk == excludingChunk1 || chunk == excludingChunk2)
            {
                continue;
            }

            return chunk;
        }

        return null;
    }

    private void OnEnable()
    {
        foreach (var chunk in _chunks)
        {
            chunk.ChunkPlacedByPlayer.AddListener(ReplaceChunks);
        }

        if (_mixerController != null)
        {
            _mixerController.SetSnapshot(_centerChunk.tag);
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
