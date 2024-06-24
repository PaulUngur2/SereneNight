using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    // Helper script to place the object on the terrain level
    public float heightAboveTerrain = 1.0f; 

    void Update()
    {
        // Create a ray starting from above the object's current position, pointing downwards
        Ray ray = new Ray(transform.position + Vector3.up * 500, Vector3.down);
        RaycastHit hit;

        // Check if the ray hits any collider
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Terrain"))
            {
                // Adjust the object's position to be a set height above the terrain
                transform.position = new Vector3(transform.position.x, hit.point.y + heightAboveTerrain, transform.position.z);
            }
        }
    }
}
