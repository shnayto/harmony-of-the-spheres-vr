using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform solarSystemViewPosition;
    public float planetViewYThreshold = 100f;
    private bool isInPlanetView = false;

    private void Update()
    {
        if (!isInPlanetView && transform.position.y < planetViewYThreshold)
        {
            // Switch to PlanetView
            EnterPlanetView();
        }
        else if (isInPlanetView && transform.position.y >= planetViewYThreshold)
        {
            // Switch to SolarSystemView
            ExitPlanetView();
        }
    }

    private void EnterPlanetView()
    {
        isInPlanetView = true;
        PauseMotionOfPlanets();

        // Additional actions for entering PlanetView, such as adjusting camera position
    }

    private void ExitPlanetView()
    {
        isInPlanetView = false;
        ResumeMotionOfPlanets();

        // Additional actions for exiting PlanetView, such as resetting camera position
    }

    private void PauseMotionOfPlanets()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        foreach (GameObject planet in planets)
        {
            PlanetController planetController = planet.GetComponent<PlanetController>();
            if (planetController != null)
            {
                planetController.PauseMotion();
            }
        }
    }

    private void ResumeMotionOfPlanets()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        foreach (GameObject planet in planets)
        {
            PlanetController planetController = planet.GetComponent<PlanetController>();
            if (planetController != null)
            {
                planetController.ResumeMotion();
            }
        }
    }
}

