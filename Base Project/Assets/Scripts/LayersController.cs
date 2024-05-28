using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersController : MonoBehaviour
{
    [SerializeField] private List<ChunksController> _layers;

    private void OnEnable()
    {
        GameController.Instance.Turned += ReverseDirectionOfMovement;
        GameController.Instance.SprintStarted += SetMultiplier;
        GameController.Instance.SprintCanceled += SetMultiplier;
    }
    private void OnDisable()
    {
        GameController.Instance.Turned -= ReverseDirectionOfMovement;
        GameController.Instance.SprintStarted -= SetMultiplier;
        GameController.Instance.SprintCanceled -= SetMultiplier;
    }

    private void ReverseDirectionOfMovement()
    {
        foreach (var layer in _layers)
        {
            layer.MoveRight = !layer.MoveRight;
        }
    }

    private void SetMultiplier(float multiplier)
    {
        foreach (var layer in _layers)
        {
            layer.MovementSpeedMultiplier = multiplier;
        }
    }
}
