using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LineTrail : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int positionsCount = 100;
    public Transform sunTransform;

    private List<Vector3> positions = new List<Vector3>();

    private void Start()
    {
        lineRenderer.positionCount = positionsCount;
    }

    private void Update()
    {
        // Add the current position to the list of positions
        positions.Add(transform.position);

        // Remove older positions if the list gets too long
        if (positions.Count > positionsCount)
        {
            positions.RemoveAt(0);
        }

        // Update the line renderer with the positions
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

    }
} 