using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    [SerializeField] private Button _red;
    [SerializeField] private Button _blue;
    [SerializeField] private Button _yellow;
    [SerializeField] private Button _green;

    private MeshRenderer _selectedMesh;

    private void Start()
    {
        AddListenerToButton(_red, delegate { SetColor(_selectedMesh, Color.red); });
        AddListenerToButton(_blue, delegate { SetColor(_selectedMesh, Color.blue); });
        AddListenerToButton(_yellow, delegate { SetColor(_selectedMesh, Color.yellow); });
        AddListenerToButton(_green, delegate { SetColor(_selectedMesh, Color.green); });
    }

    public void SetCurrentMesh(MeshRenderer newMesh)
    {
        _selectedMesh = newMesh;
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

    private void SetColor(MeshRenderer mesh, Color color)
    {
        if (mesh == null)
        {
            Debug.LogError(gameObject.name + "Mesh is NULL!!!");
            return;
        }

        mesh.material.color = color;
    }

    private void OnDestroy()
    {
        _red.onClick.RemoveAllListeners();
        _blue.onClick.RemoveAllListeners();
        _yellow.onClick.RemoveAllListeners();
        _green.onClick.RemoveAllListeners();
    }
}
