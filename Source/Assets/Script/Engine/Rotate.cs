using UnityEngine;

public class Rotate
{
    private static double[,] rotation;
    public static void RotateX()
    {
        throw new System.NotImplementedException();
    }

    public static Mesh RotateY(Mesh mesh, double delta)
    {
        mesh.vertices = RotateY(mesh.vertices, delta);
        return mesh;
    }

    public static Vector3[] RotateY(Vector3[] vectors, double delta)
    {
        return VectorXYZ.ToVector3(RotateY(VectorXYZ.FromVector3(vectors), delta));
    }

    public static VectorXYZ[] RotateY(VectorXYZ[] vectors, double delta)
    {
        rotation = new double[3, 3] { {Mathf.Cos((float)delta), 0, Mathf.Sin((float)delta)}
                                    , {0, 1, 0}
                                    , {-Mathf.Sin((float)delta), 0, Mathf.Cos((float)delta)}
                                    };

        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i] = rotation * vectors[i];
        }
        return vectors;
    }

    public static void RotateZ()
    {
        throw new System.NotImplementedException();
    }
}
