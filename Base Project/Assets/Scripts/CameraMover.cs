using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Button _up;
    [SerializeField] private Button _down;
    [SerializeField] private Button _face;
    [SerializeField] private Button _left;

    [SerializeField] private Transform _upSpot;
    [SerializeField] private Transform _downSpot;
    [SerializeField] private Transform _faceSpot;
    [SerializeField] private Transform _leftSpot;

    [SerializeField] private Transform _targetForLook;

    [SerializeField] private PlatformRotator _platformRotator;
    private void Start()
    {
        AddListenerToButton(_up, delegate { SetCameraPosition(_upSpot.localPosition); });
        AddListenerToButton(_down, delegate { SetCameraPosition(_downSpot.localPosition); });
        AddListenerToButton(_face, delegate { SetCameraPosition(_faceSpot.localPosition); });
        AddListenerToButton(_left, delegate { SetCameraPosition(_leftSpot.localPosition); });

        SetCameraPosition(_upSpot.position);
    }

    private void AddListenerToButton(Button button, UnityAction method)
    {
        if (button == null)
        {
            Debug.LogError(gameObject.name + ": Button is NULL!!!");
            return;
        }

        button.onClick.AddListener(method);
    }

    private void SetCameraPosition(Vector3 position)
    {
        //"костыль" чтобы пофиксить баг: если повернуть самолет при помощи тача и переключить на другой вид (сверху или снизу), то на мини камере появится повернутый самолет
        //пробовал выставлть локальный угол по оси z в 0, но тогда некорректно отображались другие виды (фронтальный или слева). 
        //поэтому ресетим саму платформу при переключении камеры
        if (_platformRotator != null)
        {
            _platformRotator.ResetRotation();
        }

        transform.localPosition = position;

        transform.LookAt(_targetForLook);

    }

    private void RemoveListener(Button button, UnityAction method)
    {
        if (button == null)
        {
            return;
        }

        button.onClick.RemoveListener(method);
    }

    private void OnDestroy()
    {
        RemoveListener(_up, delegate { SetCameraPosition(_upSpot.localPosition); });
        RemoveListener(_down, delegate { SetCameraPosition(_downSpot.localPosition); });
        RemoveListener(_face, delegate { SetCameraPosition(_faceSpot.localPosition); });
        RemoveListener(_left, delegate { SetCameraPosition(_leftSpot.localPosition); });
    }
}
