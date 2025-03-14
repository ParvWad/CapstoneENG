using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShowPath : MonoBehaviour
{
    public static ShowPath instance;

    public LineRenderer line;
    public Transform userTransform;  // Assign the AR-tracked user position
    public Transform destinationTransform; // Assign the destination dynamically
    public float updateInterval = 1.0f; // Time in seconds to update path
    public float LINE_HEIGHT_ABOVE_GROUND = 0.1f;

    private NavMeshPath path;
    private float nextUpdateTime = 0f;

    void Awake()
    {
        instance = this;
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        path = new NavMeshPath();
        line.enabled = false;
    }

    private void Update()
    {
        if (Time.time >= nextUpdateTime && userTransform != null && destinationTransform != null)
        {
            UpdatePath(userTransform.position, destinationTransform.position);
            nextUpdateTime = Time.time + updateInterval;
        }
    }

    public void UpdatePath(Vector3 startPosition, Vector3 destination)
    {
        if (NavMesh.CalculatePath(startPosition, destination, NavMesh.AllAreas, path))
        {
            DrawPath(path);
        }
        else
        {
            Debug.LogError("Path could not be calculated!");
            line.enabled = false;
        }
    }

    private void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2)
        {
            Debug.Log("Not enough corners to draw a path");
            return;
        }

        line.enabled = true;
        line.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            Vector3 linePosition = path.corners[i] + Vector3.up * LINE_HEIGHT_ABOVE_GROUND;
            line.SetPosition(i, linePosition);
        }
    }
}