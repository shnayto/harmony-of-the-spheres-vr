using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleScriptAB : MonoBehaviour
{
    public InputActionReference thumbstickAction = null;
    private Transform objectTransform = null;
    private bool isScalingEnabled = false;
    public float scaleStep = 10f; // Amount to increase/decrease the scale
    public float minScale = 100f; // Minimum scale value
    public float maxScale = 500f; // Maximum scale value

    private void Awake()
    {
        objectTransform = transform;
    }

    private void Update()
    {
        if (!isScalingEnabled)
            return;

        Vector2 thumbstickValue = thumbstickAction.action.ReadValue<Vector2>();
        float value = thumbstickValue.y;

        if (value > 0f)
            IncreaseScale(Mathf.Abs(value));
        else if (value < 0f)
            DecreaseScale(Mathf.Abs(value));
    }

    private void IncreaseScale(float value)
{
    Vector3 currentScale = objectTransform.localScale;
    float newScale = Mathf.Clamp(currentScale.x + value * scaleStep, minScale, maxScale);
    UpdateScale(newScale);
}

private void DecreaseScale(float value)
{
    Vector3 currentScale = objectTransform.localScale;
    float newScale = Mathf.Clamp(currentScale.x - value * scaleStep, minScale, maxScale);
    UpdateScale(newScale);
}


    private void UpdateScale(float value)
    {
        Vector3 newScale = Vector3.one * value;
        objectTransform.localScale = newScale;
    }

    public void EnableScaling()
    {
        isScalingEnabled = true;
    }

    public void DisableScaling()
    {
        isScalingEnabled = false;
    }

    public bool IsScalingEnabled()
    {
        return isScalingEnabled;
    }
}

