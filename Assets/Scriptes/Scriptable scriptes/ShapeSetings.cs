using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSetings : ScriptableObject
{
    public float planetRadius = 1f;
    public NoiseLayer[] noiseLayers;

    [System.Serializable] // to show up in the inspector
    public class NoiseLayer  // to have multiple noise settings and layer them on top of each other( to make shape o contents and mountains)
    {
        public bool enabled = true; // to toogle visability os single noise  layer
        public NoiseSettings noiseSettings;
    }
}
