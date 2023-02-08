using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    bool isPlacable;
    [SerializeField]
    Tower TowerPrefab;

    public bool IsPlaceable { get { return isPlacable; } }

    private void OnMouseDown()
    {
        if (isPlacable)
        {
            bool isPlaced = TowerPrefab.CreateTower(TowerPrefab, transform.position);
            isPlacable= !isPlaced;
        }
    }
}