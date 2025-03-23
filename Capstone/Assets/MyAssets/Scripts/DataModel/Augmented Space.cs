using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Represents an area in real life but in the digital world.
 */
public class AugmentedSpace : MonoBehaviour
{
    [Tooltip("Title of the space")]
    public string title;

    // POIs inside this space
    POI[] pois = { };

    [Tooltip("Parent of the POIs inside this space")]
    public GameObject augmentation;

    void Awake()
    {
        pois = augmentation.GetComponentsInChildren<POI>(true);
        foreach (var poi in pois)
        {
            Debug.Log("Found POI: " + poi.poiName);
        }
    }

    // Returns POIs of this space.
    public POI[] GetPOIs()
    {
        return pois;
    }
}
