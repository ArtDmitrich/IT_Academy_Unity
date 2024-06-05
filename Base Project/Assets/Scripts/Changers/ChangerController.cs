using UnityEngine;

public class ChangerController : MonoBehaviour
{
    [SerializeField] private string _tagToTriggerMutable;

    private IMovable Movement { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;

    private IMutableObject Mutable { get { return _mutable = _mutable ?? GetComponent<IMutableObject>(); } }
    private IMutableObject _mutable;

    private void OnEnable()
    {
        Movement.StartMovement(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == _tagToTriggerMutable)
        {            
            Mutable.ChangeProperty();
        }
    }
}
