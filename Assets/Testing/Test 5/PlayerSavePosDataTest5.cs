using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Custom data structure to store position and JSON data together
public struct PlayerPositionData
{
    public Vector3 position;
    public string jsonData;
}


public class PlayerSavePosDataTest5 : MonoBehaviour
{
    // List to store player positions and corresponding JSON data
    private readonly List<PlayerPositionData> positionData = new List<PlayerPositionData>();

    // Coroutine to save player positions every 0.25 seconds
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            // Add current position and JSON data to list
            positionData.Add(new PlayerPositionData
            {
                position = transform.position,
                jsonData = JsonUtility.ToJson(transform.position)
            });
        }
    }

    // Method to save data to a database or cloud storage
    private void SaveData()
    {
        // TODO: Implement code to save data to database or cloud storage
    }

    private void OnDisable()
    {
        // Save data when the script is disabled
        SaveData();
    }
}
