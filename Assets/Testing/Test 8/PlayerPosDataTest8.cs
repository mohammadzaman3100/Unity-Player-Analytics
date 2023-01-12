using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosDataTest8 : MonoBehaviour
{

    public List<Vector3> Positions = new List<Vector3>();

    private Vector3 previousPosition;

    // Grid size
    public float gridSize = 0.1f;
    
    //cube size 
    public float cubesize = 0.8f;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        // Round the current position to the nearest grid point
        Vector3 currentPosition = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            Mathf.Round(transform.position.y / gridSize) * gridSize,
            Mathf.Round(transform.position.z / gridSize) * gridSize
        );

        // Only add the position if it has changed
        if (currentPosition != previousPosition)
        {
            Positions.Add(currentPosition);
            previousPosition = currentPosition;
        }
    }

    /*private void OnDrawGizmos()
    {
        foreach (var position in Positions)
        {
            var c = Color.yellow;
            c.a = 0.1f;
            Gizmos.color = c;
            Gizmos.DrawCube(position, new Vector3(cubesize,cubesize, cubesize));
        }

        for (var i = 1; i < Positions.Count; i++) Debug.DrawLine(Positions[i - 1], Positions[i]);
    }*/
    
    private void OnDrawGizmos()
    {
        // Dictionary to keep track of the number of times each position is visited
        Dictionary<Vector3, int> positionCount = new Dictionary<Vector3, int>();

        foreach (var position in Positions)
        {
            if (!positionCount.ContainsKey(position))
            {
                positionCount[position] = 0;
            }
            positionCount[position]++;

            // Set the color of the cube based on the number of times the position is visited
            Color c = Color.green;
            if (positionCount[position] > 1)
            {
                float lerp = (float)(positionCount[position]-1) / (float)(10);
                c = Color.Lerp(new Color(0, 1, 0, 0.1f), new Color(1,1,0,0.8f), lerp);
                if (positionCount[position] >= 10)
                {
                    c = Color.red;
                }
            }
            Gizmos.color = c;
            Gizmos.DrawCube(position, new Vector3(cubesize,cubesize,cubesize));
        }

        for (var i = 1; i < Positions.Count; i++)
        {
            Debug.DrawLine(Positions[i - 1], Positions[i]);
        }
    }

}
