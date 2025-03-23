using UnityEngine;
using UnityEngine.Events;

public class ARStateController : MonoBehaviour
{
    public static ARStateController instance;

    public UnityEvent PositionFoundEvent = new UnityEvent();
    // public UnityEvent PositionLostEvent = new UnityEvent();

    private bool isLocalized = false;
    public POI pendingPOI = null;

    private void Awake()
    {
        Debug.Log("Awake called for arstatecontroller");
        instance = this;
        #if UNITY_EDITOR
        // Automatically "localize" in Editor mode for testing
        isLocalized = true;
        Debug.Log("[ARStateController] Running in Editor - Localization forced TRUE");
        #endif
    }

    // This method is now generic, no reference to "MultisetSdkManager"
    public void TriggerLocalization(GameObject multiSetObject)
    {
        if (multiSetObject == null)
        {
            Debug.LogError("Multiset prefab GameObject reference is missing.");
            return;
        }

        // Assuming the prefab has a method called LocalizeFrame()
        multiSetObject.SendMessage("LocalizeFrame", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Localization requested via SendMessage.");
    }

    // Called from Inspector (Localization Success UnityEvent)
    public void OnLocalizationSuccess()
    {
        Debug.Log("StateController on localizationsuccsess");
        if (isLocalized) return;
        isLocalized = true;

        Debug.Log("Localization successful!");
        PositionFoundEvent.Invoke();

        if (pendingPOI != null)
        {
            NavigationController.instance.SetPOIForNavigation(pendingPOI);
            pendingPOI = null;
        }

        // if (NotificationController.instance != null)
        //     NotificationController.instance.ShowNewNotification("Localization Successful");

        // if (NavUIController.instance != null)
        //     NavUIController.instance.SetLocalizationStatus("Position is being tracked");
    }

    public bool IsLocalized() => isLocalized;
}
