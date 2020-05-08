using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix3d
{
    private float[] elements;
    public Matrix3d()
    {
        elements = new float[]{1,0,0,0,
                               0,1,0,0,
                               0,0,1,0,
                               0,0,0,1};
        
    }
    public Matrix3d(float[] arr)
    {
        elements = arr;
    }

    public ref float[] GetElementsRef()
    {
        return ref elements;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(elements[3], elements[7], elements[11]);
    }

    public static Matrix3d Scale(Vector3 scale)
    {
        Matrix3d matrix = new Matrix3d();
        matrix.elements[0] = scale.x;
        matrix.elements[5] = scale.y;
        matrix.elements[10] = scale.z;
        return matrix;
    }

    public static Matrix3d Translation(Vector3 translation)
    {
        Matrix3d matrix = new Matrix3d();
        matrix.elements[3] = translation.x;
        matrix.elements[7] = translation.y;
        matrix.elements[11] = translation.z;
        return matrix;
    }

    public static Matrix3d RotationX(float angle)
    {
        Matrix3d matrix = new Matrix3d();
        matrix.elements[5] = Mathf.Cos(-angle);
        matrix.elements[6] = -Mathf.Sin(-angle);
        matrix.elements[9] = Mathf.Sin(-angle);
        matrix.elements[10] = Mathf.Cos(-angle);
        return matrix;
    }

    public static Matrix3d RotationY(float angle)
    {
        Matrix3d matrix = new Matrix3d();
        matrix.elements[0] = Mathf.Cos(-angle);
        matrix.elements[2] = Mathf.Sin(-angle);
        matrix.elements[8] = -Mathf.Sin(-angle);
        matrix.elements[10] = Mathf.Cos(-angle);
        return matrix;
    }

    public static Matrix3d RotationZ(float angle)
    {
        Matrix3d matrix = new Matrix3d();
        matrix.elements[0] = Mathf.Cos(-angle);
        matrix.elements[1] = -Mathf.Sin(-angle);
        matrix.elements[4] = Mathf.Sin(-angle);
        matrix.elements[5] = Mathf.Cos(-angle);
        return matrix;
    }

    public static Matrix3d operator *(Matrix3d a, Matrix3d b)
    {
        Matrix3d matrix = new Matrix3d();
        matrix.elements[0] = a.elements[0] * b.elements[0] + a.elements[1] * b.elements[4] + a.elements[2] * b.elements[8] + a.elements[3] * b.elements[12];
        matrix.elements[1] = a.elements[0] * b.elements[1] + a.elements[1] * b.elements[5] + a.elements[2] * b.elements[9] + a.elements[3] * b.elements[13];
        matrix.elements[2] = a.elements[0] * b.elements[2] + a.elements[1] * b.elements[6] + a.elements[2] * b.elements[10] + a.elements[3] * b.elements[14];
        matrix.elements[3] = a.elements[0] * b.elements[3] + a.elements[1] * b.elements[7] + a.elements[2] * b.elements[11] + a.elements[3] * b.elements[15];

        matrix.elements[4] = a.elements[4] * b.elements[0] + a.elements[5] * b.elements[4] + a.elements[6] * b.elements[8] + a.elements[7] * b.elements[12];
        matrix.elements[5] = a.elements[4] * b.elements[1] + a.elements[5] * b.elements[5] + a.elements[6] * b.elements[9] + a.elements[7] * b.elements[13];
        matrix.elements[6] = a.elements[4] * b.elements[2] + a.elements[5] * b.elements[6] + a.elements[6] * b.elements[10] + a.elements[7] * b.elements[14];
        matrix.elements[7] = a.elements[4] * b.elements[3] + a.elements[5] * b.elements[7] + a.elements[6] * b.elements[11] + a.elements[7] * b.elements[15];

        matrix.elements[8] = a.elements[8] * b.elements[0] + a.elements[9] * b.elements[4] + a.elements[10] * b.elements[8] + a.elements[11] * b.elements[12];
        matrix.elements[9] = a.elements[8] * b.elements[1] + a.elements[9] * b.elements[5] + a.elements[10] * b.elements[9] + a.elements[11] * b.elements[13];
        matrix.elements[10] = a.elements[8] * b.elements[2] + a.elements[9] * b.elements[6] + a.elements[10] * b.elements[10] + a.elements[11] * b.elements[14];
        matrix.elements[11] = a.elements[8] * b.elements[3] + a.elements[9] * b.elements[7] + a.elements[10] * b.elements[11] + a.elements[11] * b.elements[15];

        matrix.elements[12] = a.elements[12] * b.elements[0] + a.elements[13] * b.elements[4] + a.elements[14] * b.elements[8] + a.elements[15] * b.elements[12];
        matrix.elements[13] = a.elements[12] * b.elements[1] + a.elements[13] * b.elements[5] + a.elements[14] * b.elements[9] + a.elements[15] * b.elements[13];
        matrix.elements[14] = a.elements[12] * b.elements[2] + a.elements[13] * b.elements[6] + a.elements[14] * b.elements[10] + a.elements[15] * b.elements[14];
        matrix.elements[15] = a.elements[12] * b.elements[3] + a.elements[13] * b.elements[7] + a.elements[14] * b.elements[11] + a.elements[15] * b.elements[15];
        return matrix;
    }

    public static Vector3 operator *(Matrix3d a, Vector4 vec)
    {
        float x = a.elements[0] * vec.x + a.elements[1] * vec.y + a.elements[2]*vec.z + a.elements[3]*vec.w;
        float y = a.elements[4] * vec.x + a.elements[5] * vec.y + a.elements[6]*vec.z + a.elements[7]*vec.w;
        float z = a.elements[8] * vec.x + a.elements[9] * vec.y + a.elements[10]*vec.z + a.elements[11]*vec.w;
        return new Vector3(x, y, z);
    }


}
