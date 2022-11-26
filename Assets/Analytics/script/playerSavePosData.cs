using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class playerSavePosData : MonoBehaviour
{
    string path;
    private List<Vector3> Positions = new List<Vector3>();

    public void Awake()
    {
        path = Application.dataPath + "/LogPlayerPosData.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "");
        }

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
        List<string> vectorJSON = new List<string>();
        foreach (var vec in Positions)
        {
            vectorJSON.Add(JsonUtility.ToJson(vec));
        }

        string _data = String.Join("|", vectorJSON);

        //string dataAsJson = JsonUtility.ToJson(_data);
        File.AppendAllText(path, _data + '|');
    }
}