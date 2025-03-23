using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShowPath : MonoBehaviour
{
    public static ShowPath instance;

    LineRenderer line;
    NavMeshPath path;
    float _elapsed = 0.0f;

    [Tooltip("Height of the line above NavMesh")]
    public float LINE_HEIGHT_ABOVE_GROUND = 0.1f;

    Transform a = null;
    Transform b = null;

    [Tooltip("Prefab to visualize path corners")]
    public GameObject cornerVisualizationPrefab;

    GameObject[] visibleCorners = { };
    public bool isCornersVisible;
    bool cornerVisibilityHasChanged = false;

    void Awake()
    {
        instance = this;
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        path = new NavMeshPath();
        line.enabled = false;
        isCornersVisible = false;
    }

    void Update()
    {
        if (a != null && b != null)
        {
            line.enabled = true;
            line.SetPosition(0, a.position);
            StartCoroutine(DrawPath(path));
            PathEstimationUtils.instance.UpdateEstimation(path.corners);

            _elapsed += Time.deltaTime;
            if (_elapsed > 0.1f)
            {
                _elapsed = 0f;
                NavMesh.CalculatePath(a.position, b.position, NavMesh.AllAreas, path);
            }
        }
        else
        {
            line.enabled = false;
            SetCornerVisibility(false);
        }

        // if (a != null && b != null && NavigationController.instance.IsCurrentlyNavigating())
        // {
        //      if ( path.status == NavMeshPathStatus.PathInvalid)
        //     {
        //         // ToastManager.Instance.ShowAlert("Problem calculating route");
        //         Debug.LogWarning($"Path status: {path.status} | Agent: {a.position} | Destination: {b.position}");
        //         Debug.LogWarning("Problem Calculating route");
        //         NavigationController.instance.StopNavigation();
        //     }
        // }
    }

    IEnumerator DrawPath(NavMeshPath path)
    {
        yield return new WaitForEndOfFrame();

        if (path.corners.Length < 2)
            yield break;

        line.positionCount = path.corners.Length;

        if (isCornersVisible)
        {
            cornerVisibilityHasChanged = true;
            if (cornerVisibilityHasChanged)
            {
                SetCornerVisibility(true);
            }
            HandlePathCornerVisualization();
        }
        else
        {
            cornerVisibilityHasChanged = true;
            if (cornerVisibilityHasChanged)
            {
                SetCornerVisibility(false);
            }
        }

        for (var i = 0; i < path.corners.Length; i++)
        {
            Vector3 linePosition = new Vector3(path.corners[i].x, path.corners[i].y + LINE_HEIGHT_ABOVE_GROUND, path.corners[i].z);
            line.SetPosition(i, linePosition);

            if (isCornersVisible)
            {
                UpdateVisibleCorner(i, linePosition);
            }
        }

        // Example waypoint logic (optional):
        // float waypointDistance = 2.0f;
        // Vector3 waypointPos = LerpByDistance(path.corners[0], path.corners[1], waypointDistance);
        // Spawn waypoint prefab at waypointPos if needed
    }

    public void ResetPath()
    {
        StopAllCoroutines();
        a = null;
        b = null;
        line.positionCount = 1;
        ClearCornerObjects();
    }

    public void SetPositionFrom(Transform from)
    {
        a = from;
    }

    public void SetPositionTo(Transform to)
    {
        b = to;
        if (b != null)
        {
            NavMesh.CalculatePath(a.position, b.position, NavMesh.AllAreas, path);
        }
    }

    void HandlePathCornerVisualization()
    {
        int pathCornersCount = path.corners.Length;
        if (pathCornersCount != visibleCorners.Length)
        {
            ClearCornerObjects();
            visibleCorners = new GameObject[pathCornersCount];
        }
    }

    void UpdateVisibleCorner(int i, Vector3 newPosition)
    {
        if (visibleCorners[i] == null && cornerVisualizationPrefab != null)
        {
            visibleCorners[i] = Instantiate(cornerVisualizationPrefab, newPosition, Quaternion.identity);
        }
        else if (visibleCorners[i] != null)
        {
            visibleCorners[i].transform.position = newPosition;
        }
    }

    void ClearCornerObjects()
    {
        foreach (var corner in visibleCorners)
        {
            if (corner != null)
            {
                Destroy(corner);
            }
        }
        visibleCorners = new GameObject[0];
    }

    void SetCornerVisibility(bool show)
    {
        cornerVisibilityHasChanged = false;
        foreach (var corner in visibleCorners)
        {
            if (corner != null)
            {
                corner.SetActive(show);
            }
        }
    }

    // Utility for sampling a point along the path
    public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        return x * Vector3.Normalize(B - A) + A;
    }
}



