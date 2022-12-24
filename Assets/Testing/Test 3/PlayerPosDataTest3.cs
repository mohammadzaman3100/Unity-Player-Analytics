using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosDataTest3 : MonoBehaviour
{
    // Dictionary to store the visited positions and the time spent at each position
    private Dictionary<Vector3, float> positions = new Dictionary<Vector3, float>();

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            // Store the current position and the time spent at that position
            var currentPosition = transform.position;
            var currentTime = Time.time;

            if (positions.ContainsKey(currentPosition))
            {
                // Update the time spent at the current position
                var previousTime = positions[currentPosition];
                var timeSpent = currentTime - previousTime;
                positions[currentPosition] = currentTime;
            }
            else
            {
                // Add the current position to the dictionary
                positions.Add(currentPosition, currentTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Create a gradient to map the time spent at each position to a color
        var gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.1f, 0.0f), new GradientAlphaKey(0.1f, 1.0f) }
        );

        foreach (var entry in positions)
        {
            var position = entry.Key;
            var timeSpent = entry.Value;

            var p = position;
            p.x = (int)p.x;
            p.y = (int)p.y;
            p.z = (int)p.z;

            var c = gradient.Evaluate(timeSpent);
            Gizmos.color = c;
            Gizmos.DrawCube(p, Vector3.one);
        }

        foreach (var position in positions.Keys)
        {
            Debug.DrawLine(position, position);
        }
    }
}