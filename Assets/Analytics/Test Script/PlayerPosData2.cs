using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerPosData2 : MonoBehaviour
{
    string content;
    string path;
    public List<Vector3> savePositions = new List<Vector3>();
    public List<Vector3> Positions = new List<Vector3>();
    
    [Serializable]
    public class myClass
    {
        public List<Vector3> Positions2 = new List<Vector3>();
    }

    public void Awake()
    {
        path = Application.dataPath + "/LogPlayerPosData.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Login log \n\n");
        }
    }
    
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Positions.Add(transform.position);


            File.AppendAllText(path, content);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var position in Positions)
        {
            var p = position;
            p.x = (int)p.x;
            p.y = (int)p.y;
            p.z = (int)p.z;
            
            //
            myClass myClass01 = new myClass();
            myClass01.Positions2 = Positions;


            var c = Color.yellow;
            c.a = 0.1f;
            Gizmos.color = c;
            Gizmos.DrawCube(p, Vector3.one);
            // allpositions = new Vector3(p.x, p.y, p.z);

            string dataAsJson = JsonUtility.ToJson(myClass01.Positions2);
            content = "Vector3" + position + "\n";
            Debug.Log(position);
        }

        for (int i = 1; i < Positions.Count; i++)
        {
            Debug.DrawLine(Positions[i - 1], Positions[i]);
        }
    }

    public void Update()
    {
        savePositions = Positions;
    }


    private void OnDestroy()
    {
        // cache the positions to disk
    }
}