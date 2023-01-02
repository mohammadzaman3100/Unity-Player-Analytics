using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerLoadPosDataTest6 : MonoBehaviour
{
     // Declare variables as fields instead of inside a method to avoid re-allocating them every time the method is called
    private readonly List<Vector3> positions = new List<Vector3>();
    private string path;

    private void Start()
    {
        path = Application.dataPath + "/LogPlayerPosDataTest6.txt";
        LoadPositions();
    }

    private void OnDrawGizmos()
    {
        var prev = Vector3.zero;
        var c = Color.yellow;
        for (int i = 0; i < positions.Count; i++)
        {
            // Determine the color of the cube based on its position in the list
            c = Color.Lerp(Color.red, Color.green, (float)i / positions.Count);
            c.a = 0.1f;

            // Draw the cube at the position
            var p = positions[i];
            p.x = (int)p.x;
            p.y = (int)p.y;
            p.z = (int)p.z;
            Gizmos.color = c;
            Gizmos.DrawCube(p, Vector3.one);

            // Draw a line connecting the current position to the previous position
            if (i > 0)
            {
                if (Vector3.Distance(positions[i - 1], positions[i]) > 2)
                    c = Color.black;
                else
                    c = Color.white;
                Debug.DrawLine(positions[i - 1], positions[i]);
                Gizmos.DrawLine(positions[i - 1], positions[i]);
            }
        }
    }

    private void LoadPositions()
    {
        // Check if the file exists
        if (!File.Exists(path))
        {
            Debug.LogError("File does not exist: " + path);
            return;
        }

        // Read the file and split the data into separate JSON strings
        var inData = File.ReadAllText(path);
        var jsonStrings = inData.Split('|');

        // Deserialize each JSON string and add the resulting Vector3 to the list
        foreach (var json in jsonStrings)
        {
            if (json.Length > 0)
            {
                var position = JsonUtility.FromJson<Vector3>(json);
                positions.Add(position);
            }
        }
    }
}
