using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LineTracer : MonoBehaviour

{
    public LineRenderer lineRenderer;
    public Transform planetTransform;
    public int positionsCount = 100;
    public float semiMajorAxis = 5f;
    public float semiMinorAxis = 3f;
    public float lineWidth = 0.1f;
    public Color lineColor = Color.white;

    private Vector3[] positions;

    private void Start()
    {
        lineRenderer.positionCount = positionsCount;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material.color = lineColor;

        positions = new Vector3[positionsCount];
        UpdatePositions();
    }

    private void Update()
    {
        UpdatePositions();
        lineRenderer.SetPositions(positions);
    }

    private void UpdatePositions()
    {
        float angleStep = 360f / positionsCount;
        for (int i = 0; i < positionsCount; i++)
        {
            float angle = i * angleStep;
            float x = semiMajorAxis * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = semiMinorAxis * Mathf.Sin(Mathf.Deg2Rad * angle);
            Vector3 position = new Vector3(x, 0f, z);
            positions[i] = planetTransform.position + position;
        }
    }
}
