using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GasTransformSetter : MonoBehaviour
{
    Transform[] gasStations;

    [SerializeField]
    GameObject player;

    void Start()
    {
        gasStations = GameObject
            .FindGameObjectsWithTag("GasPlanet")
            .Select(obj => obj.transform)
            .ToArray();
    }

    void FixedUpdate()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Transform targetObj in gasStations)
        {
            Vector3 directionToTarget = targetObj.position - player.transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = targetObj.transform;
            }
        }
        this.transform.position = bestTarget.transform.position;
    }
}
