using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinesController : MonoBehaviour
{
    [SerializeField] private List<PlayableDirectorData> _directors;

    public void Play(string dirName)
    {
        foreach (var director in _directors)
        {
            if (director.Key == dirName)
            {
                director.Value.Play();
            }
        }
    }
}
