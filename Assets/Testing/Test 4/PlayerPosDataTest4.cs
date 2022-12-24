using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosDataTest4 : MonoBehaviour
{
    // A dictionary that will store the player's position as the key, and the time at which that
    // position was recorded as the value
    private Dictionary<Vector3, float> positionTimes = new Dictionary<Vector3, float>();
    
    private IEnumerator Start()
    {
        while (true)
        {
            // Wait for half a second before recording the player's position again
            yield return new WaitForSeconds(0.5f);
            
            // Record the current time
            float currentTime = Time.time;
            
            // Record the player's current position
            Vector3 position = transform.position;
            
            // If the player's position has already been recorded, update the time at which it was recorded
            if (positionTimes.ContainsKey(position))
            {
                positionTimes[position] = currentTime;
            }
            else
            {
                // Otherwise, add the position and time to the dictionary
                positionTimes.Add(position, currentTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Iterate through each position and time stored in the dictionary
        foreach (var entry in positionTimes)
        {
            Vector3 position = entry.Key;
            float time = entry.Value;
            
            // Round the position coordinates to the nearest integer
            position.x = (int)position.x;
            position.y = (int)position.y;
            position.z = (int)position.z;

            // Determine the color of the cube based on how much time has passed since the position was recorded
            Color color = Color.yellow;
            if (Time.time - time < 1.0f)
            {
                color = Color.red;
            }
            else if (Time.time - time < 2.0f)
            {
                color = Color.green;
            }
            color.a = 0.1f;
            
            // Draw the cube at the position with the determined color
            Gizmos.color = color;
            Gizmos.DrawCube(position, Vector3.one);
        }
        
        // Iterate through each position in the dictionary and draw a line between each pair of consecutive positions
        List<Vector3> positions = new List<Vector3>(positionTimes.Keys);
        for (int i = 1; i < positions.Count; i++)
        {
            Debug.DrawLine(positions[i - 1], positions[i]);
        }
    }
}