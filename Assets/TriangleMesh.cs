using System.Collections.Generic;
using UnityEngine;

public class TriangleMesh : BaseShape
{
    #region Static Variables

    protected override Vector3[] verts { get; } =
    {
        new(-1, -1, -1), new(1, -1, -1), // 0, 1
        new(-1, -1, 1), new(1, -1, 1), // 4, 5
        new(0, 1, 0) // 4 
    };

    protected override int[] edges { get; } =
    {
       0, 1, 0, 2, 1, 3, 2, 3,
       0, 4, 1, 4, 2, 4, 3, 4

    }; 


    #endregion

} 