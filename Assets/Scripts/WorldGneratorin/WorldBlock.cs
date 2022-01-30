using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class WorldBlock : MonoBehaviour
{
    public Vector3Int BlockOrigin;
    public Vector3Int Size;

    // Open spaces and neighbors
    public Vector3Int[] Openings;
    public Vector3Int[] Directions { get; private set; }
    public WorldBlock[] Neighbors { get; private set; }

    public Transform EnemySpawnPoint;
    public Transform[] PickupSpawnPoints;

    public bool IsEndWall = false;

    private void Awake()
    {
        CalcualteDirections();
    }

    private void CalcualteDirections()
    {
        Directions = new Vector3Int[Openings.Length];
        Neighbors = new WorldBlock[Openings.Length];
        var sv = 0.5f * Vector3.one;
        for (int i = 0; i < Openings.Length; i++)
        {
            Vector3Int op = Openings[i];
            var mid = op + sv;
            if (mid.x < 0)  {
                Directions[i] = new Vector3Int(-1, 0, 0);
            }
            else if (mid.x > Size.x)  {
                Directions[i] = new Vector3Int(1, 0, 0);
            }
            if (mid.y < 0)  {
                Directions[i] = new Vector3Int(0, -1, 0);
            }
            else if (mid.y > Size.y)  {
                Directions[i] = new Vector3Int(0, 1, 0);          
            }
            if (mid.z < 0) {
                Directions[i] = new Vector3Int(0, 0, -1);
            }
            else if (mid.z > Size.z)          {
                Directions[i] = new Vector3Int(0, 0, 1);
            }
        }
    }

    public void SetOrigin(Vector3Int origin)
    {
        BlockOrigin = origin;
        transform.position = WorldGenerator.BlockSize * new Vector3(origin.x, origin.y, origin.z);
    }

    public void Rotate(Quaternion rot)
    {
        transform.rotation *= rot;
        var newSize = rot * Size;
        Size = new Vector3Int(Mathf.RoundToInt(newSize.x), Mathf.RoundToInt(newSize.y), Mathf.RoundToInt(newSize.z));
        for (int i = 0; i < Openings.Length; ++i)
        {
            var no = rot * Openings[i];
            Openings[i] = new Vector3Int(Mathf.RoundToInt(no.x), Mathf.RoundToInt(no.y), Mathf.RoundToInt(no.z));
            var nd = rot * Directions[i];
            Directions[i] = new Vector3Int(Mathf.RoundToInt(nd.x), Mathf.RoundToInt(nd.y), Mathf.RoundToInt(nd.z));
        }
        
        // Get all child objects and do mysterious offset
        if (Size.x < 0)
        {
            foreach (Transform child in transform)
            {
                child.position += Vector3.right*WorldGenerator.BlockSize;
            }
        }
        if (Size.y < 0)
        {
            foreach (Transform child in transform)
            {
                child.position += Vector3.up * WorldGenerator.BlockSize;
            }
        }
        if (Size.z < 0)
        {
            foreach (Transform child in transform)
            {
                child.position += Vector3.forward * WorldGenerator.BlockSize;
            }
        }
    }

    public List<Vector3Int> GetOccupiedSpaces()
    {
        if (IsEndWall)
        {
            return new List<Vector3Int>();
        }
        List<Vector3Int> spaces = new List<Vector3Int>();
        for (int x = Mathf.Min(Size.x+1, 0); x < Mathf.Max(Size.x, 1); ++x)
        {
            for (int y = Mathf.Min(Size.y+1, 0); y < Mathf.Max(Size.y, 1); ++y)
            {
                for (int z = Mathf.Min(Size.z+1, 0); z < Mathf.Max(Size.z, 1); ++z)
                {
                    spaces.Add(BlockOrigin + new Vector3Int(x, y, z));
                }
            }
        }
        return spaces;
    }

    
    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        if (UnityEditor.Selection.activeObject != gameObject)
        {
            return;
        }
        var o = transform.position;
        var s = WorldGenerator.BlockSize;
        foreach (var op in Openings)
        {
            Vector3[] corners = new Vector3[]
            {
                new Vector3(op.x,   op.y,   op.z  ),
                new Vector3(op.x+1, op.y,   op.z  ),
                new Vector3(op.x,   op.y+1, op.z  ),
                new Vector3(op.x,   op.y,   op.z+1),
                new Vector3(op.x+1, op.y+1, op.z  ),
                new Vector3(op.x  , op.y+1, op.z+1),
                new Vector3(op.x+1, op.y  , op.z+1),
                new Vector3(op.x+1, op.y+1, op.z+1),
            };
            //var toDraw = corners.Where(a => a.x >= Mathf.Min(Size.x, 0) && a.x <= Mathf.Max(Size.x, 0) &&
            //                                a.y >= Mathf.Min(Size.y, 0) && a.y <= Mathf.Max(Size.y, 0) &&
            //                                a.z >= Mathf.Min(Size.z, 0) && a.z <= Mathf.Max(Size.z, 0)  ).ToArray();
            for (int i = 0; i < corners.Count(); ++i)
            {
                for (int j = i+1; j < corners.Count(); ++j)
                {
                    Gizmos.DrawLine(o + corners[i]*s, o + corners[j]*s);
                }
            }
        }

        Gizmos.color = Color.red;
        foreach (var space in GetOccupiedSpaces())
        {
            var sf = new Vector3(space.x, space.y, space.z) * WorldGenerator.BlockSize;
            Gizmos.DrawLine(sf,
                            sf + Vector3.one * WorldGenerator.BlockSize);
            Gizmos.DrawLine(sf + new Vector3(1, 0, 0) * WorldGenerator.BlockSize,
                            sf + (Vector3.one - new Vector3(1, 0, 0)) * WorldGenerator.BlockSize);
            Gizmos.DrawLine(sf + new Vector3(0, 1, 0) * WorldGenerator.BlockSize,
                            sf + (Vector3.one - new Vector3(0, 1, 0)) * WorldGenerator.BlockSize);
            Gizmos.DrawLine(sf + new Vector3(0, 0, 1) * WorldGenerator.BlockSize,
                            sf + (Vector3.one - new Vector3(0, 0, 1)) * WorldGenerator.BlockSize);
        }
        #endif
    }
}
