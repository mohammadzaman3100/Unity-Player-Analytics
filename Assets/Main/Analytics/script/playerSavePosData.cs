using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerSavePosData : MonoBehaviour
{
    private readonly List<Vector3> Positions = new List<Vector3>();
    private string path;

    public void Awake()
    {
        path = Application.dataPath + "/LogPlayerPosData.txt";
        if (!File.Exists(path)) File.WriteAllText(path, "");

        // LoadPos();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            Positions.Add(transform.position);
        }
    }

    private void OnDisable()
    {
        // testing saving data 
        Debug.Log($"Positions {Positions[0].ToString()}");
        var vectorJSON = new List<string>();
        foreach (var vec in Positions) vectorJSON.Add(JsonUtility.ToJson(vec));

        var _data = string.Join("|", vectorJSON);

        //string dataAsJson = JsonUtility.ToJson(_data);
        File.AppendAllText(path, _data + '|');
    }
}