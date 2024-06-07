using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private List<Tilemap> _floors;
    [SerializeField] private List<BuildingTriggerZone> _triggerZones;

    private void OnEnable()
    {
        foreach(var zone in _triggerZones)
        {
            zone.PlayerDetected += PlayerTriggeredZone;
        }

        SetActiveAllFloors();
    }

    private void OnDisable()
    {
        foreach (var zone in _triggerZones)
        {
            zone.PlayerDetected -= PlayerTriggeredZone;
        }
    }

    private void PlayerTriggeredZone(bool playerInBuilding, int floorIndex)
    {
        if (playerInBuilding)
        {
            SetActiveCurrentFloor(floorIndex);
        }
        else
        {
            SetActiveAllFloors();
        }
    }    

    private void SetActiveCurrentFloor(int floorIndex)
    {
        for (int i = 0; i < _floors.Count; i++)
        {
            _floors[i].gameObject.SetActive(floorIndex == i);
        }
    }

    private void SetActiveAllFloors()
    {
        foreach (var floor in _floors)
        {
            floor.gameObject.SetActive(true);
        }
    }
}
