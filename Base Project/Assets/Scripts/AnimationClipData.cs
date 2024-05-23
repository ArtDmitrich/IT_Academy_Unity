using System;
using UnityEngine;

[Serializable]
public struct AnimationClipData
{
    public float ClipLength { get { return _clip.length; } }

    public string Title;

    [SerializeField] AnimationClip _clip;
}
