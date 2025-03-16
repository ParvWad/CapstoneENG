using UnityEngine;
using UnityEngine.AI;

/**
 * Keeps NavMeshAgent always on NavMesh where AR camera is currently located.
 */
public class NavmeshAgentAnchoring : MonoBehaviour
{
    /** ARCamera GameObject **/
    GameObject ARcamera;

    /** NavMeshAgent component on same GameObject **/
    NavMeshAgent agent;

    void Awake()
    {
        ARcamera = Camera.main.gameObject;
        agent = GetComponent<NavMeshAgent>();
        // TODO: mesh = GetComponent<MeshRenderer>();
        // TODO: mesh.enabled = false;
    }

    void Update()
    {
        //if (ARStateController.instance.IsLocalized())
        //{
        // teleport agent to camera x & z, but take y from agent object
        agent.Warp(new Vector3(ARcamera.transform.position.x, agent.gameObject.transform.position.y, ARcamera.transform.position.z));
    }
}
