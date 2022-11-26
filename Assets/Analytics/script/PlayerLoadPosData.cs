using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerLoadPosData : MonoBehaviour
{
    string content;
    string path;
    private List<Vector3> Positions = new List<Vector3>();

    public void Awake()
    {
         path = Application.dataPath + "/LogPlayerPosData.txt";
        // if (!File.Exists(path))
        // {
        //     File.WriteAllText(path, "");
        // }

        LoadPos();
    }

    void LoadPos()
    {
        string in_data = File.ReadAllText(path);
        Debug.Log($"in_data {in_data}");
        string[] jsonString = in_data.Split('|');
        foreach (var json in jsonString)
        {
            if (json.Length > 0)
            {
                Vector3 p = JsonUtility.FromJson<Vector3>(json);
                Positions.Add(p);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 prev = Vector3.zero;
        var c = Color.yellow;
        foreach (var position in Positions)
        {
            //this creates a box and line of the player's pos
            var p = position;
            p.x = (int) p.x;
            p.y = (int) p.y;
            p.z = (int) p.z;


            c.a = 0.1f;
            Gizmos.color = c;
            Gizmos.DrawCube(p, Vector3.one);


            // testing saving
            myClass myClass01 = new myClass();
            myClass01.Positions2 = Positions;

            //allpositions = new Vector3(p.x, p.y, p.z);

            //string dataAsJson = JsonUtility.ToJson(myClass01.Positions2);
            content = "Vector3" + position + "\n";
            Debug.Log(position);
        }

        for (int i = 1; i < Positions.Count; i++)
        {
            if (Vector3.Distance(Positions[i - 1], Positions[i]) > 2)
            {
                c = Color.black;
            }
            else
            {
                c = Color.white;
            }

            Debug.DrawLine(Positions[i - 1], Positions[i]);
            Gizmos.DrawLine(Positions[i - 1], Positions[i]);
        }
    }

    [Serializable]
    public class myClass
    {
        public List<Vector3> Positions2 = new List<Vector3>();
    }
}