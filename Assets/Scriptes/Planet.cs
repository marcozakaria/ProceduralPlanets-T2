using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution;
    public bool autoUpdate;

    public ShapeSetings shapeSetings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSttingsFoldOut;
    [HideInInspector]
    public bool colorSttingsFoldOut;

    ShapeGenerator shapeGenerator;

    [SerializeField ,HideInInspector]
    MeshFilter[] meshFilter;
    TerrainFace[] terrainFaces;

    /* function called when script is loaded or value is changed
    private void OnValidate()
    {
        GeneratePlanet();
    }*/

    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSetings);

        if (meshFilter == null || meshFilter.Length == 0)
        {
            meshFilter = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

         // array for direction to assign in localup variable of terrainface
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilter[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard")); // add matrial to the shader
                meshFilter[i] = meshObj.AddComponent<MeshFilter>();
                meshFilter[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(shapeGenerator ,resolution, meshFilter[i].sharedMesh, directions[i]);
        }
    }

    public void GeneratePlanet() 
    {
        Initialize();
        GenerateMesh();
        GenerateColour();
    }

    public void OnShapeSettingsUpdated() // call only when we change shape settings
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }      
    }

    public void OnColourSettingsUpdated() // call only when we change colour settings
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColour();
        }
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    void GenerateColour()
    {
        foreach (MeshFilter m in meshFilter)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.planetColor;
        }
    }

}
