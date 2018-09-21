using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSetings shapeSetings;

    public ShapeGenerator(ShapeSetings shapeSetings)
    {
        this.shapeSetings = shapeSetings;
    }

    public Vector3 PointOnUnitPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * shapeSetings.planetRadius;
    }
}
