using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Zenject.SpaceFighter;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject _bulletBody;
    [SerializeField] protected ParticleSystem _hitEffect;
    [SerializeField] protected TrailRenderer _trail;
    [SerializeField] protected float _livetime;

    public Rigidbody BulletRigidbody { get { return _rb = _rb ?? GetComponent<Rigidbody>(); } }

    private Rigidbody _rb;
    private ParticleCallback _cb;
    private PooledItem _pooledItem;
    private float _trailTime;
    public void PreparingToShoot(Transform shootingSpot)
    {
        _bulletBody.SetActive(true);
        BulletRigidbody.isKinematic = true;

        transform.position = shootingSpot.position;
        transform.rotation = Quaternion.identity;

        BulletRigidbody.isKinematic = false;
        BulletRigidbody.velocity = Vector3.zero;
        BulletRigidbody.angularVelocity = Vector3.zero;

        _trail.time = _trailTime;
    }

    protected void Hit()
    {
        _bulletBody.SetActive(false);
        BulletRigidbody.isKinematic = true;
        _trail.time = 0.0f;

        _hitEffect.Play();
    }

    protected void Deactivate()
    {
        _pooledItem.Release();
    }

    protected IEnumerator DeactivateFromTime(float time)
    {
        yield return new WaitForSeconds(time);

        Deactivate();
    }

    protected void OnEnable()
    {
        StartCoroutine(DeactivateFromTime(_livetime));
        _cb.ParticleStoped += Deactivate;
    }

    protected void OnDisable()
    {
        _cb.ParticleStoped -= Deactivate;
    }
    protected void Awake()
    {
        _cb = _hitEffect.GetComponent<ParticleCallback>();
        _pooledItem = gameObject.GetComponent<PooledItem>();

        _trailTime = _trail.time;
    }
}
