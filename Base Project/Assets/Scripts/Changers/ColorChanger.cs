using UnityEngine;

public class ColorChanger : MonoBehaviour, IMutableObject
{
    private MeshRenderer Renderer { get { return _renderer = _renderer ?? GetComponentInChildren<MeshRenderer>(); } }
    private MeshRenderer _renderer;
    private readonly Color[] _colors = {Color.yellow, Color.red, Color.green, Color.blue, Color.black};

    public void ChangeProperty()
    {
        //working, but not visualizable effect
        //var color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
        var color = _colors[Random.Range(0, _colors.Length)];
        SetColor(color);
    }

    private void SetColor(Color color)
    {
        if (Renderer == null)
        {
            Debug.LogError(gameObject.name + "Mesh is NULL!!!");
            return;
        }

        Renderer.material.color = color;
    }
}
