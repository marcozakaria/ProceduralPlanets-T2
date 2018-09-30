using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgedNoiseFilter
{
    NoiseSettings settings;
    Noise noise = new Noise();

    public RidgedNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        // float noiseValue = (noise.Evaluate(point * settings.roughness + settings.center) + 1) * 0.5f; // generate value in range of (0,1)
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;  // we want noise at the ridges to be more detailed more than values below

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.center));  // 1-mathf.abs to make the value of sin wave is peak to get relistic details
            v *= v;  // square to make edges charper
            v *= weight;
            weight = v;

            noiseValue += v * amplitude; // 1-mathf.abs() wil aleardy give us a value from (0 to 1)
            frequency *= settings.roughness;  // when roughness is value grater than 1 frequency will increase with each layer
            amplitude *= settings.presistance; // when presistance is less than one amplitude  will decrease by each layer
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
