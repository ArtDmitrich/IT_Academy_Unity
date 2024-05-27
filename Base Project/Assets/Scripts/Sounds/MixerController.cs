using System;
using System.Collections.Generic;
using UnityEngine;

public class MixerController : MonoBehaviour
{
    [SerializeField] private List<SnapshotData> _snapshots;
    [SerializeField] private float _timeToReach;

    public void SetSnapshot(string tag)
    {
        foreach (var snapshot in _snapshots)
        {
            if(snapshot.Title.Contains(tag))
            {
                snapshot.Snapshot.TransitionTo(_timeToReach);
            }
        }
    }
}
