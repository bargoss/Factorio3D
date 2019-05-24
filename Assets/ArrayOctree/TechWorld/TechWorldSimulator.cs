using UnityEngine;
using System.Collections;

public class TechWorldSimulator
{
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
        for(int x = 0; x < chunkSettings.ChunkSL; x++)
        {
            for (int y = 0; y < chunkSettings.ChunkSL; y++)
            {
                for (int z = 0; z < chunkSettings.ChunkSL; z++)
                {
                    Vector3Int indexInChunk = new Vector3Int(x, y, z);
                    Block block = chunk.GetVoxel(indexInChunk);
                    TechnicalBlock technicalBlock = block.technicalBlock;
                    if(technicalBlock != null)
                    {
                        technicalBlock.Update(deltaTime);
                        if (technicalBlock.UpdatesNeighbour)
                        {
                            Vector3Int myVoxelIndexInLayer = chunkIndex.Multiply(chunkSL) + indexInChunk;
                            TechnicalBlock[] targetTechBlocks = new TechnicalBlock[technicalBlock.requestedNeighbours.Length];
                            
                            for (int i = 0; i < technicalBlock.requestedNeighbours.Length; i++)
                            {
                                byte targetDirection = technicalBlock.requestedNeighbours[i];
                                Vector3Int delta = targetDirection.ToVector3Int();
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
                    }
                }
            }
        }
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