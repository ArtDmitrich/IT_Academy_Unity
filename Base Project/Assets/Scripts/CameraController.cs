using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _freeLookCamera;
    [SerializeField] AnimationClips _clips;

    private void Start()
    {
        if (_freeLookCamera != null)
        {
            _freeLookCamera.gameObject.SetActive(false);
        }

        StartCoroutine(SetActiveAfterTime(_clips.GetAnimationLength("Spawn")));
    }

    private IEnumerator SetActiveAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (_freeLookCamera != null)
        {
            _freeLookCamera.gameObject.SetActive(true);
        }
    }
}
