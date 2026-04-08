using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LineTargetFollower : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Transform player;

    LineRenderer lineRenderer;

    bool active = false;

    [SerializeField]
    bool isDistanceCounter = false;

    [SerializeField]
    TextMeshProUGUI distanceCounter;

    [SerializeField]
    bool alwaysActive = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (active || alwaysActive)
        {
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, target.position);
            if (isDistanceCounter)
            {
                distanceCounter.text =
                    $"{Math.Round((target.position - player.position).sqrMagnitude)}m";
            }
        }
        else
        {
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, player.position);
            distanceCounter.text = $"None";
        }
    }

    public void SetActive(bool active)
    {
        this.active = active;
    }

    public void SetPosition(Vector3 transform)
    {
        target.position = transform;
    }
}
