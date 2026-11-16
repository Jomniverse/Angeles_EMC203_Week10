
using UnityEngine;

public class CubeMesh : BaseShape
{
    #region Static Variables

    protected override Vector3[] verts { get; } =
    {
        new(-1, -1, -1), new(1, -1, -1),
        new(1, 1, -1), new(-1, 1, -1),
        new(-1, -1, 1), new(1, -1, 1), 
        new(1, 1, 1), new(-1, 1, 1) 
    };

    protected override int[] edges { get; } =
    {
        0, 1, 1, 2, 2, 3, 3, 0, 
        4, 5, 5, 6, 6, 7, 7, 4,
        0, 4, 1, 5, 2, 6, 3, 7
    }; 

    #endregion
}
