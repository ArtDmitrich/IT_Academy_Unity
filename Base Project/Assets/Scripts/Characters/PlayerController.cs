using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    //[SerializeField] private float _rotationSpeed;

    private NavMeshAgent Agent { get { return _agent = _agent ?? GetComponent<NavMeshAgent>(); } }
    private NavMeshAgent _agent;

    private InputActions _input;
    //private float _rotationAngel = 0.0f;

    private void Awake()
    {
        _input = new InputActions();
    }

    //private void Update()
    //{
    //    var inputMovement = _input.Player.Movement.ReadValue<Vector2>();
    //    var rotationY = _camera.transform.rotation.eulerAngles.y;

    //    MoveCharacter(inputMovement, rotationY);
    //}

    //private void MoveCharacter(Vector2 input, float rotationY)
    //{
    //    Vector3 movement = new Vector3(input.x, 0.0f, input.y);
    //    Vector3 rotatedMovement = Quaternion.Euler(0.0f, rotationY, 0.0f) * movement.normalized;

    //    Agent.Move(rotatedMovement * Agent.speed * Time.deltaTime);

    //    if (rotatedMovement.sqrMagnitude > 0.0f)
    //    {
    //        _rotationAngel = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
    //    }

    //    Quaternion currentRotation = transform.rotation;
    //    Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngel, 0.0f);
    //    transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed);
    //}

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.MouseLeftClick.performed += MouseLeftClick_performed;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.MouseLeftClick.performed -= MouseLeftClick_performed;
    }

    private void MouseLeftClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_camera != null)
        {
            var mousePos = _input.Player.MousePos.ReadValue<Vector2>();
            Ray ray = _camera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Agent.SetDestination(hit.point);
            }
        }        
    }
}
