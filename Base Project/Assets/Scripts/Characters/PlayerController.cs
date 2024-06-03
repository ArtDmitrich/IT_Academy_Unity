using UnityEngine;

[RequireComponent(typeof(PhysicalMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private PhysicalMovement PlayerMovement { get { return _playerMovement = _playerMovement ?? GetComponent<PhysicalMovement>(); } }
    private PhysicalMovement _playerMovement;

    private Vector2 _currentDirection = Vector2.right;

    private void OnEnable()
    {
        GameController.Instance.PlayerMoveningStarted += StartMovement;
        GameController.Instance.PlayerMoveningStoped += StopMovement;
        GameController.Instance.PlayerJumped += Jump;
    }

    private void OnDisable()
    {
        GameController.Instance.PlayerMoveningStarted -= StartMovement;
        GameController.Instance.PlayerMoveningStoped -= StopMovement;
        GameController.Instance.PlayerJumped -= Jump;
    }

    private void StartMovement(Vector2 direction)
    {
        PlayerMovement.StartMovement(direction);
        _animator.SetTrigger("StartMovement");
        
        if (direction != _currentDirection)
        {
            _currentDirection = direction;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    private void StopMovement()
    {
        PlayerMovement.StopMovement();
        _animator.SetTrigger("StopMovement");
    }

    private void Jump()
    {
        PlayerMovement.Jump();
    }
}
