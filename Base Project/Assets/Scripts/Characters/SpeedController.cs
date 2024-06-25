using UnityEngine;
using UnityEngine.AI;

public class SpeedController : MonoBehaviour
{
    public float BaseSpeed
    {
        get { return _baseSpeed; }
        set
        {
            if (value < 0)
            {
                _baseSpeed = 0;
            }

            _baseSpeed = value;
        }
    }

    private NavMeshAgent Agent { get { return _agent = _agent ?? GetComponent<NavMeshAgent>(); } }
    private NavMeshAgent _agent;

    private float _baseSpeed;

    private void Start()
    {
        _baseSpeed = Agent.speed;
    }

    void Update()
    {
        var environmentMultiplier = GetMultiplierFromEnvironment();
        SetMultipliedSpeed(_baseSpeed, environmentMultiplier);
    }

    private void SetMultipliedSpeed(float speed, float multipier)
    {
        Agent.speed = speed * multipier;
    }

    private float GetMultiplierFromEnvironment()
    {
        Agent.SamplePathPosition(NavMesh.AllAreas, 1f, out NavMeshHit hit);
        int index = IndexFromMask(hit.mask);

        if(index < 0)
        {
            return 0.6f;
        }

        var areaCost = Agent.GetAreaCost(index);

        //return areaCost > 0 ? 1f / areaCost : Mathf.Abs(areaCost);
        return 1f / areaCost;
    }

    private int IndexFromMask(int mask)
    {
        for (int i = 0; i < 32; ++i)
        {
            if ((1 << i) == mask)
                return i;
        }

        return -1;
    }
}
