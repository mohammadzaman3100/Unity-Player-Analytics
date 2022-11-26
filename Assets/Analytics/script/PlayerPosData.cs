using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosData : MonoBehaviour
{
    public List<Vector3> Positions = new List<Vector3>();

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Positions.Add(transform.position);
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

            var c = Color.yellow;
            c.a = 0.1f;
            Gizmos.color = c;
            Gizmos.DrawCube(p, Vector3.one);
        }
        
        for (int i = 1; i < Positions.Count; i++)
        {
            Debug.DrawLine(Positions[i - 1], Positions[i]);
        }
    }

    private void OnDestroy()
    {
        // cache the positions to disk
    }
}