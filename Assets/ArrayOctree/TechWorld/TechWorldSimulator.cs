using UnityEngine;
using System.Collections;

public class TechWorldSimulator
{
    static readonly byte[] iterationOrders=
    {
            0b000,
            0b001,
            0b010,
            0b011,

            0b100,
            0b101,
            0b110,
            0b111
    };
    int iterationOrdersIndex = 0;

    byte IterationOrder
    {
        get { return iterationOrders[iterationOrdersIndex]; }
    }

    bool XSign
    {
        get { return (IterationOrder & 0b001) == 0b001; }
    }
    bool YSign
    {
        get { return (IterationOrder & 0b010) == 0b010; }
    }
    bool ZSign
    {
        get { return (IterationOrder & 0b100) == 0b100; }
    }

    ChunkSettings chunkSettings;
    public TechWorldSimulator(ChunkSettings chunkSettings)
    {
        this.chunkSettings = chunkSettings;
    }

    public void SimulateChunk(Vector3Int chunkIndex, ChunkLayer<Block> chunkLayer, float deltaTime)
    {
        int chunkSL = chunkSettings.ChunkSL;
        Chunk<Block> chunk = chunkLayer.GetChunk(chunkIndex);
        // iterate all blocks (for now)
        int o = 0;
        for(int x = 0; x < chunkSettings.ChunkSL; x++)
        {
            int realX = x;
            if (XSign)
            {
                realX = chunkSettings.ChunkSL - x - 1;
            }
            for (int y = 0; y < chunkSettings.ChunkSL; y++)
            {
                int realY = y;
                if (YSign)
                {
                    realY = chunkSettings.ChunkSL - y - 1;
                }
                for (int z = 0; z < chunkSettings.ChunkSL; z++)
                {
                    int realZ = z;
                    if (ZSign)
                    {
                        realZ = chunkSettings.ChunkSL - z - 1;
                    }
                    Vector3Int indexInChunk = new Vector3Int(realX, realY, realZ);
                    //if(o < 6) { Debug.Log(indexInChunk); o++;  }
                    Block block = chunk.GetVoxel(indexInChunk);
                    TechnicalBlock technicalBlock = block.technicalBlock;
                    if(technicalBlock != null)
                    {
                        if (technicalBlock.UpdatesNeighbour)
                        {
                            Vector3Int myVoxelIndexInLayer = chunkIndex.Multiply(chunkSL) + indexInChunk;
                            TechnicalBlock[] targetTechBlocks = new TechnicalBlock[technicalBlock.requestedNeighbours.Length];
                            
                            for (int i = 0; i < technicalBlock.requestedNeighbours.Length; i++)
                            {
                                Vector3Int delta = technicalBlock.requestedNeighbours[i];
                                Vector3Int neighbourIndexInLayer = myVoxelIndexInLayer + delta;
                                Block targetBlock = chunkLayer.GetVoxel(neighbourIndexInLayer);
                                TechnicalBlock targetTechBlock = targetBlock.technicalBlock;
                                if (targetTechBlock != null)
                                {
                                    targetTechBlocks[i] = targetTechBlock;
                                }
                            }
                            technicalBlock.UpdateNeighbour(deltaTime, targetTechBlocks);
                        }
                        technicalBlock.Update(deltaTime);
                    }
                }
            }
        }
        ChangeIterationOrder();
    }
    void ChangeIterationOrder()
    {
        iterationOrdersIndex += 1;
        iterationOrdersIndex = iterationOrdersIndex % 8;
    }
}
/*
I have this hierarchy:
```cs

public interface IItemInput
public interface IItemOutput

public class TechnicalBlock
public class Conveyor : TechnicalBlock, IItemInput, IItemOutput
public class Fabricator : TechnicalBlock, IItemInput, IItemOutput 
```

I'm trying to make a game like factorio in 3D
I have an array storing TechnicalBlock's

I am looping trough TechnicalBlock's and running their updating methods.

The problem is these subclasses should be able to read and modify some data on their neighbours
But i don't want to access that array from below the control flow
Whats the cleanest way i can preserve this control flow and still be able to modify neighbours

TechWorldSimulator.SimulateChunk() 
                                        V
TechWorldSimulator.SimulateTechnicalBlock()  
                                       V
                TechnicalBlock.Update() 
                         V                              V
Conveyor.Update() *or* Fabricator.Update()

    */