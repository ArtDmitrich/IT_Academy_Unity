using UnityEngine;
using UnityEngine.Events;

public class ParticleCallback : MonoBehaviour
{
    public UnityAction ParticleStoped;

    private ParticleSystem _particleSystem;

    protected void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        var main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        ParticleStoped?.Invoke();
    }
}
