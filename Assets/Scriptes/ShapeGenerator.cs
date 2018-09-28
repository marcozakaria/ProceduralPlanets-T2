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
        float firstLayerValue=0;
        float elevation = 0;

        if (noiseFilters.Length >0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (shapeSetings.noiseLayers[0].enabled)  // to have ability to enable and disable the layers
            {
                elevation += firstLayerValue; // to add first layer to be used as a mask for next layers 
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (shapeSetings.noiseLayers[i].enabled)  // to have ability to enable and disable the layers
            {
                float mask = (shapeSetings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;  // if we checked to use first layer as a mask
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) *mask; // to add noise layers to gether  
            }
        }
        return pointOnUnitSphere * shapeSetings.planetRadius *(1+elevation); // (elevation +1) bec, planet point must be not = to zero
    }
}
