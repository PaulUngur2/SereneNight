using UnityEngine;
using SunCalcNet;
using SunCalcNet.Model;
using System;
using stardata;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;

public class setSunPosition : MonoBehaviour
{
    [SerializeField] private Transform stars; // Reference to the Transform of stars GameObject

    [SerializeField] public GameObject timeGameObject; // GameObject containing XRTime component

    [SerializeField] public GameObject meteor; // GameObject for meteor effect

    private XRTime xrTime; // Reference to XRTime component for time and location data
    private float latitude; // Current latitude
    private float longitude; // Current longitude

    DateTime time; // Variable to store current time
    SunPosition sunPos; // Variable to store Sun's position data
    ParticleSystem starParticles; // Reference to ParticleSystem component of stars

    // Start is called before the first frame update
    void Start()
    {
        if (meteor != null)
        {
            meteor.SetActive(false);
        }
        xrTime = timeGameObject.GetComponent<XRTime>();
        latitude = xrTime.getLatitude();
        longitude = xrTime.getLongitude();
        starParticles = stars.GetComponent<ParticleSystem>(); // Get ParticleSystem component from stars
    }

    float crtLat, crtLon; // Variables to store current latitude and longitude
    DateTime crtTime, newTime; // Variables to store current and updated time
    private float updateInterval = 1f / 30f; // Interval for 30 FPS
    private float lastUpdateTime = 0f;

    void LateUpdate()
    {
        newTime = xrTime.getTime();

        // Check if enough time has passed to update
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            // Only update when the latitude, longitude, or enough time has passed
            if (crtLat != latitude || crtLon != longitude || (Math.Abs((newTime - crtTime).TotalSeconds) > 1))
            {
                UpdateSunPosition();
                crtTime = newTime;
                crtLat = latitude;
                crtLon = longitude;
            }
            lastUpdateTime = Time.time;
        }
    }

    ParticleSystem.Particle[] particles; // Array to hold ParticleSystem particles
    float fadeStarsValue; // Variable to control star fading effect
    Star star; // Variable to hold current star being updated
    bool firstPass = true; // Flag to determine first update pass
    MainModule main; // Reference to ParticleSystem's MainModule

    // Method to update Sun's position and adjust star particles
    private void UpdateSunPosition()
    {
        time = xrTime.getTime();

        sunPos = SunCalc.GetSunPosition(time, latitude, longitude); // Calculate Sun's position

        // Adjust culling mask for light based on Sun's altitude
        if (sunPos.Altitude < 0)
        {
            // Disable terrain and player layers from light culling mask
            GetComponent<Light>().cullingMask &= ~(1 << LayerMask.NameToLayer("Terrain"));
            GetComponent<Light>().cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));

            if (meteor != null)
            {
                meteor.SetActive(true); // Activate meteor effect if available
            }
        }
        else
        {
            // Enable terrain and player layers in light culling mask
            GetComponent<Light>().cullingMask |= 1 << LayerMask.NameToLayer("Terrain");
            GetComponent<Light>().cullingMask |= 1 << LayerMask.NameToLayer("Player");
        }

        // Set rotation of this GameObject to represent Sun's position in the sky
        transform.eulerAngles = new Vector3((float)(sunPos.Altitude) * Mathf.Rad2Deg,
                                            180 + (float)sunPos.Azimuth * Mathf.Rad2Deg,
                                            0);

        // Fade stars based on Sun's altitude
        if (sunPos.Altitude * Mathf.Rad2Deg <= 0 && sunPos.Altitude * Mathf.Rad2Deg >= -12f)
        {
            fadeStarsValue = (Mathf.Abs((float)sunPos.Altitude) * Mathf.Rad2Deg) / 12f;
        }
        if (sunPos.Altitude * Mathf.Rad2Deg > 0)
        {
            fadeStarsValue = 0f;
        }
        if (sunPos.Altitude * Mathf.Rad2Deg < -12f)
        {
            fadeStarsValue = 1f;
        }

        // Render stars
        particles = new ParticleSystem.Particle[Stars.getNumberVisibleStars()]; // Initialize particle array
        int p = 0; // Counter for particles array
        for (int i = 0; i < Stars.getNumberOfStars(); i++)
        {
            if (p >= particles.Length)
                continue; // Skip if particles array is full

            star = Stars.GetStar(i); // Get star data from Stars collection

            // Check if star is visible or on the first update pass
            if (star.isVisible() || firstPass)
            {
                // Set particle position in normalized space and scale by 1500
                particles[p].position = new Vector3(star.getX(), star.getY(), star.getZ()).normalized * 1500;

                // Calculate star's size based on visual magnitude
                particles[p].startSize = 200 * Mathf.Pow(10, (-1.44f - Stars.getVisualMagnitudeAfterExtinction(p)) / 5);
            }

            // Set star's color with fading effect based on Sun's altitude
            particles[p].startColor = new Color(star.getR(), star.getG(), star.getB(), fadeStarsValue * particles[p].startSize);

            p++; // Increment particle array index
        }

        firstPass = false; // Set firstPass to false after the first update
        main = starParticles.main; // Get main module of ParticleSystem
        main.maxParticles = particles.Length; // Set maximum particles in ParticleSystem
        starParticles.SetParticles(particles, particles.Length); // Set particles in ParticleSystem
    }
}