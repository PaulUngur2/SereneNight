using SunCalcNet;
using SunCalcNet.Model;
using System;
using UnityEngine;
using UnityEngine.UI;

public class setMoonPosition : MonoBehaviour
{
    [SerializeField] int hour = DateTime.Now.Hour; // Initial hour (default to current hour)
    [SerializeField] int minute = DateTime.Now.Minute; // Initial minute (default to current minute)
    [SerializeField] public GameObject timeGameObject; // Reference to XRTime GameObject

    private XRTime xrTime; // Reference to XRTime component for time and location data
    private float latitude; // Current latitude
    private float longitude; // Current longitude

    DateTime date; // Variable to store current date and time
    MoonPosition moonPos; // Variable to store Moon's position data

    // Start is called before the first frame update
    void Start()
    {   
        xrTime = timeGameObject.GetComponent<XRTime>();
        latitude = xrTime.getLatitude();
        longitude = xrTime.getLongitude();
    }

    float crtLat, crtLon; // Variables to store current latitude and longitude
    DateTime crtTime, newTime; // Variables to store current and updated time

    // LateUpdate is called once per frame, after Update
    public void LateUpdate()
    {
        newTime = xrTime.getTime();

        // Only update when the latitude, longitude, or enough time has passed
        if (crtLat != latitude || crtLon != longitude || (Math.Abs((crtTime - newTime).TotalSeconds) > 1))
        {
            crtTime = newTime;
            crtLat = latitude;
            crtLon = longitude;
            UpdateMoonPosition();
        }
    }

    // Method to update Moon's position based on current time and location
    private void UpdateMoonPosition()
    {
        moonPos = MoonCalc.GetMoonPosition(xrTime.getTime(), latitude, longitude); // Calculate Moon's position

        // Set rotation of this GameObject to represent Moon's position in the sky
        transform.eulerAngles = new Vector3((float)(moonPos.Altitude) * Mathf.Rad2Deg,
                                            180 + (float)moonPos.Azimuth * Mathf.Rad2Deg,
                                            0);
    }
}