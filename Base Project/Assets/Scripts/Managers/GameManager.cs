using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> _changerNames;
    [SerializeField] private List<Transform> _spawnSpots;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private Transform _parentForObj;
    private InputController Input { get {  return _input = _input ?? new InputController(); } }
    private InputController _input;

    private List<ChangerController> _changers = new List<ChangerController>();

    private void OnEnable()
    {
        Input.Enable();
        Input.MainScene.Spawn.performed += Spawn_performed;
        Input.MainScene.Look.performed += Look_performed;
    }

    private void OnDisable()
    {
        Input.Enable();
        Input.MainScene.Spawn.performed -= Spawn_performed;
        Input.MainScene.Look.performed -= Look_performed;
    }

    private void Spawn_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var item = _spawner.Spawn(_changerNames[Random.Range(0, _changerNames.Count)]);

        if(item != null)
        {
            item.transform.position = _spawnSpots[Random.Range(0, _spawnSpots.Count)].position;
            item.transform.SetParent(_parentForObj);
            _changers.Add(item);
        }
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_changers.Count == 0)
        {
            return;
        }

        var objToLook = _changers[Random.Range(0, _changers.Count)].transform;

        if (objToLook != null && _virtualCamera != null)
        {
            _virtualCamera.LookAt = objToLook;
            _virtualCamera.Follow = objToLook;
        }
    }
}
