using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector3Int Divide(this Vector3Int vec, float divisor)
    {
        Vector3Int result = new Vector3Int((int)(vec.x / divisor), (int)(vec.y / divisor), (int)(vec.z / divisor));
        return result;
    }
    public static Vector3Int Multiply(this Vector3Int vec, float multiplier)
    {
        Vector3Int result = new Vector3Int((int)(vec.x * multiplier), (int)(vec.y * multiplier), (int)(vec.z * multiplier));
        return result;
    }
    public static Vector3Int ToVector3Int(this Vector3 vector)
    {
        return new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);
    }
    public static void GetChunkAndVoxelIndex(Vector3Int index, ChunkSettings chunkSettings, out Vector3Int chunkIndex3D, out Vector3Int voxelIndex3D)
    {
        Vector3Int quotinent = index.Divide(chunkSettings.ChunkSL); // deltaFromStart / chunkWorldLen; // get chunk index
        Vector3Int remainder = index - quotinent * chunkSettings.ChunkSL; // get voxel index
        chunkIndex3D = new Vector3Int((int)quotinent.x, (int)quotinent.y, (int)quotinent.z);
        voxelIndex3D = new Vector3Int((int)remainder.x, (int)remainder.y, (int)remainder.z);
    }


    // direction: XXYYZZ
    // direction: [-1,0,1,2] [-1,0,1,2] [-1,0,1,2]
    // dont ever give 0b11 to field
    public static readonly Vector3Int[] directionVectors =
    {
            new Vector3Int(0, 0, 1),
            new Vector3Int(0, 0, -1),

            new Vector3Int(0, 1,0),
            new Vector3Int(0, -1,0),

            new Vector3Int(1, 0,0),
            new Vector3Int(-1, 0,0),
    };
    public static readonly byte[] directionBytes =
    {
            0b000001,
            0b000010,
            0b000100,
            0b001000,
            0b010000,
            0b100000,
    };

    // its a bitmask

    // 00 00 01 right
    // 00 00 11 right, left
    // 10 00 00 back




    // direction should have only one bit
    public static Vector3Int ToVector3Int(this byte direction)
    {
        for(int i = 0; i < 6; i++)
        {
            if(direction == directionBytes[i])
            {
                return directionVectors[i];
            }
        }
        return Vector3Int.zero; // cant find
    }
    public static byte ToDirection(this Vector3Int vector3Int)
    {
        for (int i = 0; i < 6; i++)
        {
            if(vector3Int == directionVectors[i])
            {
                return directionBytes[i];
            }
        }
        return 7; // cant find
    }

    public static Vector3 GetMousePosInWorldSpace(Plane plane, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        float enter = 0;
        plane.Raycast(ray, out enter);

        Vector3 contactPoint = ray.origin + ray.direction * enter;
        return contactPoint;
    }

    public static void DrawPoint(Vector3 position, Color color, float duration)
    {
        Debug.DrawRay(position, Vector3.up, color, duration);
        Debug.DrawRay(position, Vector3.down, color, duration);
        Debug.DrawRay(position, Vector3.right, color, duration);
        Debug.DrawRay(position, Vector3.left, color, duration);
        Debug.DrawRay(position, Vector3.forward, color, duration);
        Debug.DrawRay(position, Vector3.back, color, duration);
    }
}

