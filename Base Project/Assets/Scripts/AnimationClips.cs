using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationClips : MonoBehaviour
{
    public int AttacksCount { get { return _attacksCount; } }

    [SerializeField] private int _attacksCount;
    [SerializeField] private List<AnimationClipData> _animations;
      
    public float GetAnimationLength(string animationTitle)
    {
        foreach (var animation in _animations)
        {
            if (animationTitle == animation.Title)
            {
                return animation.ClipLength;
            }
        }

        return 0f;
    }
}
