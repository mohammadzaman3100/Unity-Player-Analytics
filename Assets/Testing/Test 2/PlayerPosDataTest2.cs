using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosDataTest2 : MonoBehaviour
{
    private float elapsedTime;

    private GameObject heatMap;
    private Vector3 lastPosition = Vector3.zero;
    private readonly Dictionary<Vector3, int> positions = new Dictionary<Vector3, int>();

    private IEnumerator Start()
    {
        // Create a new GameObject to hold the heat map.
        heatMap = new GameObject("HeatMap");

        while (true)
        {
            // Update the elapsed time.
            elapsedTime += Time.deltaTime;

            // Check if the player has moved to a new position.
            if (transform.position != lastPosition)
            {
                // Update the last position.
                lastPosition = transform.position;

                // Check if the player has visited this position before.
                if (positions.ContainsKey(lastPosition))
                    // Increment the count for this position.
                    positions[lastPosition] += 1;
                else
                    // Add a new entry for this position.
                    positions.Add(lastPosition, 1);

                // Update the heat map.
                UpdateHeatMap();
            }

            yield return null;
        }
    }

    private void UpdateHeatMap()
    {
        // Clear the current heat map.
        foreach (Transform child in heatMap.transform) Destroy(child.gameObject);

        // Draw new cubes for each position in the dictionary.
        foreach (var position in positions)
        {
            var p = position.Key;
            p.x = (int)p.x;
            p.y = (int)p.y;
            p.z = (int)p.z;

            // Calculate the color for the cube based on the number of times the player has visited this position.
            var c = Color.Lerp(Color.blue, Color.red, position.Value / 10.0f);
            c.a = 0.5f;

            // Create a new cube at the current position and add it to the heat map.
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = p;
            cube.transform.localScale = Vector3.one;
            cube.GetComponent<Renderer>().material.color = c;
            cube.transform.SetParent(heatMap.transform, false);
        }
    }
}