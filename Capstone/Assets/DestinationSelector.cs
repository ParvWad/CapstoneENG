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
        PopulateDropdown();
        //destinationDropdown.onValueChanged.AddListener(delegate { NavigateToDestination(); });
        // Find all objects tagged as "destination" and add them to the dictionary
        
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("destination");
        
        foreach (GameObject dest in destinations)
        {
            destinationMap.Add(dest.name, dest.transform);
            destinationDropdown.options.Add(new Dropdown.OptionData(dest.name));
        }

        destinationDropdown.onValueChanged.AddListener(delegate { OnDestinationSelected(); });
    }
        
        void PopulateDropdown()
    {
        // Clear existing options
        destinationDropdown.ClearOptions();

        // Find all objects with the "Destination" tag
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Destination");

        List<string> destinationNames = new List<string>();

        foreach (GameObject dest in destinations)
        {
            destinationNames.Add(dest.name);
            destinationMap[dest.name] = dest.transform;
        }

        // Add to dropdown
        destinationDropdown.AddOptions(destinationNames);
    }
    public void OnDestinationSelected()
    {
        string selectedName = destinationDropdown.options[destinationDropdown.value].text;

        if (destinationMap.ContainsKey(selectedName))
        {
            Vector3 destinationPosition = destinationMap[selectedName].position;
            ShowPath.instance.UpdatePath(playerTransform.position, destinationPosition);
        }
    }
}
