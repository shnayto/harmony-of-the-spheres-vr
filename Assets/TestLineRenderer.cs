using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3 startPoint;
    public Vector3 endPoint;

    private void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
