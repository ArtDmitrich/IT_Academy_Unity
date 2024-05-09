using DG.Tweening;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _centerPoint;

    public void MoveToSide(Transform tansformToMove, float duration, bool toRight)
    {
        if (toRight)
        {
            tansformToMove.DOMove(_rightPoint.position, duration);
        }
        else
        {
            tansformToMove.DOMove(_leftPoint.position, duration);
        }
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
