using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private Selector _selector;
    [SerializeField] private Painter _painter;
    [SerializeField] private Transporter _transporter;

    [SerializeField] private float _durationMove;

    private GameObject _selectedAirplane;

    private void Start()
    {
        _selector.ItemSelected += SetSelectedAirplane;
    }

    private void SetSelectedAirplane(GameObject airplane, bool next)
    {
        airplane.SetActive(true);

        MoveSelectedAirplaneAway(next);
        MoveNewAirplaneToCenter(airplane, next);

        _selectedAirplane = airplane;
        _painter.SetCurrentMesh(_selectedAirplane.GetComponent<MeshRenderer>());
    }

    private void MoveSelectedAirplaneAway(bool next)
    {
        if (_selectedAirplane != null)
        {
            if (next)
            {
                _transporter.MoveToRight(_selectedAirplane.transform, _durationMove);
            }
            else
            {
                _transporter.MoveToLeft(_selectedAirplane.transform, _durationMove);
            }

            StartCoroutine(Coroutines.SetActiveObjectAfterTime(_selectedAirplane, false, _durationMove));
        }
    }

    private void MoveNewAirplaneToCenter(GameObject airplane, bool next)
    {
        _transporter.MoveToCenter(airplane.transform, _durationMove, next);
    }

    private void OnDestroy()
    {
        _selector.ItemSelected -= SetSelectedAirplane;
    }
}
