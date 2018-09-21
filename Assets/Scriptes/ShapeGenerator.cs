using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSetings shapeSetings;
    NoiseFilter noiseFilter;

    public ShapeGenerator(ShapeSetings shapeSetings)
    {
        this.shapeSetings = shapeSetings;
        noiseFilter = new NoiseFilter();
    }

    public Vector3 PointOnUnitPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = noiseFilter.Evaluate(pointOnUnitSphere);
        return pointOnUnitSphere * shapeSetings.planetRadius *(1+elevation);
    }
}
