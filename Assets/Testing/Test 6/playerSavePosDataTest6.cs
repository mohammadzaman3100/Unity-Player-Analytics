using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerSavePosDataTest6 : MonoBehaviour
{
    // Declare variables as fields instead of inside a method to avoid re-allocating them every time the method is called
    private readonly List<Vector3> positions = new List<Vector3>();
    private string path;
    private Vector3 previousPosition;
    private Coroutine saveCoroutine;

    public void Awake()
    {
        path = Application.dataPath + "/LogPlayerPosDataTest6.txt";
        if (!File.Exists(path)) File.WriteAllText(path, "");

        // Store the initial position as the previous position
        previousPosition = transform.position;
    }

    private void OnEnable()
    {
        saveCoroutine = StartCoroutine(SavePositions());
    }

    private void OnDisable()
    {
        StopCoroutine(saveCoroutine);
        var vectorJSON = new List<string>();
        foreach (var vec in positions) vectorJSON.Add(JsonUtility.ToJson(vec));

        var data = string.Join("|", vectorJSON);
        File.AppendAllText(path, data + '|');
    }

    private IEnumerator SavePositions()
    {
        while (true)
        {
            // Check if the player has moved since the last frame
            if (transform.position != previousPosition)
            {
                // Record the current position
                positions.Add(transform.position);

                // Update the previous position
                previousPosition = transform.position;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
