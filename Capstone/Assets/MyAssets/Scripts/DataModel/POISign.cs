using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Represents sign of poi.
 * Handles clicking on sign of poi.
 */
public partial class POISign : MonoBehaviour
{
    POI poi; // corresponding poi

    //public Image imageNormal;
    //public Image imageLibrary;

    public TextMeshProUGUI title;

    public Material blue;
    public Material green;

    public Image backgroundSignImage;

    bool toggle = false;

    const float MAX_SIGN_CLICK_DISTANCE = 7.5f; // max distance allowed to click sign from

    private void HandleClick()
    {
        toggle = !toggle;
        if (toggle)
        {
            backgroundSignImage.material = blue;
        }
        else
        {
            backgroundSignImage.material = green;
        }

        // Your normal UI click handling code here
        float distanceToSign = Vector3.Distance(Camera.main.transform.position, gameObject.transform.position);
        if (distanceToSign > MAX_SIGN_CLICK_DISTANCE)
        {
            return;
        }
        Debug.Log("Clicked POI: " + poi.poiName);
    }

    /**
     * Set poi from parent
     */
    public void SetPOI(POI aPoi)
    {
        if (aPoi == null)
        {
            Debug.LogError("POISign.SetPOI received NULL POI");
            return;
        }

        poi = aPoi;

        if (title != null)
        {
            title.text = aPoi.poiName; // Safe now
        }
        else
        {
            Debug.LogWarning("POISign title is not assigned in Inspector!");
        }
    }

}

#if UNITY_EDITOR || UNITY_STANDALONE
/**
 * Represents sign of poi.
 * Handles clicking on sign of poi.
 */
public partial class POISign : MonoBehaviour, IPointerClickHandler
{
    // Use OnPointerClick for desktop platforms
    public void OnPointerClick(PointerEventData eventData)
    {
        HandleClick();
    }
}

#else

/**
 * Represents sign of poi.
 * Handles clicking on sign of poi.
 */
public partial class POISign : MonoBehaviour, IPointerDownHandler
{
    // Use OnPointerDown for mobile platforms
    public void OnPointerDown(PointerEventData eventData)
    {
        HandleClick();
    }
}

#endif