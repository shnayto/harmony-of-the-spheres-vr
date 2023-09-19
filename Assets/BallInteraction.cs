using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallInteraction : MonoBehaviour
{
    public string frequencyChannel = "freq";
    public float maxHeight = 0.5f;
    public float minHeight = 0f;
    public float maxFrequency = 4000f;
    public float minFrequency = 0f;
    CsoundUnity csoundUnity;
    private Rigidbody rb;

    private void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float heightRatio = Mathf.InverseLerp(minHeight, maxHeight, transform.position.y);
        float frequency = Mathf.Lerp(minFrequency, maxFrequency, heightRatio);
        csoundUnity.SetChannel(frequencyChannel, frequency);
    }
}


