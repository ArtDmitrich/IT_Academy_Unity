using UnityEngine;

public class Obstacle : MonoBehaviour, IRefresh
{
    private Vector3 _startPos;

    public virtual void Refresh()
    {
        transform.position = _startPos;
    }

    private void Awake()
    {
        _startPos = transform.position;
    }
}
