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
        //сбрасываем в ноль значение поворота, чтобы платформа была корректно повернута к основной камере 
        _miniCameraPlatform.ResetRotation();

        //убираем родителя с текущего самолета
        if (_selectedAirplane != null)
        {
            _selectedAirplane.transform.SetParent(null);
        }

        //перед каждой установкой нового самолета выключаем камеру и включаем новый объект
        _miniCamera.gameObject.SetActive(false);        
        airplane.SetActive(true);

        //убираем вбок выбранный самолет (если такой есть) и перемещаем "из-за экрана" в центр новый самолет
        MoveSelectedAirplaneAway(next);
        MoveNewAirplaneToCenter(airplane, next);

        //устанавливаем новый самолет в текущий и даем меш рендерер классу, отвечающему за покраску
        //устанавливаем родительский объект для нового самолета
        _selectedAirplane = airplane;
        _painter.SetCurrentMesh(_selectedAirplane.GetComponent<MeshRenderer>());
        _selectedAirplane.transform.SetParent(_miniCameraPlatform.transform);

        //запускаем корутину, которая включит камеру через время, равное времени перемещения объектов перед основной камерой
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
