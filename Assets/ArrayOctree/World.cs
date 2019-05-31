using UnityEngine;
using System.Collections;

public class ChunkSettings
{
    int chunkSL; // 16
    int chunkSLsqr; // 16 * 16
    float voxelSize; //1
    float chunkWorldLen; // 16 * 1 = 16

    public ChunkSettings(int chunkSL, float voxelSize)
    {
        this.chunkSL = chunkSL;
        this.chunkSLsqr = chunkSL * chunkSL;
        this.voxelSize = voxelSize;
        this.chunkWorldLen = chunkSL * voxelSize;
    }

    public int ChunkSL { get => chunkSL; }
    public int ChunkSLsqr { get => chunkSLsqr; }
    public float VoxelSize { get => voxelSize; }
    public float ChunkWorldLen { get => chunkWorldLen; } // for main level
}


public class World<T>
{
    public ChunkSettings chunkSettings;
    Vector3Int voxelSLs; // 256, 64, 256
    Vector3 worldStart;

    public ChunkLayer<T> mainLayer;
    //public ChunkLayer lodLayers;


    public World(ChunkSettings chunkSettings, Vector3Int voxelSLs)
    {
        this.chunkSettings = chunkSettings;
        this.voxelSLs = voxelSLs;
        AllocateMainLayer();
        //GenerateMainLayer();
    }

    public T GetVoxel(Vector3 position)
    {
        Vector3Int voxelIndexInLayer = GetIndexInLayer(position, mainLayer);
        return mainLayer.GetVoxel(voxelIndexInLayer);
    }
    public void SetVoxel(Vector3 position, T voxel)
    {
        Vector3Int voxelIndexInLayer = GetIndexInLayer(position, mainLayer);
        mainLayer.SetVoxel(voxelIndexInLayer, voxel);
    }

    Vector3Int GetIndexInLayer(Vector3 position, ChunkLayer<T> layer)
    {
        //position += layer.VoxelSize * 0.5f * Vector3.one;
        Vector3 quotinent = position / layer.VoxelSize;
        Vector3Int layerIndex = quotinent.ToVector3Int();

        return layerIndex;
    }

    

    void AllocateMainLayer()
    {
        Vector3Int chunkSLs = new Vector3Int(voxelSLs.x / chunkSettings.ChunkSL, voxelSLs.y / chunkSettings.ChunkSL, voxelSLs.z / chunkSettings.ChunkSL);
        mainLayer = new ChunkLayer<T>(chunkSLs, chunkSettings.VoxelSize * chunkSettings.ChunkSL, chunkSettings);

        //worldStart = -new Vector3(mainLevel.ChunkLayerSLs.x * 0.5f, mainLevel.ChunkLayerSLs.y * 0.5f, mainLevel.ChunkLayerSLs.z * 0.5f) * chunkSettings.ChunkWorldLen;
        worldStart = Vector3.zero;
    }
    /*
    void GenerateMainLayer()
    {
        ChunkGenerator chunkGenerator = new ChunkGenerator(chunkSettings);
        float chunkWorldLen = chunkSettings.ChunkWorldLen;

        for (int z = 0; z < mainLayer.ChunkLayerSLs.z; z++)
        {
            for (int y = 0; y < mainLayer.ChunkLayerSLs.y; y++)
            {
                for (int x = 0; x < mainLayer.ChunkLayerSLs.x; x++)
                {
                    Vector3 chunkCenterPos = worldStart + new Vector3(x, y, z) * chunkWorldLen;

                    Chunk<Voxel> chunk = new Chunk<Voxel>(chunkSettings);
                    chunkGenerator.GenerateChunk(chunk, chunkCenterPos);

                    Vector3Int index = new Vector3Int(x, y, z);
                    mainLayer.SetChunk(index, chunk);
                }
            }
        }
    }
    */



}
