using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private float _jumpForce;
    [SerializeField] private string _tagForGrounding;

    private Rigidbody2D Rb { get { return _rb = _rb ?? GetComponent<Rigidbody2D>(); } }
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        GameController.Instance.Turned += Turn;
        GameController.Instance.SprintStarted += SetMultiplier;
        GameController.Instance.SprintCanceled += SetMultiplier;
        GameController.Instance.Jumped += Jump;
    }

    private void OnDisable()
    {
        GameController.Instance.Turned -= Turn;
        GameController.Instance.SprintStarted -= SetMultiplier;
        GameController.Instance.SprintCanceled -= SetMultiplier;
        GameController.Instance.Jumped -= Jump;
    }

    private void Turn()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void Jump()
    {
        Rb.AddRelativeForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        _animator.SetTrigger("Jump");
    }

    private void SetMultiplier(float multiplier)
    {      
        _animator.speed = multiplier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _tagForGrounding)
        {
            _animator.SetTrigger("Grounded");
        }
    }
}
