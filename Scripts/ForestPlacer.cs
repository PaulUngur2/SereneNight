using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Inspiration https://www.youtube.com/watch?v=Bv1foWFahM4

public class ForestPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> forestPrefabs; // List of forest prefabs to instantiate
    [SerializeField] private List<GameObject> forests; // List to hold instantiated forest game objects

    [SerializeField] private float scaleWidth, scaleHeight, distance; // Parameters for forest scaling and positioning

    [SerializeField] private LayerMask terrainLayer; // Layer mask for terrain detection

    // Context menu method to place forests
    [ContextMenu("Place Forest")]
    private void PlaceForest()
    {
        // Destroy existing forest game objects
        foreach (var forest in forests)
        {
            if (forest != null)
                DestroyImmediate(forest);
        }
        forests.Clear(); // Clear the list of forests

        int prefabIndex = 0; // Index to select forest prefab from forestPrefabs list
        float angle = 0f; // Initial angle for placing forests around the center

        // Loop to place 8 forests around the center at equal angles
        for (int i = 0; i < 8; i++)
        {
            // Calculate spawn position using angle and distance from the center
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
            
            RaycastHit hit;
            // Raycast downward to find terrain and place forest on it
            if (Physics.Raycast(spawnPosition + Vector3.up * 50, Vector3.down, out hit, 100, terrainLayer))
            {
                // Instantiate forest prefab at hit point on terrain
                GameObject forest = Instantiate(forestPrefabs[prefabIndex], hit.point, Quaternion.Euler(0, angle, 0), transform);
                forests.Add(forest); // Add instantiated forest to forests list
            }

            angle += 45f; // Increment angle for next forest placement
            prefabIndex++; // Move to the next forest prefab in the list

            // Wrap around the prefabIndex to prevent going out of bounds
            if (prefabIndex >= forestPrefabs.Count)
                prefabIndex = 0;
        }
    }

    // Context menu method to set forest scale and position
    [ContextMenu("Set Forest Scale")]
    private void SetForestScale()
    {
        foreach (var forest in forests)
        {
            // Set the scale of the forest game object
            forest.transform.localScale = new Vector3(scaleWidth, scaleHeight, 1f);

            // Calculate distance for positioning based on sprite length
            float spriteLength = forests[0].GetComponent<SpriteRenderer>().bounds.size.x;
            distance = spriteLength / 2f + (Mathf.Sqrt(2) / 2) * spriteLength;
            forest.transform.position = distance * forest.transform.forward;
        }
    }

    // Method called whenever a value in the inspector is changed
    private void OnValidate()
    {
        // Automatically set forest scale if there are exactly 8 forests
        if (forests.Count == 8)
        {
            SetForestScale();
        }
    }
}
