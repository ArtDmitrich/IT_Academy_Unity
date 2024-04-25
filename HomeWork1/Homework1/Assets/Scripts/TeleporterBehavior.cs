using UnityEngine;

public class TeleporterBehavior : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _timeBeforeTeleportion;

    private float _timer;

    private void Start()
    {
        _timer = _timeBeforeTeleportion;     
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            TeleportObjectToRandomPoint();
            _timer = _timeBeforeTeleportion;
        }
    }

    private void TeleportObjectToRandomPoint()
    {
        transform.position += new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
