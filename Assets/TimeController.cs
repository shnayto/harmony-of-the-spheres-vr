using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private bool isTimeFrozen = false;
    private float transitionDuration = 1f;

    public void FreezeTime()
    {
        StartCoroutine(TransitionTimeScale(0f));
        isTimeFrozen = true;
    }

    public void ResumeTime()
    {
        StartCoroutine(TransitionTimeScale(1f));
        isTimeFrozen = false;
    }

    public bool IsTimeFrozen()
    {
        return isTimeFrozen;
    }

    private IEnumerator TransitionTimeScale(float targetTimeScale)
    {
        float startTimeScale = Time.timeScale;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float normalizedTime = elapsedTime / transitionDuration;
            Time.timeScale = Mathf.Lerp(startTimeScale, targetTimeScale, normalizedTime);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = targetTimeScale;
    }
}
