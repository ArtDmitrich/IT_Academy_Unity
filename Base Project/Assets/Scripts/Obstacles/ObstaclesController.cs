using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _obstacles;

    public void RefreshObstacles()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.Refresh();
        }
    }
}
