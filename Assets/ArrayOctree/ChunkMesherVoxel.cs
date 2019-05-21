using UnityEngine;
using System.Collections;
using MarchingCubesProject;
using System.Collections.Generic;

public class VoxelChunkMesher
{
    // meshes regular voxels only

    float[] renderBuffer;
    ChunkSettings chunkSettings;
    int sl;
    int slSqr;

    public VoxelChunkMesher(ChunkSettings chunkSettings)
    {
        int sideLen = chunkSettings.ChunkSL;
        sl = sideLen + 1;
        slSqr = sl * sl;

        renderBuffer = new float[(int)Mathf.Pow(sideLen + 1,3)];
        this.chunkSettings = chunkSettings;
    }

    public Mesh MeshChunk(Vector3Int chunkIndex, ChunkLayer<Voxel> chunkLayer)
    {
        BufferIntoDensityArray(chunkIndex, chunkLayer);
        return MeshDensityArray();
    }
    Mesh MeshDensityArray()
    {
        Marching marching = new MarchingCubes();
        marching.Surface = 0.0f;

        int sl = chunkSettings.ChunkSL + 1;
        int width = sl;
        int height = sl;
        int length = sl;
        



        List<Vector3> verts = new List<Vector3>();
        List<int> indices = new List<int>();

        //The mesh produced is not optimal. There is one vert for each index.
        //Would need to weld vertices for better quality mesh.
        marching.Generate(renderBuffer, width, height, length, verts, indices);
        List<Color32> colors = new List<Color32>();
        foreach(Vector3 vertex in verts)
        {
            colors.Add(Random.ColorHSV());
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.colors32 = colors.ToArray();
        mesh.RecalculateNormals();


        return mesh;
    }

    void BufferIntoDensityArray(Vector3Int chunkIndex, ChunkLayer<Voxel> chunkLayer)
    {
        Chunk<Voxel> chunk = chunkLayer.GetChunk(chunkIndex);
        Vector3Int voxelZeroLayerIndex = chunkIndex.Multiply(chunkSettings.ChunkSL);
        Vector3Int layerVoxelWorldSLs = chunkLayer.ChunkLayerSLs.Multiply(chunkSettings.ChunkSL);
        
        for (int z = 0; z < chunkSettings.ChunkSL + 1; z++)
        {
            bool zOut = (z == chunkSettings.ChunkSL);
            for (int y = 0; y < chunkSettings.ChunkSL + 1; y++)
            {
                bool yOut = (y == chunkSettings.ChunkSL);
                for (int x = 0; x < chunkSettings.ChunkSL + 1; x++)
                {
                    bool xOut = (x == chunkSettings.ChunkSL);
                    Vector3Int index3D = new Vector3Int(x, y, z);
                    if (zOut || yOut || xOut)
                    {
                        Vector3Int indexInLayer = new Vector3Int(x, y, z) + voxelZeroLayerIndex;

                        // stay in range
                        if(indexInLayer.x < layerVoxelWorldSLs.x && indexInLayer.y < layerVoxelWorldSLs.y && indexInLayer.z < layerVoxelWorldSLs.z)
                        {
                            BufferSingleVoxel(chunkLayer, indexInLayer, index3D);
                        }
                    }
                    else
                    {
                        BufferSingleVoxel(chunk, index3D);
                    }
                }
            }
        }
    }
    /*
    void BufferBodyIntoDensityArray(Chunk<Voxel> chunk)
    {
        for (int z = 0; z < chunkSettings.ChunkSL - 1; z++)
        {
            for (int y = 0; y < chunkSettings.ChunkSL - 1; y++)
            {
                for (int x = 0; x < chunkSettings.ChunkSL - 1; x++)
                {
                    Vector3Int index3D = new Vector3Int(x, y, z);
                    BufferSingleVoxel(chunk, index3D);
                }
            }
        }
    }
    void BufferBordersIntoDensityArray(Chunk<Voxel> chunk)
    {


        {
            int z = chunkSettings.ChunkSL - 1;
            for (int y = 0; y < chunkSettings.ChunkSL; y++)
            {
                for (int x = 0; x < chunkSettings.ChunkSL; x++)
                {
                    Vector3Int index3D = new Vector3Int(x, y, z);
                    BufferSingleVoxel(chunk, index3D);
                }
            }
        }
        for (int z = 0; z < chunkSettings.ChunkSL; z++)
        {
            for (int y = 0; y < chunkSettings.ChunkSL; y++)
            {
                for (int x = 0; x < chunkSettings.ChunkSL; x++)
                {
                    Vector3Int index3D = new Vector3Int(x, y, z);
                    BufferSingleVoxel(chunk, index3D);
                }
            }
        }
    }
    */
    void BufferSingleVoxel(Chunk<Voxel> chunk, Vector3Int index3D)
    {
        int index = index3D.z * slSqr + index3D.y * sl + index3D.x;

        Voxel voxel = chunk.GetVoxel(index3D);
        renderBuffer[index] = ConvertToArrayDensity(voxel);
    }
    void BufferSingleVoxel(ChunkLayer<Voxel> chunkLayer, Vector3Int indexInLayer, Vector3Int index3D)
    {
        int index = index3D.z * slSqr + index3D.y * sl + index3D.x;

        Voxel voxel = chunkLayer.GetVoxel(indexInLayer);
        try
        {
            renderBuffer[index] = ConvertToArrayDensity(voxel);
        }
        catch
        {
            Debug.Log("hey");
        }
    }

    float ConvertToArrayDensity(Voxel voxel)
    {
        float density = 1;
        if (voxel.IsAir == false) // air material has 0 density;
        {
            density = density - 2 * (voxel.Density / 255.0f);
        }
        return density;
    }
}
