using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2 : MonoBehaviour
{
    [SerializeField, HideInInspector]
    public Matrix4x4 transformMatrix = Matrix4x4.identity;
    [SerializeField]
    public Rotation2 center = null;
    [Range(0, 100)]
    public float radius = 20;
    [Range(0, 100)]
    public float speed = 20;
    [Range(0, 100)]
    public float scale = 1;
    [Range(0, 90)]
    public float angle = 20;
    [SerializeField]
    Vector4 startPos = new Vector4(0,0,0,1);
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        time+=Time.deltaTime;
        
    }

    Matrix4x4 GetMatrix()
    {
        if(center==null)
            return transformMatrix;
        else
            return center.transformMatrix * transformMatrix;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Euler(angle, 0, 0) * Quaternion.Euler(1, speed * time*10, 1);
        transformMatrix = Matrix4x4.Scale(new Vector3(scale, 1, 1)) * Matrix4x4.Rotate(rotation) * Matrix4x4.Translate(new Vector3(radius, 0, 0));;
        transform.position = GetMatrix()*startPos;
        time+=Time.deltaTime;
    }
}
