using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField, HideInInspector]
    Matrix3d transformMatrix;
    [SerializeField]
    Rotation center = null;
    [Range(0, 100)]
    public float radius = 20;
    [Range(0, 100)]
    public float speed = 20;
    [Range(0, 100)]
    public float scale = 1;
    [SerializeField]
    Vector4 startPos = new Vector4(0,0,0,1);
    float time = 0;

    public Rotation()
    {
        transformMatrix = new Matrix3d();
    }

    Matrix3d GetMatrix()
    {
        if(center==null)
            return transformMatrix;
        else
            return center.transformMatrix * transformMatrix;
    }

    // Start is called before the first frame update
    void Start()
    {
        time+=Time.deltaTime;
        transformMatrix = Matrix3d.Scale(new Vector3(scale,1,1))*Matrix3d.RotationY(speed*time) * Matrix3d.Translation(new Vector3(radius,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        transformMatrix = Matrix3d.Scale(new Vector3(scale,1,1))*Matrix3d.RotationY(speed*time) * Matrix3d.Translation(new Vector3(radius,0,0));
        transform.position =  GetMatrix() * startPos;
        time+=Time.deltaTime;
    }
}
