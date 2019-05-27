﻿using UnityEngine;
using System.Collections;
using UnityEngine;

public class TechWorld // world + tech stuff
{
    public ChunkSettings chunkSettings;

    Item[] items;
    Recipe[] recipes;
    public World<Block> world;
    public TechWorldSimulator worldSimulator;

    public TechWorld(ItemsContainer itemsContainer, ChunkSettings chunkSettings, Vector3Int worldSLs)
    {
        items = itemsContainer.items;
        recipes = itemsContainer.recipes;

        this.chunkSettings = chunkSettings;
        world = new World<Block>(chunkSettings, worldSLs);

        worldSimulator = new TechWorldSimulator(chunkSettings);
    }
    
    public Block GetElement(Vector3 position)
    {
        return world.GetVoxel(position);
    }
    public void SetElement(Vector3 position, Block element, Matrix4x4 transform)
    {
        // Call previous technicalBlock's OnDestroy
        Block previousElement = GetElement(position);
        if(previousElement.technicalBlock != null) { previousElement.technicalBlock.OnDestroy(); }

        // Create the new element
        InitializeTechBlock(ref element, transform);
        world.SetVoxel(position, element);
    }
    public void SetElement(Matrix4x4 transform, Block element, TechnicalGoInfo technicalGoInfo)
    {
        // Call previous technicalBlock's OnDestroy
        Block previousElement = GetElement(transform.GetColumn(3));
        if (previousElement.technicalBlock != null) { previousElement.technicalBlock.OnDestroy(); }

        // Create the new element
        InitializeGoConnection(ref element, transform, technicalGoInfo);
        world.SetVoxel(transform.GetColumn(3), element);
    }
    public void SimulateWorld(float deltaTime)
    {
        ChunkLayer<Block> chunkLayer = world.mainLayer;
        Vector3Int chunkLayerSLs = chunkLayer.ChunkLayerSLs;
        for(int x = 0; x < chunkLayerSLs.x; x++)
        {
            for (int y = 0; y < chunkLayerSLs.y; y++)
            {
                for (int z = 0; z < chunkLayerSLs.z; z++)
                {
                    Vector3Int chunkIndex = new Vector3Int(x, y, z);
                    worldSimulator.SimulateChunk(chunkIndex, chunkLayer, deltaTime);
                }
            }
        }
    }

    void InitializeGoConnection(ref Block block, Matrix4x4 transform, TechnicalGoInfo technicalGoInfo)
    {
        block.technicalBlock = new GoConnection(transform,technicalGoInfo);
    }
    void InitializeTechBlock(ref Block block, Matrix4x4 transform)
    {
        Item.TechBlockType techBlockType = items[block.blockType].techBlockType;
        switch (techBlockType)
        {
            case Item.TechBlockType.NonTechnical:
                break;
            case Item.TechBlockType.Conveyor:
                block.technicalBlock = new Conveyor(transform);
                break;
            case Item.TechBlockType.Fabricator:
                block.technicalBlock = new Assembler();
                break;
            case Item.TechBlockType.Inserter:
                block.technicalBlock = new Inserter(transform);
                break;
        }
    }
}
