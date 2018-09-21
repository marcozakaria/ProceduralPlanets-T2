using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter 
{
    Noise noise = new Noise();

    public float Evaluate(Vector3 point)
    {
        float noiseValue = (noise.Evaluate(point) + 1) * 0.5f; // generate value in range of (0,1)
        return noiseValue;
    }
}
