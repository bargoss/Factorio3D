using System;
using System.Collections.Generic;
using UnityEngine;

public class TechWorldMono : MonoBehaviour
{
    public ItemsContainer itemsContainer;

    TechWorld world;
    TechWorldMesher chunkMesher;
    TechWorldRenderer worldRenderer;

    ChunkSettings chunkSettings;

    public void Start()
    {
        chunkSettings = new ChunkSettings(16, 1);
        world = new TechWorld(itemsContainer, chunkSettings, new Vector3Int(64, 16, 64));
        chunkSettings = world.chunkSettings;

        chunkMesher = new TechWorldMesher(chunkSettings);
        worldRenderer = new TechWorldRenderer(itemsContainer);
        
        print("Ready");
    }
    public void Update()
    {
        MeshAllChunks_Items();
        DrawMeshInfos();
    }
    public void FixedUpdate()
    {
        world.SimulateWorld(Time.fixedDeltaTime);
    }

    public void SetElement(Vector3 position, Block block) 
    {
        world.SetElement(position, block);
        MeshAllChunks_Blocks();
    }
    /*
    public void SetElement(Vector3 position, Quaternion rotation, Block block, TechnicalGoInfo technicalGoInfo)
    {
        world.SetElement(position, rotation ,block, technicalGoInfo);
        MeshAllChunks_Blocks();
    }
    */
    public Block GetElement(Vector3 position)
    {
        return world.GetElement(position);
    }
    public void MeshAllChunks_Blocks()
    {
        Vector3Int chunkLayerSLs = world.world.mainLayer.ChunkLayerSLs;
        worldRenderer.blockMeshInfos.Clear();
        for(int x = 0; x < chunkLayerSLs.x; x++)
        {
            for (int y = 0; y < chunkLayerSLs.y; y++)
            {
                for (int z = 0; z < chunkLayerSLs.z; z++)
                {
                    Vector3Int chunkIndex = new Vector3Int(x, y, z);
                    InstancedMeshInfo instancedMeshInfo = chunkMesher.MeshChunkBlocks(chunkIndex, world.world.mainLayer);
                    worldRenderer.blockMeshInfos.Add(chunkIndex, instancedMeshInfo);
                }
            }
        }
    }
    public void MeshAllChunks_Items()
    {
        Vector3Int chunkLayerSLs = world.world.mainLayer.ChunkLayerSLs;
        worldRenderer.itemMeshInfos.Clear();
        for (int x = 0; x < chunkLayerSLs.x; x++)
        {
            for (int y = 0; y < chunkLayerSLs.y; y++)
            {
                for (int z = 0; z < chunkLayerSLs.z; z++)
                {
                    Vector3Int chunkIndex = new Vector3Int(x, y, z);
                    InstancedMeshInfo instancedMeshInfo = chunkMesher.MeshChunkItems(chunkIndex, world.world.mainLayer);
                    worldRenderer.itemMeshInfos.Add(chunkIndex, instancedMeshInfo);
                }
            }
        }
    }

    public void DrawMeshInfos()
    {
        worldRenderer.Draw();
    }
    
}

