using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbitLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform sunTransform;
    public Transform planetTransform;
    public int sortingOrder = 0;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = sunTransform.position;
        positions[1] = planetTransform.position;
        lineRenderer.SetPositions(positions);
    }
}

