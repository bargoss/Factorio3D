using UnityEngine;
using System.Collections;

public class ChunkGenerator : MonoBehaviour
{
    ChunkSettings chunkSettings;

    public ChunkGenerator(ChunkSettings chunkSettings)
    {
        this.chunkSettings = chunkSettings;
    }

    public void GenerateChunk(Chunk<Voxel> chunk, Vector3 worldPos)
    {
        Color color = Random.ColorHSV();
        
        Vector3 voxelPositionStart = worldPos;
        int chunkSL = chunkSettings.ChunkSL;
        float voxelSize = chunkSettings.VoxelSize;



        for (int z = 0; z < chunkSL; z++)
        {
            for (int y = 0; y < chunkSL; y++)
            {
                for (int x = 0; x < chunkSL; x++)
                {
                    Vector3Int index3D = new Vector3Int(x, y, z);
                    Vector3 voxelPosition = voxelPositionStart + (Vector3)index3D * voxelSize;

                    Voxel voxel = new Voxel();
                    byte voxDensity = GetDensity(voxelPosition);
                    voxel.Color = 0b00100101;
                    voxel.Density = voxDensity;
                    if (voxDensity == 0) { voxel.IsAir = true; }
                    else { voxel.IsAir = false; }



                    chunk.SetVoxel(index3D, voxel);
                }
            }
        }
    }
    void DisplayDot(Vector3 pos, Color color)
    {
        Debug.DrawRay(pos, Random.insideUnitSphere * 0.2f, color, 1000);
    }

    byte GetDensity(Vector3 input)
    {
        float noise = GetNoise(input);
        if (noise > 0.5f)
        {
            //return 255; 
            return (byte)(Mathf.Clamp((noise - 0.5f) * 5, 0f, 1) * 255);
        }
        else { return 0; }
    }
    float GetNoise(Vector3 input)
    {
        //return 0;
        Vector3 noiseInput = input * 0.16f * 0.2f;
        float noise = Noise.Noise.GetNoise(noiseInput.x, noiseInput.y, noiseInput.z);
        return noise;

    }
}
