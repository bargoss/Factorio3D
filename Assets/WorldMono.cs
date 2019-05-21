using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMono : MonoBehaviour
{
    World<Voxel> world;
    VoxelChunkMesher chunkMesherVoxel;
    public GameObject chunkPrefab;
    List<GameObject> renderedChunks;
    ChunkSettings chunkSettings;
    void Start()
    {
        chunkSettings = new ChunkSettings(16, 1);
        renderedChunks = new List<GameObject>();
        world = new World<Voxel>(chunkSettings, new Vector3Int(256, 64, 256));
        chunkMesherVoxel = new VoxelChunkMesher(world.chunkSettings);

        RenderTest();
    }
    public void SetVoxel(Vector3 position, byte material)
    {
        position += world.chunkSettings.VoxelSize * 0.5f * Vector3.one; // center of voxel
        Voxel voxel = new Voxel();
        voxel.Density = 255;
        voxel.Material = material;

        world.SetVoxel(position, voxel);

        RenderTest();
    }
    void RenderTest()
    {
        //ChunkLayer surfaceVoxelLayer = new ChunkLayer(world.mainLayer.ChunkLayerSLs, world.mainLayer.ChunkWorldLength, world.chunkSettings);
        ChunkSurfaceExtractor chunkSurfaceExtractor = new ChunkSurfaceExtractor(world);
        chunkSurfaceExtractor.CalculateLayer(world.mainLayer);

        //delete previous
        GameObject[] toDelete = renderedChunks.ToArray();
        renderedChunks.Clear();
        for(int i = 0; i < toDelete.Length; i++) 
        {
            Destroy(toDelete[i]);
        }

        for (int x = 0; x < world.mainLayer.ChunkLayerSLs.x; x++)
        {
            for (int y = 0; y < world.mainLayer.ChunkLayerSLs.y; y++)
            {
                for (int z = 0; z < world.mainLayer.ChunkLayerSLs.z; z++)
                {
                    RenderChunk(new Vector3Int(x, y, z), world.mainLayer);
                }
            }
        }
    }
    void RenderChunk(Vector3Int index, ChunkLayer<Voxel> layer)
    {
        float chunkWorldLen = world.chunkSettings.ChunkWorldLen;
        // render 3 chunks
        
        Chunk<Voxel> chunk = layer.GetChunk(index);
        GameObject chunkGO = RenderChunk2(index, layer);

        Vector3 chunkCenterPos = (Vector3)index * chunkWorldLen;
        chunkGO.transform.position = chunkCenterPos;

    }

    GameObject RenderChunk2(Vector3Int index, ChunkLayer<Voxel> layer)
    {
        Mesh chunkMesh = chunkMesherVoxel.MeshChunk(index, layer);
        GameObject chunkGO = Instantiate(chunkPrefab);
        chunkGO.transform.localScale *= world.chunkSettings.VoxelSize;
        chunkGO.GetComponent<MeshFilter>().mesh = chunkMesh;
        renderedChunks.Add(chunkGO);
        return chunkGO;
    }
    /*
    void SetPositionOfChunk(GameObject chunkGO, Vector3 worldPosition)
    {
        float chunkLen = world.chunkSettings.ChunkWorldLen;
        chunkGO.transform.position = worldPosition - Vector3.one * (chunkLen * 0.5f);
    }
    */
}
