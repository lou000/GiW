using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFace
{
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public MeshFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        this.axisB = Vector3.Cross(localUp, axisA);
    }


    public void createMesh()
    {
        Vector3[] vertices = new Vector3[resolution*resolution];
        int[] triangles = new int[(resolution-1)*(resolution-1)*6];
        int count = 0;
        int iIndex = 0;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector2 progress = new Vector2(x, y) / (resolution-1);
                Vector3 point = localUp + axisA*2*(progress.x-0.5f) + axisB*2*(progress.y-0.5f);
                vertices[count] = point.normalized;
                if(x!=resolution-1 && y!=resolution-1)
                {
                    triangles[iIndex] = count;
                    triangles[iIndex+1] = count+resolution+1;
                    triangles[iIndex+2] = count+resolution;

                    triangles[iIndex+3] = count;
                    triangles[iIndex+4] = count+1;
                    triangles[iIndex+5] = count+resolution+1;
                    iIndex+=6;
                }
                count++;
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
