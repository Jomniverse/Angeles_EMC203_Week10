
using System.Collections.Generic;
using UnityEngine;

public class SphereMesh : BaseShape
{
    [SerializeField] private int segments = 8;

    private Vector3[] sphereVerts;
    private int[] sphereEdges;

    protected override Vector3[] verts
    {
        get { return sphereVerts; }
    }

    protected override int[] edges
    {
        get { return sphereEdges; }
    }

    private void OnEnable()
    {
        List<Vector3> points = new();
        List<int> lines = new();

        for (int i = 0; i <= segments; i++)
        {
            float a = Mathf.PI * i / segments;

            for (int j = 0; j < segments; j++)
            {
                float b = 2 * Mathf.PI * j / segments;

                float x = Mathf.Sin(a) * Mathf.Cos(b);
                float y = Mathf.Cos(a);
                float z = Mathf.Sin(a) * Mathf.Sin(b);

                points.Add(new Vector3(x, y, z));

                int current = i * segments + j;
                lines.Add(current);
                lines.Add(i * segments + (j + 1) % segments);

                if (i < segments)
                {
                    lines.Add(current);
                    lines.Add(current + segments);
                }
            }
        }

        sphereVerts = points.ToArray();
        sphereEdges = lines.ToArray();
    }
}
