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
    /*
    public static Vector3Int ToVector3Int(this byte direction)
    {
        Vector3Int output = new Vector3Int(-1, -1, -1);
        if((direction & 0b001) == 0b001)
        {
            output.x = 1;
        }
        if ((direction & 0b010) == 0b010)
        {
            output.y = 1;
        }
        if ((direction & 0b100) == 0b100)
        {
            output.z = 1;
        }
        return output;
    }
    public static byte ToDirection(this Vector3Int vector3Int)
    {
        byte direction = 0;
        if(vector3Int.x > 0)
        {
            direction += 1;
        }
        if (vector3Int.y > 0)
        {
            direction += 2;
        }
        if (vector3Int.z > 0)
        {
            direction += 4;
        }
        return direction;
    }

    public static Vector3Int ToVector3Int(this byte direction)
    {
        Vector3Int output = new Vector3Int(-1, -1, -1);
        if ((direction & 0b001) == 0b001)
        {
            output.x = 1;
        }
        else if ((direction & 0b010) == 0b010)
        {
            output.y = 1;
        }
        else if ((direction & 0b100) == 0b100)
        {
            output.z = 1;
        }

        if((direction & 0b1000) == 0b1000)
        {
            output.x *= -1;
            output.y *= -1;
            output.z *= -1;
        }
        return output;
    }
    */

    // direction: XXYYZZ
    // direction: [-1,0,1,2] [-1,0,1,2] [-1,0,1,2]
    // dont ever give 0b11 to field
    public static Vector3Int ToVector3Int(this byte direction)
    {
        int z = (direction & 0b110000) >> 4;
        int y = (direction & 0b001100) >> 2;
        int x = (direction & 0b000011);

        Vector3Int output = new Vector3Int(x - 1, y - 1, z - 1);
        return output;
    }
    public static byte ToDirection(this Vector3Int vector3Int)
    {
        byte direction = 0b010101; // 0,0,0
        if (vector3Int.x != 0)
        {
            if(vector3Int.x > 0)
            {
                direction += 0b000001;
            }
            else
            {
                direction -= 0b000001;
            }
        }
        if (vector3Int.y != 0)
        {
            if (vector3Int.y > 0)
            {
                direction += 0b000100;
            }
            else
            {
                direction -= 0b000100;
            }
        }
        if (vector3Int.z != 0)
        {
            if (vector3Int.z > 0)
            {
                direction += 0b010000;
            }
            else
            {
                direction -= 0b010000;
            }
        }
        return direction;
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

