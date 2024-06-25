using DG.Tweening;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    [SerializeField] private string _tagToCheck;
    [SerializeField] private Transform _door;
    [SerializeField] private float _moveTime;

    [SerializeField] private Transform _openPos;
    [SerializeField] private Transform _closePos;

    private void Start()
    {
        MoveDoor(_closePos.position);
    }

    private void MoveDoor(Vector3 targetPos)
    {
        _door.transform.DOMove(targetPos, _moveTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCheck))
        {
            MoveDoor(_openPos.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tagToCheck))
        {
            MoveDoor(_closePos.position);
        }
    }
}
