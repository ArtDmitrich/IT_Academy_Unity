using UnityEngine;
using UnityEngine.Events;

public class BuildingTriggerZone : MonoBehaviour
{
    public UnityAction<bool, int> PlayerDetected;

    [SerializeField] private string _tagToDetected;
    [SerializeField] private bool _insideBuilding;

    [Min(0)]
    [SerializeField] private int _floorIndex;

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_tagToDetected))
        {
            PlayerDetected?.Invoke(_insideBuilding, _floorIndex);
        }
    }
}
