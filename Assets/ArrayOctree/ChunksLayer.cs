using UnityEngine;
using System.Collections;

public class ChunkLayer<T>
{
    Chunk<T>[] chunks;
    ChunkSettings chunkSettings;
    Vector3Int chunkLayerSLs;
    float chunkWorldLength;
    float voxelSize;
    int yJump;
    int zJump;
    public ChunkLayer(Vector3Int chunkLayerSLs, float chunkWorldLength, ChunkSettings chunkSettings)
    {
        this.chunkSettings = chunkSettings;
        this.chunkWorldLength = chunkWorldLength;
        this.chunkLayerSLs = chunkLayerSLs;

        chunks = new Chunk<T>[chunkLayerSLs.x * chunkLayerSLs.y * chunkLayerSLs.z];
        InitializeChunks();

        yJump = chunkLayerSLs.x;
        zJump = chunkLayerSLs.x * chunkLayerSLs.y;
        voxelSize = (int)(chunkWorldLength / chunkSettings.ChunkWorldLen)* chunkSettings.VoxelSize; // hmm
    }

    public Vector3Int ChunkLayerSLs { get => chunkLayerSLs; }
    public float ChunkWorldLength { get => chunkWorldLength; }
    public ChunkSettings ChunkSettings { get => chunkSettings; }
    public float VoxelSize { get => voxelSize; }

    public Chunk<T> GetChunk(Vector3Int pos)
    {
        int index = pos.z * zJump + pos.y * yJump + pos.x;
        return chunks[index];
    }
    public void SetChunk(Vector3Int pos, Chunk<T> chunk)
    {
        int index = pos.z * zJump + pos.y * yJump + pos.x;
        chunks[index] = chunk;
    }
    // use Chunk.GetVoxel() if possible
    public T GetVoxel(Vector3Int voxelIndexInLayer)
    {
        ChunkLayer<T> layer = this;
        Vector3Int chunkIndex3D;
        Vector3Int voxelIndex3D;

        Utility.GetChunkAndVoxelIndex(voxelIndexInLayer, chunkSettings, out chunkIndex3D, out voxelIndex3D);

        Chunk<T> chunk = layer.GetChunk(chunkIndex3D);
        return chunk.GetVoxel(voxelIndex3D);
    }
    public void SetVoxel(Vector3Int voxelIndexInLayer, T voxel)
    {
        ChunkLayer<T> layer = this;
        Vector3Int chunkIndex3D;
        Vector3Int voxelIndex3D;

        Utility.GetChunkAndVoxelIndex(voxelIndexInLayer, chunkSettings ,out chunkIndex3D, out voxelIndex3D);

        Chunk<T> chunk = layer.GetChunk(chunkIndex3D);
        chunk.SetVoxel(voxelIndex3D ,voxel);
    }
    // faster versions
    public T GetVoxel(Vector3Int chunkIndex, Vector3Int voxelIndex)
    {
        ChunkLayer<T> layer = this;

        Chunk<T> chunk = layer.GetChunk(chunkIndex);
        return chunk.GetVoxel(voxelIndex);
    }
    public void SetVoxel(Vector3Int chunkIndex, Vector3Int voxelIndex,T voxel)
    {
        ChunkLayer<T> layer = this;

        Chunk<T> chunk = layer.GetChunk(chunkIndex);
        chunk.SetVoxel(voxelIndex, voxel);
    }

    void InitializeChunks()
    {
        for(int i = 0; i < chunks.Length; i++)
        {
            chunks[i] = new Chunk<T>(chunkSettings);
        }
    }
}
