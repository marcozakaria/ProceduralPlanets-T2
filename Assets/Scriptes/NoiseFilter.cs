using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter 
{
    NoiseSettings settings;
    Noise noise = new Noise();

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        // float noiseValue = (noise.Evaluate(point * settings.roughness + settings.center) + 1) * 0.5f; // generate value in range of (0,1)
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.center);
            noiseValue += (v + 1) * 0.5f * amplitude; // (v + 1) * 0.5f  = to generate value in range of (0,1)
            frequency *= settings.roughness;
            amplitude *= settings.presistance;
        }
        //Debug.Log(noiseValue * settings.strength);
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
