using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject _bulletBody;
    [SerializeField] protected ParticleSystem _hitEffect;
    [SerializeField] protected float _livetime;

    public Rigidbody BulletRigidbody { get { return _rb = _rb ?? GetComponent<Rigidbody>(); } }

    private Rigidbody _rb;
    private ParticleCallback _cb;

    private void Awake()
    {
        _cb = _hitEffect.GetComponent<ParticleCallback>();
    }

    protected void OnEnable()
    {
        _bulletBody.SetActive(true);
        StartCoroutine(DeactivateFromTime(_livetime));
        _cb.ParticleStoped += Deactivate;
    }

    private void OnDisable()
    {
        _cb.ParticleStoped -= Deactivate;
    }

    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }

    protected IEnumerator DeactivateFromTime(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
