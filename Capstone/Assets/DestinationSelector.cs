using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DestinationSelector : MonoBehaviour
{
    public Dropdown destinationDropdown;
    private Dictionary<string, Transform> destinationMap = new Dictionary<string, Transform>();
    public Transform playerTransform;

    void Start()
    {
    // Automatically find the player GameObject by tag
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        playerTransform = player.transform;
    }
    else
    {
        Debug.LogError("Player GameObject not found! Make sure it has the 'Player' tag.");
    }

    PopulateDropdown();
    destinationDropdown.onValueChanged.AddListener(delegate { OnDestinationSelected(); });
    }

    void PopulateDropdown()
    {
        destinationDropdown.ClearOptions();

        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Destination");
        List<string> destinationNames = new List<string>();

        foreach (GameObject dest in destinations)
        {
            destinationNames.Add(dest.name);
            destinationMap[dest.name] = dest.transform;
            Debug.Log("Added destination: " + dest.name);
        }

        destinationDropdown.AddOptions(destinationNames);
    }

    public void OnDestinationSelected()
    {
        string selectedName = destinationDropdown.options[destinationDropdown.value].text;
        Debug.Log("Selected destination: " + selectedName);

        if (destinationMap.ContainsKey(selectedName))
        {
            Vector3 destinationPosition = destinationMap[selectedName].position;
            Debug.Log("Destination position: " + destinationPosition);
            ShowPath.instance.UpdatePath(playerTransform.position, destinationPosition);
        }
        else
        {
            Debug.LogError("Destination not found in map: " + selectedName);
        }
    }
}