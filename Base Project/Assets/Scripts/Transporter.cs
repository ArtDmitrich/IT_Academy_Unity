using DG.Tweening;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _centerPoint;

    public void MoveToRight(Transform tansformToMove, float duration)
    {
        tansformToMove.DOMove(_rightPoint.position, duration);
    }

    public void MoveToLeft(Transform tansformToMove, float duration)
    {
        tansformToMove.DOMove(_leftPoint.position, duration);
    }

    public void MoveToCenter(Transform tansformToMove, float duration, bool fromLeft)
    {
        if (fromLeft)
        {
            tansformToMove.position = _leftPoint.position;
        }
        else
        {
            tansformToMove.position = _rightPoint.position;
        }

        tansformToMove.DOMove(_centerPoint.position, duration);
    }
}
