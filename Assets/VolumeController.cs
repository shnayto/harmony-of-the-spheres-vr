using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class VolumeController : MonoBehaviour
{
    private StudioEventEmitter eventEmitter;
    private float initialVolume;

    private void Start()
    {
        // Get the StudioEventEmitter component attached to this GameObject
        eventEmitter = GetComponent<StudioEventEmitter>();
        // Get the initial volume from the event instance using the parameter name
        eventEmitter.EventInstance.getParameterByName("Volume", out initialVolume);
    }

    public void IncreaseVolume()
    {
        StartCoroutine(SmoothlyChangeVolume(0.5f, 1.0f));
    }

    public void DecreaseVolume()
    {
        StartCoroutine(SmoothlyChangeVolume(1.0f, 0.5f));
    }

    private System.Collections.IEnumerator SmoothlyChangeVolume(float targetVolume, float duration = 0.3f)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float currentVolume = Mathf.Lerp(initialVolume, targetVolume, t);

            // Set the new volume using the parameter name
            eventEmitter.EventInstance.setParameterByName("Volume", currentVolume);

            yield return null;
        }
    }
}
