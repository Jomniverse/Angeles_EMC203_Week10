
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShape : MonoBehaviour
{
    [SerializeField] private Material lineMat;
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private float scale = 1;
    [SerializeField] private Vector3 rotation = Vector3.zero;

    [Range(1, 500)]
    [SerializeField] private float focalLength = 20f;

    private Mesh mesh;
    private readonly List<Vector3> actualPosition = new();

    protected abstract Vector3[] verts { get; }
    protected abstract int[] edges { get; }

    #region Unity Functions

    private void Awake()
    {
        mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = lineMat;
    }

    private void LateUpdate()
    {
        actualPosition.Clear();

        foreach (var point in verts)
        {
            actualPosition.Add(Project(point));
        }

        mesh.vertices = actualPosition.ToArray();

        mesh.SetIndices(edges, MeshTopology.Lines, 0);
        mesh.RecalculateBounds();
    }

    #endregion

    #region Helper

    private Vector2 Project(Vector3 point)
    {
        point *= .5f;

        // Rotate along X axis
        var yRot = point.y * Mathf.Cos(rotation.x * Mathf.Deg2Rad) - point.z * Mathf.Sin(rotation.x * Mathf.Deg2Rad);

        var zRot = point.y * Mathf.Sin(rotation.x * Mathf.Deg2Rad) + point.z * Mathf.Cos(rotation.x * Mathf.Deg2Rad);

        point = new Vector3(point.x, yRot, zRot);

        //rotate via Y axis
        var xRot = point.x * Mathf.Cos(rotation.y * Mathf.Deg2Rad) - point.z * Mathf.Sin(rotation.y * Mathf.Deg2Rad);

        zRot = point.x * Mathf.Sin(rotation.y * Mathf.Deg2Rad) + point.z * Mathf.Cos(rotation.y * Mathf.Deg2Rad);

        // Rotate along Z axis
        xRot = point.x * Mathf.Cos(rotation.z * Mathf.Deg2Rad) - point.y * Mathf.Sin(rotation.z * Mathf.Deg2Rad);

        yRot = point.x * Mathf.Sin(rotation.z * Mathf.Deg2Rad) + point.y * Mathf.Cos(rotation.z * Mathf.Deg2Rad);

        point = new Vector3(xRot, yRot, point.z);

        point = Quaternion.Euler(rotation) * point;

        point = new Vector3(
            point.x + position.x,
            point.y + position.y,
            point.z + position.z
        );

        var p = focalLength / (focalLength + point.z);

        return new Vector2(point.x, point.y) * scale * p;
    }

    #endregion
}
