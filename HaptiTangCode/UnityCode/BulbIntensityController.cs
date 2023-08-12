using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulbIntensityController : MonoBehaviour
{
    public Transform sliderTransform; // Reference to the slider's Transform
    public float minX; // Minimum X position of the slider
    public float maxX; // Maximum X position of the slider

    public float minLightIntensity; // Minimum intensity of the Point light
    public float maxLightIntensity; // Maximum intensity of the Point light

    public float minEmissionIntensity; // Minimum emission intensity of the material
    public float maxEmissionIntensity; // Maximum emission intensity of the material

    // Reference to the Point light component and the Material of the GameObject
    public Light pointLight;
    public Material material;

    void Update()
    {
        // Get the X position of the slider
        float slider = sliderTransform.position.z;

        // Map the slider position (minX to maxX) to the intensity values for the Point light and Material emission
        float mappedIntensityL = Mathf.Lerp(minLightIntensity, maxLightIntensity, Mathf.InverseLerp(minX, maxX, slider));
        float mappedIntensityE = Mathf.Lerp(minEmissionIntensity, maxEmissionIntensity, Mathf.InverseLerp(minX, maxX, slider));

        // Set the intensity of the Point light and the emission intensity of the Material
        pointLight.intensity = mappedIntensityL;
        material.SetColor("_EmissionColor", new Color(mappedIntensityE, mappedIntensityE, mappedIntensityE));
    }
}