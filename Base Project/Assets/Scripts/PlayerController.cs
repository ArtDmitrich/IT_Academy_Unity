using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        GameController.Instance.Turned += Turn;
        GameController.Instance.SprintStarted += SetMultiplier;
        GameController.Instance.SprintCanceled += SetMultiplier;
    }

    private void OnDisable()
    {
        GameController.Instance.Turned -= Turn;
        GameController.Instance.SprintStarted -= SetMultiplier;
        GameController.Instance.SprintCanceled -= SetMultiplier;
    }

    private void Turn()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void SetMultiplier(float multiplier)
    {
        Debug.Log("SetMultiplier");

        _animator.speed = multiplier;
    }
}
