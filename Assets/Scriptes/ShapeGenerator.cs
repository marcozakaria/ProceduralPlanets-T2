using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSetings shapeSetings;
    NoiseFilter[] noiseFilters;

    public ShapeGenerator(ShapeSetings shapeSetings)
    {
        this.shapeSetings = shapeSetings;
        noiseFilters = new NoiseFilter[shapeSetings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(shapeSetings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 PointOnUnitPlanet(Vector3 pointOnUnitSphere)
    {
        //float elevation = noiseFilter.Evaluate(pointOnUnitSphere);
        float elevation = 0;
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            if (shapeSetings.noiseLayers[i].enabled)  // to have ability to enable and disable the layers
            {
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere); // to add noise layers to gether  
            }
        }
        return pointOnUnitSphere * shapeSetings.planetRadius *(1+elevation); // (elevation +1) bec, planet point must be not = to zero
    }
}
