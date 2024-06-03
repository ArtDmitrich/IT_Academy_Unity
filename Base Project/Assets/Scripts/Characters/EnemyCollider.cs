using UnityEngine;

public enum EnemyType
{
    EnemyCharacter,
    Spike
}

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] EnemyType _enemyType;
    [SerializeField] private float _recoilImpulse;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var normal = collision.contacts[0].normal;

            if (CheckKillingEnemy(normal))
            {
                collision.rigidbody.AddForce(Vector2.up * _recoilImpulse, ForceMode2D.Impulse);
                gameObject.SetActive(false);
            }
            else
            {
                GameController.Instance.ChangePlayerLivesCount(-1);
            }

        }
    }

    private bool CheckKillingEnemy(Vector2 normal)
    {
        switch (_enemyType)
        {
            case EnemyType.EnemyCharacter:
                return normal.y < 0f;
            case EnemyType.Spike:
                return false;
            default:
                return false;
        }
    }
}

