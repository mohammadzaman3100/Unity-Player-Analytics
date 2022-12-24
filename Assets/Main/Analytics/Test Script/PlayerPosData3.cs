using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerPosData3 : MonoBehaviour
{
    public List<Vector3> savePositions = new List<Vector3>();
    private readonly List<Vector3> Positions = new List<Vector3>();
    private Vector3 _currentPos = Vector3.zero;
    private string content;
    private string path;

    public void Awake()
    {
        path = Application.dataPath + "/LogPlayerPosData.txt";
        if (!File.Exists(path)) File.WriteAllText(path, "");

        LoadPos();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Positions.Add(transform.position);
        }
    }

    public void Update()
    {
        savePositions = Positions;
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

    private void OnDestroy()
    {
        // cache the positions to disk
    }

    private void OnDrawGizmos()
    {
        var prev = Vector3.zero;
        var c = Color.yellow;
        foreach (var position in Positions)
        {
            //this creates a box and line of the player's pos
            var p = position;
            p.x = (int)p.x;
            p.y = (int)p.y;
            p.z = (int)p.z;


            c.a = 0.1f;
            Gizmos.color = c;
            Gizmos.DrawCube(p, Vector3.one);


            // testing saving
            var myClass01 = new myClass();
            myClass01.Positions2 = Positions;

            //allpositions = new Vector3(p.x, p.y, p.z);

            //string dataAsJson = JsonUtility.ToJson(myClass01.Positions2);
            content = "Vector3" + position + "\n";
            Debug.Log(position);
        }

        for (var i = 1; i < Positions.Count; i++)
        {
            if (Vector3.Distance(Positions[i - 1], Positions[i]) > 2)
                c = Color.black;
            else
                c = Color.white;

            Debug.DrawLine(Positions[i - 1], Positions[i]);
        }
    }

    private void LoadPos()
    {
        var in_data = File.ReadAllText(path);
        Debug.Log($"in_data {in_data}");
        var jsonString = in_data.Split('|');
        foreach (var json in jsonString)
            if (json.Length > 0)
            {
                var p = JsonUtility.FromJson<Vector3>(json);
                Positions.Add(p);
            }
    }

    [Serializable]
    public class myClass
    {
        public List<Vector3> Positions2 = new List<Vector3>();
    }
}