using System;
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

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (active)
        {
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, target.position);
        }
        else
        {
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, player.position);
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
