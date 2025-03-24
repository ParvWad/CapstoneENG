using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : ListItemData
{
    int id;
    public int identification;                  // a unique identification for this point, e.g. room number - INFO: currently ignored in code!
    public string poiName;                      // title of Point of interest (POI)
    public string descriptionKey;                  // description of POI
    public POIType type;                        // type of the POI
    public POICollider poiCollider;             // object for nav mesh agent calculation and detect user arrival
    public POISign sign;                        // sign of POI
    AugmentedSpace space;                       // space in which POI is located



  void Awake()
    {
        Debug.Log("POI Awake called for " + poiName);
        base.listTitle = poiName;
        id = identification; // this can be adapted if you get id from external source
        sign.SetPOI(this);
        poiCollider.SetPOI(this);
    }
   // returns this id
    public int GetId()
    {
        return id;
    }

    public void Arrived()
    {
        if (NavigationController.instance.currentDestination != null && NavigationController.instance.currentDestination.GetId() == id)
        {
            // arrived at the selected POI
            NavigationController.instance.ArrivedAtDestination();
        }
    }


}
public enum POIType { Room, VendingMachine, Exit, Staircase, WashroomA, WashroomB, Elevator }
