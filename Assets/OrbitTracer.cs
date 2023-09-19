using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class OrbitTracer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int positionsCount = 100;
    public float trailTime = 5f;
    public Transform sunTransform;
    public Transform planetTransform;

    private List<Vector3> positions = new List<Vector3>();

    private void Start()
    {
        lineRenderer.positionCount = positionsCount;
    }

    private void Update()
    {
        // Add the current position to the list of positions
        positions.Add(planetTransform.position);

        // Remove older positions if the list gets too long
        if (positions.Count > positionsCount)
        {
            positions.RemoveAt(0);
        }

        // Update the line renderer with the positions
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

        // Clear the positions that are older than trailTime
        for (int i = positions.Count - 1; i >= 0; i--)
        {
            if (Time.time - trailTime > i * Time.deltaTime)
            {
                positions.RemoveAt(i);
            }
        }
    }
}

