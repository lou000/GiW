using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMesh : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;

    [SerializeField, HideInInspector]
    MeshFilter[] filters;
    MeshFace[] faces;

    private void OnValidate() 
    {
        initialize();
        generateMesh();    
    }

    void initialize()
    {
        if(filters == null || filters.Length == 0)
            filters = new MeshFilter[6];
        faces = new MeshFace[6];
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.forward,
                                Vector3.back, Vector3.left, Vector3.right};
        for (int i = 0; i < 6; i++)
        {
            if(filters[i]==null)
            {
                GameObject obj = new GameObject("mesh");
                obj.transform.parent = transform;
                obj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                filters[i] = obj.AddComponent<MeshFilter>();
                filters[i].sharedMesh = new Mesh();
            }
            faces[i] = new MeshFace(filters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void generateMesh()
    {
        foreach(MeshFace face in faces)
        {
            face.createMesh();
        }
    }


}
