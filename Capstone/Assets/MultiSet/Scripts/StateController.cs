using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateController : MonoBehaviour
{
    public static StateController instance;

    public TextMeshProUGUI stateText;

    bool IsPositionTracked = false;

    void Awake() { instance = this; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Area Target got found, called from DefaultTargetEventHandler (on AT object)
     */
    public void ATFound()
    {
        IsPositionTracked = true;
        stateText.text = "Position tracked";
    }

    /**
     * Area Target lost, called from DefaultTargetEventHandler (on AT object)
     */
    public void ATLost()
    {
        IsPositionTracked=false;
        stateText.text = "Position lost";
    }

    /**
     * Returns state of position. True if Area Target found and currently being tracked.
     */
    public bool IsLocalized() { return IsPositionTracked; }
}
