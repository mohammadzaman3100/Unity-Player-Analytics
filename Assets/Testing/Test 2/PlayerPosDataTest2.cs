using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosDataTest2 : MonoBehaviour
{
    // need to make this more efficient
    
    public List<Vector3> Positions = new List<Vector3>();

    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        // Only add the position if it has changed
        if (previousPosition != transform.position)
        {
            Positions.Add(transform.position);
            previousPosition = transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var position in Positions)
        {
            var c = Color.yellow;
            c.a = 0.1f;
            Gizmos.color = c;
            Gizmos.DrawCube(position, Vector3.one);
        }

        for (var i = 1; i < Positions.Count; i++) Debug.DrawLine(Positions[i - 1], Positions[i]);
    }
}