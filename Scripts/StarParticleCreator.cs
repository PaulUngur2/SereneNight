using System;
using System.Globalization;
using System.IO;
using UnityEngine;
using stardata;
using UnityEngine.UI;

// Based on https://thomaskole.nl/2016/07/01/star-field-generator/
// For realistic Sun https://www.youtube.com/watch?v=BUi2PpR4u6o
// For star colors http://www.vendian.org/mncharity/dir3/starcolor/
public class StarParticleCreator : MonoBehaviour
{
    private ParticleSystem particleSystem; // Reference to the ParticleSystem component
    [SerializeField] public int maxParticles; // Maximum number of particles to be emitted
    [SerializeField] public GameObject timeGameObject; // GameObject holding XRTime component
    private XRTime xrTime; // Reference to XRTime component for time and location data
    private float latitude; // Current latitude
    private float longitude; // Current longitude

    private float fadeLevel; // Variable for managing fade level

    void Awake()
    {
        // Initialize ParticleSystem component and set maximum particles
        particleSystem = GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.maxParticles = maxParticles;

        // Set burst configuration for the ParticleSystem
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[1];
        bursts[0].minCount = (short)maxParticles;
        bursts[0].maxCount = (short)maxParticles;
        bursts[0].time = 0.0f;
        particleSystem.emission.SetBursts(bursts, 1);
    }

    String fileContent; // Variable to hold contents of the stars file
    
    void Start()
    {
        // Get XRTime component and retrieve initial latitude and longitude
        xrTime = timeGameObject.GetComponent<XRTime>();
        latitude = xrTime.getLatitude();
        longitude = xrTime.getLongitude();

        // Load star data from resources
        fileContent = Resources.Load<TextAsset>("stars").text;

        // Load particles initially based on current time, latitude, and longitude
        LoadParticles(xrTime.getTime(), latitude, longitude);

        // Store current latitude, longitude, and time for comparison in LateUpdate
        crtLat = latitude;
        crtLon = longitude;
        crtTime = xrTime.getTime();

        // Update stars initially based on current time, latitude, and longitude
        UpdateStars(newTime, latitude, longitude);
    }

    float crtLat, crtLon; // Variables to store current latitude and longitude
    DateTime crtTime, newTime; // Variables to store current and updated time

    // LateUpdate method to continuously update stars when parameters change
    public void LateUpdate()
    {
        newTime = xrTime.getTime();

        // Update stars if latitude, longitude, or time have changed significantly
        if (crtLat != latitude || crtLon != longitude || (Math.Abs((crtTime - newTime).TotalSeconds) > 1))
        {
            UpdateStars(newTime, latitude, longitude);
            crtTime = newTime;
            crtLat = latitude;
            crtLon = longitude; 
        }
    }

    private float alt, az, dist, r, g, b; // Variables for altitude, azimuth, distance, and color components
    string[] components; // Array to hold components of each star data line

