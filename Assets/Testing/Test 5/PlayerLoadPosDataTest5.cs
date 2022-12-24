using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadPosDataTest5 : MonoBehaviour
{
    // List to store player positions
    private readonly List<Vector3> positions = new List<Vector3>();

    // 3D grid of cubes to display heat map
    public GameObject cubePrefab;
    public int gridSize = 10;

    private void Start()
    {
        // Load player position data from database or cloud storage
        // TODO: Implement code to load data from database or cloud storage

        // Create a grid of cubes and set their initial colors to transparent
        for (int x = -gridSize / 2; x < gridSize / 2; x++)
        {
            for (int y = -gridSize / 2; y < gridSize / 2; y++)
            {
                for (int z = -gridSize / 2; z < gridSize / 2; z++)
                {
                    Vector3 position = new Vector3(x, y, z);
                    GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                    cube.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
                }
            }
        }
    }

    private void Update()
    {
        // Update the colors of the cubes based on the player's visit frequency
        foreach (Vector3 position in positions)
        {
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            int z = Mathf.RoundToInt(position.z);
            GameObject cube = GameObject.Find($"Cube ({x}, {y}, {z})");
            if (cube != null)
            {
                // Increase the alpha value of the cube's color based on the player's visit frequency
                Color color = cube.GetComponent<Renderer>().material.color;
                color.a += 0.1f;
                cube.GetComponent<Renderer>().material.color = color;
            }
        }
    }
}