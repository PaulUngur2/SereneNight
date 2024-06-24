using Normal.Realtime;
using SunCalcNet;
using SunCalcNet.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using stardata;

public class XRTime : MonoBehaviour
{
    // References to the light sources for the sun and moon
    [SerializeField] private Light sunLight;
    [SerializeField] private Light moonLight;
    
    // References to the skybox materials for day and night
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;
    
    // Reference to the Transform that represents the stars in the sky
    [SerializeField] private Transform stars;
    
    // References to the main camera for VR and non-VR modes
    [SerializeField] public Camera MainCamera;
    [SerializeField] public Camera MainCameraNonVR;
    
    // Latitude and longitude for the location
    [SerializeField] public float latitude = 34.0522f;
    [SerializeField] public float longitude = -118.2437f;
    
    // Time and time limit for the simulation
    [SerializeField] public int time = 00;
    [SerializeField] public int timeLimit = 6;
    
    // Variables to sync XR data and store sun and moon positions
    XRDataSync model;
    SunPosition sunPos;
    MoonPosition moonPos;
    bool useCrtTime = true;
    
    // Starting time and duration of a day in the simulation (20 minutes)
    private DateTime startTime;
    private float dayDurationInSeconds = 1200f; // 20 minutes for dawn to dusk

    // Initialize the start time to the current system time
    private void Awake()
    {
        startTime = DateTime.Now;
    }

    // Set the initial skybox to the day skybox
    void Start()
    {
        RenderSettings.skybox = daySkybox;
    }
    
    int secs = 0;

    // Calculate the simulated time based on the elapsed time since the start
    public DateTime getTime()
    {
        float elapsedSeconds = (float)(DateTime.Now - startTime).TotalSeconds;

        // Reset the start time if the elapsed time exceeds the duration of a day
        if (elapsedSeconds > dayDurationInSeconds)
        {
            startTime = DateTime.Now;
            elapsedSeconds = 0;
        }

        // Calculate the number of hours to add based on the elapsed time
        double hoursToAdd = (elapsedSeconds / dayDurationInSeconds) * 6;
        
        // Calculate the simulated time starting from the specified 'time'
        DateTime simulatedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, time, 0, 0).AddHours(hoursToAdd);

        // Check if the simulated time has reached the time limit
        if (simulatedTime.Hour >= timeLimit && simulatedTime.Hour < timeLimit + 1)
        {
            ExitGame();
        }
        
        Debug.Log(simulatedTime);
        return simulatedTime;
    }   

    void ExitGame()
    {
        Debug.Log("Time Passed");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public float getLatitude()
    {
        return latitude;
    }
    
    public float getLongitude()
    {
        return longitude;
    }
}
