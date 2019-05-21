using UnityEngine;
using System.Collections;

public class ChunkSurfaceExtractor
{
    World<Voxel> world;
    ChunkSettings chunkSettings;
    readonly int[] sides = { -1, 1 };
    int surfaceCounter = 0;
    int internalCounter = 0;

    public ChunkSurfaceExtractor(World<Voxel> world)
    {
        this.world = world;
        this.chunkSettings = world.chunkSettings;
    }
    /*
    // two layers must have same SLs
    public void ExtractLayer(ChunkLayer voxelLayer, ChunkLayer surfaceVoxelLayer)
    {
        Vector3Int voxelSLs = Utility.Multiply(voxelLayer.ChunkLayerSLs, chunkSettings.VoxelSize);
        for(int x = 0; x < voxelSLs.x; x++)
        {
            for (int y = 0; y < voxelSLs.y; y++)
            {
                for (int z = 0; z < voxelSLs.z; z++)
                {
                    Vector3Int voxelIndexInLayer = new Vector3Int(x, y, z);
                    ExtractVoxel(voxelIndexInLayer, voxelLayer, surfaceVoxelLayer);
                }
            }
        }
    }

    public void ExtractChunk(Vector3Int chunkIndexInLayer, ChunkLayer voxelLayer, ChunkLayer surfaceVoxelLayer)
    {
        //for(int i)
        Chunk voxelChunk = 
    }

    public void ExtractVoxel(Vector3Int voxelIndexInLayer, ChunkLayer voxelLayer, ChunkLayer surfaceVoxelLayer)
    {
        Vector3Int voxelIndex;
        Vector3Int chunkIndex;
        Utility.GetChunkAndVoxelIndex(voxelIndexInLayer, chunkSettings, out chunkIndex, out voxelIndex);

        Voxel voxel = voxelLayer.GetVoxel(voxelIndexInLayer);
        if (IsAir(voxel))
        {
            // don't extract
            // air is air, not extracted voxels will be air
            surfaceVoxelLayer.SetVoxel(chunkIndex, voxelIndex, new Voxel());
            return;
        }
        else
        {
            Chunk chunk = voxelLayer.GetChunk(chunkIndex);
            bool surroundedByAir = false;

            if (IsChunkBorder(voxelIndex))
            {
                surroundedByAir = SurroundedByAir(voxelIndexInLayer, voxelLayer);
            }
            else
            {
                surroundedByAir = SurroundedByAir(voxelIndex, chunk); // really getting optimization here? (yeah, neighbour search will be fast)
            }


            if (surroundedByAir)
            {
                // its a surface voxel, extract (copy)
                surfaceVoxelLayer.SetVoxel(chunkIndex, voxelIndex, voxel);
                Debug.Log("a surface voxel");
                return;
            }
            else
            {
                // dont extract
                surfaceVoxelLayer.SetVoxel(chunkIndex, voxelIndex, new Voxel());
                return;
            }
        }
    }

    */


    /*
    // has "air" neighbour
    bool SurroundedByAir(Vector3Int voxelIndex, Chunk chunk)
    {
        foreach (int x in sides)
        {
            foreach (int y in sides)
            {
                foreach (int z in sides)
                {
                    Vector3Int index = voxelIndex + new Vector3Int(x, y, z);
                    Voxel voxel = chunk.GetVoxel(index);
                    if (voxel.IsAir)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    */
    public void CalculateLayer(ChunkLayer<Voxel> voxelLayer)
    {
        surfaceCounter = 0;
        internalCounter = 0;

        Vector3Int voxelSLs = Utility.Multiply(voxelLayer.ChunkLayerSLs, chunkSettings.ChunkSL);
        for (int x = 0; x < voxelSLs.x; x++)
        {
            for (int y = 0; y < voxelSLs.y; y++)
            {
                for (int z = 0; z < voxelSLs.z; z++)
                {
                    Vector3Int voxelIndexInLayer = new Vector3Int(x, y, z);
                    CalculateVoxel(voxelIndexInLayer, voxelLayer);
                }
            }
        }

        Debug.Log("surface: " + surfaceCounter + ", internal: " + internalCounter);
    }
    public void CalculateVoxel(Vector3Int voxelIndexInLayer, ChunkLayer<Voxel> voxelLayer)
    {
        Voxel voxel = voxelLayer.GetVoxel(voxelIndexInLayer);
        if (CheckIfSurface(voxelIndexInLayer, voxelLayer))
        {
            voxel.IsSurface = true;
            Debug.DrawRay(voxelIndexInLayer, Random.insideUnitSphere * 0.2f, Color.green, 1000);
            surfaceCounter++;
        }
        else
        {
            voxel.IsSurface = false;
            internalCounter++;
        }
        voxelLayer.SetVoxel(voxelIndexInLayer, voxel);
    }
    bool CheckIfSurface(Vector3Int voxelIndexInLayer, ChunkLayer<Voxel> voxelLayer)
    {
        if (IsLayerBorder(voxelLayer, voxelIndexInLayer) == false)
        {
            Voxel thisVoxel = voxelLayer.GetVoxel(voxelIndexInLayer);
            if (thisVoxel.IsAir) return false;
            foreach (int x in sides)
            {
                foreach (int y in sides)
                {
                    foreach (int z in sides)
                    {
                        Vector3Int index = voxelIndexInLayer + new Vector3Int(x, y, z);

                        Voxel voxel = voxelLayer.GetVoxel(index);
                        if (voxel.IsAir)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        return true; // assuming border voxels are a
    }
    // better not use this
    bool IsLayerBorder(ChunkLayer<Voxel> layer, Vector3Int voxelIndexInLayer)
    {
        Vector3Int layerVoxelSL = Utility.Multiply(layer.ChunkLayerSLs, chunkSettings.ChunkSL);
        if (voxelIndexInLayer.x == 0 || voxelIndexInLayer.y == 0 || voxelIndexInLayer.z == 0 || voxelIndexInLayer.x == layerVoxelSL.x - 1 || voxelIndexInLayer.y == layerVoxelSL.y - 1 || voxelIndexInLayer.z == layerVoxelSL.z - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool IsChunkBorder(Vector3Int voxelIndex)
    {
        int chunkSL = chunkSettings.ChunkSL;
        if (voxelIndex.x == 0 || voxelIndex.x == chunkSL - 1 || voxelIndex.y == 0 || voxelIndex.y == chunkSL - 1 || voxelIndex.z == 0 || voxelIndex.z == chunkSL - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
