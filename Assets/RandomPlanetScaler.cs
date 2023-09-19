using UnityEngine;

public class RandomPlanetScaler : MonoBehaviour
{
    public float minScale = 0.05f;
    public float maxScale = 0.2f;

    private void Start()
    {
        // Get a random scale for the planet
        float randomScale = Random.Range(minScale, maxScale);

        // Set the planet's scale uniformly in all three axes
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
}

