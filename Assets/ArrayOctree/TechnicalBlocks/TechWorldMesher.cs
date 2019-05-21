using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TechWorldMesher
{
    ChunkSettings chunkSettings;
    int chunkSL;

    public TechWorldMesher(ChunkSettings chunkSettings)
    {
        this.chunkSL = chunkSettings.ChunkSL;
        this.chunkSettings = chunkSettings;
    }
    public InstancedMeshInfo MeshChunkBlocks(Vector3Int chunkIndex, ChunkLayer<Block> chunkLayer)
    {
        InstancedMeshInfo instancedMeshInfo = new InstancedMeshInfo();
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

                    Quaternion rotation = Quaternion.identity;
                    TechnicalBlock technicalBlock = block.technicalBlock;
                    if(technicalBlock != null)
                    {
                        Vector3 lookDirection = technicalBlock.GetLookDirection().ToVector3Int();
                        rotation = Quaternion.LookRotation(lookDirection);
                    }
                    Matrix4x4 transform = Matrix4x4.TRS(position, rotation, Vector3.one * chunkSettings.VoxelSize * 0.5f);
                    instancedMeshInfo.types[blockType].AddTransform(transform);
                }
            }
        }
        return instancedMeshInfo;
    }
    /*
    */
    public InstancedMeshInfo MeshChunkItems(Vector3Int chunkIndex, ChunkLayer<Block> chunkLayer)
    {
        InstancedMeshInfo instancedMeshInfo = new InstancedMeshInfo();
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
                    //Matrix4x4 transform = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one * chunkSettings.VoxelSize);
                    Block block = chunk.GetVoxel(indexInChunk);
                    int blockType = block.blockType;
                    //instancedMeshInfo.blockTypes[blockType].AddTransform(transform);
                    TechnicalBlock technicalBlock = block.technicalBlock;
                    if(technicalBlock != null)
                    {
                        if (technicalBlock.RendersItems)
                        {
                            TechnicalBlock.ItemMesh[] itemMeshes = technicalBlock.GetItemsMesh(position);
                            for(int i = 0; i < itemMeshes.Length; i++)
                            {
                                if (itemMeshes[i].itemType != 0)
                                {
                                    Matrix4x4 transform = Matrix4x4.TRS(itemMeshes[i].position + position, Quaternion.identity, Vector3.one * 0.25f); ;
                                    instancedMeshInfo.AddInstance(transform, itemMeshes[i].itemType);
                                }
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
    readonly static int typeCount = 6;
    public InstancedMeshInfoType[] types; // type 0 is air, always empty
    //public InstanceMeshInfoType[] itemTypes;// later
    public InstancedMeshInfo()
    {
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
}
