// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;

// public class DestinationSelector : MonoBehaviour
// {
//     public Dropdown destinationDropdown;
//     public ARNavController navController;
//     private List<POI> availablePOIs;

//     void Start()
//     {
//         PopulateDropdown();
//         destinationDropdown.onValueChanged.AddListener(delegate { OnDestinationSelected(); });
//     }

//     void PopulateDropdown()
//     {
//         destinationDropdown.ClearOptions();

//         // Find all POIs in the scene
//         availablePOIs = new List<POI>(FindObjectsOfType<POI>());
//         List<string> destinationNames = new List<string>();

//         foreach (POI poi in availablePOIs)
//         {
//             destinationNames.Add(poi.poiName);
//             Debug.Log("Added POI to dropdown: " + poi.poiName);
//         }

//         destinationDropdown.AddOptions(destinationNames);
//     }

//     public void OnDestinationSelected()
//     {
//         int selectedIndex = destinationDropdown.value;
//         if (selectedIndex >= 0 && selectedIndex < availablePOIs.Count)
//         {
//             POI selectedPOI = availablePOIs[selectedIndex];
//             Debug.Log("Selected destination: " + selectedPOI.poiName);
//             navController.SetPOIForNavigation(selectedPOI);
//         }
//         else
//         {
//             Debug.LogError("Selected POI index out of range");
//         }
//     }
// }
