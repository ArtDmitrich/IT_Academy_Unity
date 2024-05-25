using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController Input { get { return _input = _input ?? new InputController(); } }
    private InputController _input;

    private void OnEnable()
    {
        Input.Enable();
        Input.Ninja.Turn.performed += Turn_performed;
    }
    private void OnDisable()
    {
        Input.Disable();
        Input.Ninja.Turn.performed -= Turn_performed;
    }

    private void Turn_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

}
