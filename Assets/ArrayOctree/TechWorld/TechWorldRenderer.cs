using System;
using System.Collections.Generic;
using UnityEngine;

public class TechWorldRenderer
{
    public Dictionary<Vector3Int, InstancedMeshInfo> blockMeshInfos;
    public Dictionary<Vector3Int, InstancedMeshInfo> itemMeshInfos;

    Mesh[] blockMeshes;
    Material[] blockMaterials;

    Mesh[] itemMeshes;
    Material[] itemMaterials;

    public TechWorldRenderer(ItemsContainer itemsContainer)
    {
        blockMeshInfos = new Dictionary<Vector3Int, InstancedMeshInfo>();
        itemMeshInfos = new Dictionary<Vector3Int, InstancedMeshInfo>();
        int length = itemsContainer.items.Length;
        blockMeshes = new Mesh[length];
        blockMaterials = new Material[length];
        itemMeshes = new Mesh[length];
        itemMaterials = new Material[length];

        for (int i = 0; i < itemsContainer.items.Length; i++)
        {
            if (itemsContainer.items[i] != null && i != 0)
            {
                blockMeshes[i] = itemsContainer.models[itemsContainer.items[i].blockModel].mesh;
                blockMaterials[i] = itemsContainer.models[itemsContainer.items[i].blockModel].material;

                itemMeshes[i] = itemsContainer.models[itemsContainer.items[i].itemModel].mesh;
                itemMaterials[i] = itemsContainer.models[itemsContainer.items[i].itemModel].material;
            }
        }
    }
    public void Draw()
    {
        foreach(InstancedMeshInfo instancedMeshInfo in blockMeshInfos.Values)
        {
            DrawBlocksMeshInfo(instancedMeshInfo);
        }
        foreach (InstancedMeshInfo instancedMeshInfo in itemMeshInfos.Values)
        {
            DrawItemsMeshInfo(instancedMeshInfo);
        }
    }
    void DrawBlocksMeshInfo(InstancedMeshInfo instancedMeshInfo) // chunk
    {
        for (int type = 0; type < instancedMeshInfo.types.Length; type++)
        {
            InstancedMeshInfoType instancedMeshInfoType = instancedMeshInfo.types[type];
            DrawInstancedMeshInfoType(instancedMeshInfoType, blockMeshes[type], blockMaterials[type]);
        }
    }
    void DrawItemsMeshInfo(InstancedMeshInfo instancedMeshInfo) // chunk
    {
        for (int type = 0; type < instancedMeshInfo.types.Length; type++)
        {
            InstancedMeshInfoType instancedMeshInfoType = instancedMeshInfo.types[type];
            DrawInstancedMeshInfoType(instancedMeshInfoType, itemMeshes[type], itemMaterials[type]);
        }
    }
    void DrawInstancedMeshInfoType(InstancedMeshInfoType instancedMeshInfoType, Mesh mesh, Material material) // one type of block in chunk
    {
        if (mesh == null || material == null) return;
        Graphics.DrawMeshInstanced(mesh, 0, material, instancedMeshInfoType.transforms);
    }
}

