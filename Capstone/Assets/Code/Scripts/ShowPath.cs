using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShowPath : MonoBehaviour
{
    public static ShowPath instance;

    public LineRenderer line;
    public float LINE_HEIGHT_ABOVE_GROUND = 0.1f; // Line height above ground
    private NavMeshPath path;

    void Awake()
    {
        instance = this;
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        path = new NavMeshPath();
        line.enabled = true;
    }

    // New method to update path from an external script (like your destination selector)
    public void UpdatePath(Vector3 startPosition, Vector3 destination)
    {
        if (NavMesh.CalculatePath(startPosition, destination, NavMesh.AllAreas, path))
        {
            DrawPath(path);
        }
        else
        {
            Debug.LogError("Path could not be calculated!");
            line.enabled = true;
        }
    }

    // Draws the calculated path
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
