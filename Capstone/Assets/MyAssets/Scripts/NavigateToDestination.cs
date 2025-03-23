using UnityEngine;
using UnityEngine.AI;

public class NavigateToDestination : MonoBehaviour
{
    public Transform destination;          // Target destination (e.g., TV)
    private LineRenderer lineRenderer;     // LineRenderer to visualize the path
    private NavMeshPath navPath;           // NavMeshPath for path calculation
    public NavMeshAgent agent;             // agent doing navigation

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        navPath = new NavMeshPath();
        agent.isStopped = true; // he doesn't walk there like NPC :)) (but great for testing)

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component missing.");
        }

        if (destination == null)
        {
            Debug.LogWarning("Destination not set. Please assign a target.");
        }
    }

    void Update()
    {
        // Calculate the path from the AR Camera's current position to the destination
        if (destination != null && NavMesh.CalculatePath(agent.gameObject.transform.position, destination.position, NavMesh.AllAreas, navPath))
        {
            if (navPath.status == NavMeshPathStatus.PathComplete)
            {
                DrawPath(navPath);
            }
            else
            {
                Debug.LogWarning("Incomplete path detected.");
                lineRenderer.positionCount = 0; // Clear the line if no valid path
            }
        }

        // only show line and enable agent when is localized
        lineRenderer.enabled = StateController.instance.IsLocalized();
        agent.enabled = StateController.instance.IsLocalized();
    }

    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2) return;

        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }
}
