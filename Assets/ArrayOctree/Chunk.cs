using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk<T>
{
    T[] voxels;
    ChunkSettings chunkSettings;
    public Chunk(ChunkSettings chunkSettings)
    {
        voxels = new T[(int)Mathf.Pow(chunkSettings.ChunkSL, 3)];
        this.chunkSettings = chunkSettings;
    }

    public ChunkSettings ChunkSettings { get => chunkSettings; set => chunkSettings = value; }
    
    public T GetVoxel(Vector3Int pos)
    {
        int index = pos.z * chunkSettings.ChunkSLsqr + pos.y * chunkSettings.ChunkSL + pos.x;
        return voxels[index];
    }
    public void SetVoxel(Vector3Int pos, T voxel)
    {
        int index = pos.z * chunkSettings.ChunkSLsqr + pos.y * chunkSettings.ChunkSL + pos.x;
        voxels[index] = voxel;
    }
}
public struct Voxel
{
    // xx xx xx xx
    // r  g  b  special
    byte material;
    byte density;

    public bool IsSurface
    {
        set
        {
            if(value) material = (byte)(0b10000000 | material);
            else material = (byte)(0b01111111 & material);
        }
        get
        {
            return ((0b10000000 & material) == 0b10000000);
        }
    }
    public bool IsAir
    {
        set
        {
            if(value) material = (byte)(0b01000000 | material);
            else material = (byte)(0b10111111 & material);
        }
        get
        {
            return ((0b01000000 & material) == 0b01000000);
        }
    }

    public byte Color
    {
        set
        {
            byte input = (byte)(value & 0b00111111); // input = 00cccccc
            material = (byte)(0b11000000 & material); // material = ??000000
            material = (byte)(material | input); // material = ??cccccc
        }
        get
        {
            return (byte)(0b00111111 & material);
        }
    }
    public byte Density
    {
        set { density = value; }
        get { return density; }
    }
    public byte Material
    {
        set { material = value; }
        get { return material; }
    }

    // material
    //  X         X          XX XX XX
    //  isSurface isAir      rr gg bb 
}