    // Method to load particles (stars) from data based on current time and location
    public void LoadParticles(DateTime time, float latitude, float longitude)
    {
        String[] lines = fileContent.Split('\n'); // Split file content into lines

        for (int i = 1; i < maxParticles; i++) // Loop through each line (excluding header)
        {
            components = lines[i].Split(','); // Split line into components based on ',' delimiter

            // Calculate altitude and azimuth for the star using star position utility methods
            alt = StarPosition.getAltitude(float.Parse(components[4], NumberStyles.Any, CultureInfo.InvariantCulture) * 15,
                                           float.Parse(components[5], NumberStyles.Any, CultureInfo.InvariantCulture),
                                           latitude, longitude,
                                           time) * Mathf.Deg2Rad;
            az = StarPosition.getAzimuth(float.Parse(components[4], NumberStyles.Any, CultureInfo.InvariantCulture) * 15,
                                        float.Parse(components[5], NumberStyles.Any, CultureInfo.InvariantCulture),
                                        latitude, longitude,
                                        time) * Mathf.Deg2Rad;
            dist = float.Parse(components[6], NumberStyles.Any, CultureInfo.InvariantCulture); // Distance of the star

            // https://math.stackexchange.com/questions/15323/how-do-i-calculate-the-cartesian-coordinates-of-stars
            // Determine color based on spectral type of the star
            switch (components[9][0])
            {
                case 'O':
                    r = 155f / 255f;
                    g = 176f / 255f;
                    b = 255f / 255f;
                    if (components[9].Length < 2)
                    {
                        break;
                    }
                    switch (components[9][1])
                    {
                        case '5':
                            r = 157f / 255f;
                            g = 180f / 255f;
                            b = 255f / 255f;
                            break;
                    }
                    break;
                case 'B':
                    r = 170f / 255f;
                    g = 191f / 255f;
                    b = 255f / 255f;
                    if (components[9].Length < 2)
                    {
                        break;
                    }
                    switch (components[9][1])
                    {
                        case '1':
                            r = 162f / 255f;
                            g = 185f / 255f;
                            b = 255f / 255f;
                            break;
                        case '3':
                            r = 167f / 255f;
                            g = 188f / 255f;
                            b = 255f / 255f;
                            break;
                        case '5':
                            r = 170f / 255f;
                            g = 191f / 255f;
                            b = 255f / 255f;
                            break;
                        case '8':
                            r = 175f / 255f;
                            g = 195f / 255f;
                            b = 255f / 255f;
                            break;
                    }
                    break;
                case 'A':
                    r = 202f / 255f;
                    g = 215f / 255f;
                    b = 255f / 255f;
                    if (components[9].Length < 2)
                    {
                        break;
                    }
                    switch (components[9][1])
                    {
                        case '1':
                            r = 186f / 255f;
                            g = 204f / 255f;
                            b = 255f / 255f;
                            break;
                        case '3':
                            r = 192f / 255f;
                            g = 209f / 255f;
                            b = 255f / 255f;
                            break;
                        case '5':
                            r = 202f / 255f;
                            g = 216f / 255f;
                            b = 255f / 255f;
                            break;
                    }
                    break;
                case 'F':
                    r = 248f / 255f;
                    g = 247f / 255f;
                    b = 255f / 255f;
                    if (components[9].Length < 2)
                    {
                        break;
                    }
                    switch (components[9][1])
                    {
                        case '0':
                            r = 228f / 255f;
                            g = 232f / 255f;
                            b = 255f / 255f;
                            break;
                        case '2':
                            r = 237f / 255f;
                            g = 238f / 255f;
                            b = 255f / 255f;
                            break;
                        case '5':
                            r = 251f / 255f;
                            g = 248f / 255f;
                            b = 255f / 255f;
                            break;
                        case '8':
                            r = 255f / 255f;
                            g = 249f / 255f;
                            b = 249f / 255f;
                            break;
                    }
                    break;

                case 'G':
                    r = 255f / 255f;
                    g = 244f / 255f;
                    b = 234f / 255f;
                    if (components[9].Length < 2)
                    {
                        break;
                    }
                    switch (components[9][1])
                    {
                        case '2':
                            r = 255f / 255f;
                            g = 245f / 255f;
                            b = 236f / 255f;
                            break;
                        case '5':
                            r = 255f / 255f;
                            g = 244f / 255f;
                            b = 232f / 255f;
                            break;
                        case '8':
                            r = 255f / 255f;
                            g = 241f / 255f;
                            b = 223f / 255f;
                            break;
                    }
                    break;
                case 'K':
                    r = 255f / 255f;
                    g = 210f / 255f;
                    b = 161f / 255f;
                    if (components[9].Length < 2)
                    {
                        break;
                    }
                    switch (components[9][1])
                    {
                        case '0':
                            r = 255f / 255f;
                            g = 235f / 255f;
                            b = 209f / 255f;
                            break;
                        case '4':
                            r = 255f / 255f;
                            g = 215f / 255f;
                            b = 174f / 255f;
                            break;
                        case '7':
                            r = 255f / 255f;
                            g = 198f / 255f;
                            b = 144f / 255f;
                            break;
                    }
                    break;
                case 'M':
                    r = 255f / 255f;
                    g = 204f / 255f;
                    b = 111f / 255f;
                    if (components[9].Length < 2)
                    {
                        break;
                    }
                    switch (components[9][1])
                    {
                        case '2':
                            r = 255f / 255f;
                            g = 190f / 255f;
                            b = 127f / 255f;
                            break;
                        case '4':
                        case '6':
                            r = 255f / 255f;
                            g = 187f / 255f;
                            b = 123f / 255f;
                            break;
                    }
                    break;
            }
            // Add star to the Stars collection with parsed and calculated values
            Stars.addStar(components[3].Trim() == "" ? components[0] : components[3], // Star name or identifier
                          float.Parse(components[4]), // Right Ascension (RA)
                          float.Parse(components[5]), // Declination (DEC)
                          alt * Mathf.Rad2Deg, // Altitude in degrees
                          az * Mathf.Rad2Deg, // Azimuth in degrees
                          float.Parse(components[7]), // Visual magnitude
                          dist, // Distance
                          dist * Mathf.Cos(alt) * Mathf.Cos(az), // X position in Cartesian coordinates
                          dist * Mathf.Cos(alt) * Mathf.Sin(az), // Y position in Cartesian coordinates
                          dist * Mathf.Sin(alt), // Z position in Cartesian coordinates
                          r, g, b // RGB color components
                         );
        }
    }
    Star star; // Variable to hold current star being updated

    // Method to update stars based on current time and location
    public void UpdateStars(DateTime time, float latitude, float longitude)
    {
        Stars.resetNumberVisibleStars(); // Reset count of visible stars

        // Loop through each star and update its position and visibility
        for (int i = 1; i < Stars.getNumberOfStars(); i++)
        {
            star = Stars.GetStar(i); // Get star object from Stars collection

            // Calculate altitude and azimuth for the star
            alt = StarPosition.getAltitude(star.getRA() * 15, // Convert RA to degrees
                                           star.getDec(), // Get declination
                                           latitude, longitude, // Current latitude and longitude
                                           time) * Mathf.Deg2Rad; // Convert altitude to radians
            az = (StarPosition.getAzimuth(star.getRA() * 15, // Convert RA to degrees
                                         star.getDec(), // Get declination
                                         latitude, longitude, // Current latitude and longitude
                                         time) + 90) * Mathf.Deg2Rad; // Convert azimuth to radians, +90 for scene orientation

            dist = star.getDistance(); // Get star's distance

            // Update star's position and other properties in the Stars collection
            Stars.UpdateStar(i,
                             alt * Mathf.Rad2Deg, // Convert altitude back to degrees
                             az * Mathf.Rad2Deg, // Convert azimuth back to degrees
                             dist * Mathf.Cos(alt) * Mathf.Cos(az), // Calculate new X position
                             dist * Mathf.Cos(alt) * Mathf.Sin(az), // Calculate new Y position
                             dist * Mathf.Sin(alt)); // Calculate new Z position

            // Increment count of visible stars if the star's altitude is greater than 0
            if (star.getAltitude() > 0)
            {
                Stars.incrementVisibleStars();
            }
        }
    }

    // Method to read text from a file (not currently used in the provided code)
    String ReadTextFile(string file_path)
    {
        String text = "";
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            text += inp_stm.ReadLine() + "\n";
        }

        inp_stm.Close();
        return text;
    }
}