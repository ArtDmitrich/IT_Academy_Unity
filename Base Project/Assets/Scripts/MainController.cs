using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private Selector _selector;
    [SerializeField] private Painter _painter;
    [SerializeField] private Transporter _transporter;
    [SerializeField] private GameObject _miniCamera;
    [SerializeField] private PlatformRotator _miniCameraPlatform;

    [SerializeField] private float _durationMove;

    private GameObject _selectedAirplane;
    private GameInput _input;

    private void Awake()
    {
        _input = new GameInput();
        _input.Enable();

        _selector.ItemSelected += SetSelectedAirplane;
    }

    private void Update()
    {
        var inputHor = _input.MainScene.RotatePlatform.ReadValue<float>();

        if (inputHor != 0f)
        {
            _miniCameraPlatform.RotatePlatform(inputHor * Time.deltaTime);
        }

    }

    private void SetSelectedAirplane(GameObject airplane, bool next)
    {
        //���������� � ���� �������� ��������, ����� ��������� ���� ��������� ��������� � �������� ������ 
        _miniCameraPlatform.ResetRotation();

        //������� �������� � �������� ��������
        if (_selectedAirplane != null)
        {
            _selectedAirplane.transform.SetParent(null);
        }

        //����� ������ ���������� ������ �������� ��������� ������ � �������� ����� ������
        _miniCamera.gameObject.SetActive(false);        
        airplane.SetActive(true);

        //������� ���� ��������� ������� (���� ����� ����) � ���������� "��-�� ������" � ����� ����� �������
        MoveSelectedAirplaneAway(next);
        MoveNewAirplaneToCenter(airplane, next);

        //������������� ����� ������� � ������� � ���� ��� �������� ������, ����������� �� ��������
        //������������� ������������ ������ ��� ������ ��������
        _selectedAirplane = airplane;
        _painter.SetCurrentMesh(_selectedAirplane.GetComponent<MeshRenderer>());
        _selectedAirplane.transform.SetParent(_miniCameraPlatform.transform);

        //��������� ��������, ������� ������� ������ ����� �����, ������ ������� ����������� �������� ����� �������� �������
        StartCoroutine(Coroutines.SetActiveObjectAfterTime(_miniCamera.gameObject, true, _durationMove));
    }

    private void MoveSelectedAirplaneAway(bool next)
    {
        if (_selectedAirplane != null)
        {
            _transporter.MoveToSide(_selectedAirplane.transform, _durationMove, next);

            StartCoroutine(Coroutines.SetActiveObjectAfterTime(_selectedAirplane, false, _durationMove));
        }
    }

    private void MoveNewAirplaneToCenter(GameObject airplane, bool next)
    {
        _transporter.MoveToCenter(airplane.transform, _durationMove, next);
    }

    private void OnDestroy()
    {
        if (_selector != null)
        {
            _selector.ItemSelected -= SetSelectedAirplane;            
        }
    }
}
