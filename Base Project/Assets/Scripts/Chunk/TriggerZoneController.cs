using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZoneController : MonoBehaviour
{
    public UnityEvent ZoneTriggered;

    [SerializeField] private string _tagForTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagForTrigger)) 
        {
            ZoneTriggered?.Invoke();
        }
    }
}
