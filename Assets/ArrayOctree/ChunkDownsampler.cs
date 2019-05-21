using System;
using System.Collections.Generic;
using UnityEngine;
public class ChunkDownsampler
{
    ChunkSettings chunkSettings;
    int[] sides = { -1, 1 };

    public ChunkDownsampler(ChunkSettings chunkSettings)
    {
        this.chunkSettings = chunkSettings;
    }


    /*
    ChunkLayer DownsampleLayer(ChunkLayer layer)
    {
        Vector3Int initialLayerSLs = layer.ChunkLayerSLs;
        Vector3Int newLayerSLs = new Vector3Int((int)(initialLayerSLs.x / 2.0f), (int)(initialLayerSLs.y / 2.0f), (int)(initialLayerSLs.z / 2.0f));

        if(newLayerSLs.x * newLayerSLs.y * newLayerSLs.z == 0) { Debug.LogError("layer cannot make chunks"); }

        ChunkLayer chunkLayer = new ChunkLayer(newLayerSLs, 2 * layer)
    }
    Voxel DownsampleVoxels(Voxel[] voxels)
    {
        Voxel downsampledVoxel = new Voxel();
        float totalMass = 0;
        float material = 0;

        for (int i = 0; i < 8; i++)
        {
            Voxel voxel = voxels[i];
            float massOfVoxel = 0;
            if(voxel.material != 0)
            {
                massOfVoxel = voxel.density;
            }

            totalMass += massOfVoxel;
        }

        downsampledVoxel.material = 1; // templorary
        downsampledVoxel.density = (byte)(totalMass / 8);
        return downsampledVoxel;
    }
    Voxel DownsampleVoxels(Chunk sourceChunk, Chunk destinationChunk, Vector3Int sourceIndexStart)
    {
        Voxel[] sourceVoxels = new Voxel[8];
        int i = 0;
        for(int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    Vector3Int delta = new Vector3Int(x, y, z);
                    Vector3Int sourceIndex = sourceIndexStart + delta;
                    sourceVoxels[i] = s
                }
            }
        }
    }
    Chunk DownsampleChunks(Chunk[] chunks)
    {
        Chunk downsampledChunk = new Chunk(chunks[0].ChunkSettings);
        
    }
    */

        // downsampling from lower to higher
    void DownsampleVoxel(Vector3Int voxelIndexInHigherLayer, ChunkLayer<Voxel> higherLayer, ChunkLayer<Voxel> lowerLayer)
    {
        
    }

    /*
    Voxel DownsampleVoxels(Voxel[] voxels)
    {
        Voxel downsampledVoxel = new Voxel();
        int rSum = 0;
        int gSum = 0;
        int bSum = 0;
        int densitySum;

        for (int i = 0; i < 8; i++)
        {
            Voxel voxel = voxels[i];
            int color = voxel.Color;
            int r = color & 0b110000;

        }

    }
    */

    // if voxel is not on the chunk limit
    
}

