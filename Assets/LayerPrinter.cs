using UnityEngine;

public class LayerPrinter : MonoBehaviour
{
    private void Start()
    {
        // Print the layer of this GameObject
        Debug.Log("Layer of " + gameObject.name + ": " + gameObject.layer);
    }
}

