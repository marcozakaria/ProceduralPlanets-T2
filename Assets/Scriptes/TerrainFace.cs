using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    int resolution;
    Mesh mesh;
    Vector3 localUP;
    ShapeGenerator shapeGenerator;

    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator,int resolution, Mesh mesh, Vector3 localUP)
    {
        this.shapeGenerator = shapeGenerator;
        this.resolution = resolution;
        this.mesh = mesh;
        this.localUP = localUP;

        axisA = new Vector3(localUP.y, localUP.z, localUP.x);
        axisB = Vector3.Cross(localUP, axisA); // cross return veector perpendicular to them
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[((resolution - 1) * (resolution - 1)) * 6];
        int triIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + (y*resolution);  // to get vertices index
                Vector2 percent = new Vector2(x, y) / (resolution - 1); // tell us how near we are to complete the loop ,to define were the vertics should be along the face
                Vector3 pointOnUnitCube = localUP + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB; // (percent.x - 0.5f) * 2 = will give us a value between (-1,1)
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;  // will change from cube to sphere to make vertices from the same distance from the center
                vertices[i] = shapeGenerator.PointOnUnitPlanet(pointOnUnitSphere);  // when we change planet radius

                if (x!=resolution-1 && y!= resolution-1)  // || not percent equal to one
                {
                    triangles[triIndex] = i;            // for first triangle in quad
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;        // secont triangele in quad
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution +1;
                    triIndex += 6;                      
                }               
            }
        }
        mesh.Clear(); // clear all vertices and triangles to ovoid any problems when we change the resolution
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // recalculate normals for new triangles

    }
}
