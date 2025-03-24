using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CirclePath : MonoBehaviour
{
    public static CirclePath instance;

    NavMeshPath path;
    float _elapsed = 0.0f;

    [Tooltip("Height offset for each marker above ground")]
    public float LINE_HEIGHT_ABOVE_GROUND = 0.1f;

    Transform a = null;
    Transform b = null;

    [Tooltip("Prefab for each path marker (circle, footstep, etc.)")]
    public GameObject pathMarkerPrefab;

    [Tooltip("Distance between path markers")]
    public float markerSpacing = 1.0f;

    private List<GameObject> activeMarkers = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        path = new NavMeshPath();
    }

    void Update()
    {
        if (a != null && b != null)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > 0.2f)
            {
                _elapsed = 0f;
                NavMesh.CalculatePath(a.position, b.position, NavMesh.AllAreas, path);
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    DrawPathMarkers(path);
                }
            }
        }
        else
        {
            ClearMarkers();
        }
    }

    void DrawPathMarkers(NavMeshPath path)
    {
        ClearMarkers();

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Vector3 start = path.corners[i];
            Vector3 end = path.corners[i + 1];
            float segmentDistance = Vector3.Distance(start, end);
            int numMarkers = Mathf.CeilToInt(segmentDistance / markerSpacing);

            for (int j = 0; j <= numMarkers; j++)
            {
                Vector3 markerPos = Vector3.Lerp(start, end, (float)j / numMarkers);
                markerPos.y += LINE_HEIGHT_ABOVE_GROUND;
                GameObject marker = Instantiate(pathMarkerPrefab, markerPos, Quaternion.identity);
                activeMarkers.Add(marker);
            }
        }
    }

    void ClearMarkers()
    {
        foreach (var marker in activeMarkers)
        {
            Destroy(marker);
        }
        activeMarkers.Clear();
    }

    public void ResetPath()
    {
        StopAllCoroutines();
        a = null;
        b = null;
        ClearMarkers();
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
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                DrawPathMarkers(path);
            }
        }
    }
}
