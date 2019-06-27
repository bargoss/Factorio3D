using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TechWorldMesher
{
    ChunkSettings chunkSettings;
    int chunkSL;
    int modelTypeCount;

    public TechWorldMesher(ChunkSettings chunkSettings, int modelTypeCount)
    {
        this.modelTypeCount = modelTypeCount;
        this.chunkSL = chunkSettings.ChunkSL;
        this.chunkSettings = chunkSettings;
    }
    public InstancedMeshInfo MeshChunkBlocks(Vector3Int chunkIndex, ChunkLayer<Block> chunkLayer)
    {
        InstancedMeshInfo instancedMeshInfo = new InstancedMeshInfo(modelTypeCount);
        Chunk<Block> chunk = chunkLayer.GetChunk(chunkIndex);
        Vector3 chunkStart = chunkIndex.Multiply(chunkSettings.ChunkWorldLen);

        for (int x = 0; x < chunkSL; x++)
        {
            for (int y = 0; y < chunkSL; y++)
            {
                for (int z = 0; z < chunkSL; z++)
                {
                    Vector3Int indexInChunk = new Vector3Int(x, y, z);
                    Vector3 position = new Vector3(x, y, z) * chunkSettings.VoxelSize + chunkStart;
                    Block block = chunk.GetVoxel(indexInChunk);

                    TechnicalBlock technicalBlock = block.technicalBlock;
                    if(technicalBlock != null)
                    {
                        instancedMeshInfo.AddModelInfos(technicalBlock.GetStaticMesh(), Matrix4x4.Translate(position) * technicalBlock.localTransform * Matrix4x4.Scale(Vector3.one * 0.5f));
                    }
                    
                }
            }
        }
        return instancedMeshInfo;
    }
    /*
    */
    public InstancedMeshInfo MeshChunkItems(Vector3Int chunkIndex, ChunkLayer<Block> chunkLayer)
    {
        InstancedMeshInfo instancedMeshInfo = new InstancedMeshInfo(modelTypeCount);
        Chunk<Block> chunk = chunkLayer.GetChunk(chunkIndex);
        Vector3 chunkStart = chunkIndex.Multiply(chunkSettings.ChunkWorldLen);

        for (int x = 0; x < chunkSL; x++)
        {
            for (int y = 0; y < chunkSL; y++)
            {
                for (int z = 0; z < chunkSL; z++)
                {
                    Vector3Int indexInChunk = new Vector3Int(x, y, z);
                    Vector3 position = new Vector3(x, y, z) * chunkSettings.VoxelSize + chunkStart;
                    Block block = chunk.GetVoxel(indexInChunk);
                    int blockType = block.blockType;
                    TechnicalBlock technicalBlock = block.technicalBlock;
                    if (technicalBlock != null)
                    {
                        TechnicalBlock.ModelInfo[] itemMeshes = technicalBlock.GetDynamicMesh();
                        for (int i = 0; i < itemMeshes.Length; i++)
                        {
                            if (itemMeshes[i].modelType != 0)
                            {
                                Matrix4x4 transform = itemMeshes[i].transform;
                                transform = Matrix4x4.Translate(position) * transform;
                                instancedMeshInfo.AddInstance(transform, itemMeshes[i].modelType);
                            }
                        }
                    }
                }
            }
        }
        return instancedMeshInfo;
    }
}


public class InstancedMeshInfo
{
    int typeCount;
    public InstancedMeshInfoType[] types; // type 0 is air, always empty
    //public InstanceMeshInfoType[] itemTypes;// later
    public InstancedMeshInfo(int typeCount)
    {
        this.typeCount = typeCount;
        types = new InstancedMeshInfoType[typeCount];
        for(int i = 0; i < typeCount; i++)
        {
            types[i] = new InstancedMeshInfoType();
        }
    }
    public void AddInstance(Matrix4x4 transform, int type)
    {
        types[type].AddTransform(transform);
    }
    public void AddModelInfos(TechnicalBlock.ModelInfo[] modelInfos, Matrix4x4 transform)
    {
        for(int i = 0; i < modelInfos.Length; i++)
        {
            int modelType = modelInfos[i].modelType;
            types[modelType].AddTransform(transform * modelInfos[i].transform);
        }
    }
}


public class InstancedMeshInfoType
{
    public List<Matrix4x4> transforms;
    public InstancedMeshInfoType()
    {
        transforms = new List<Matrix4x4>();
    }
    public void AddTransform(Matrix4x4 transform)
    {
        transforms.Add(transform);
    }
    /*
    public void AddTransform(Matrix4x4[] transforms)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            this.transforms.Add(transforms[i]);
        }
    }
    */
}
