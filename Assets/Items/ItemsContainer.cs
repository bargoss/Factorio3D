using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class ItemsContainer : ScriptableObject
{
    
    //identify all items as indices
    // since index == 0 means null, leave first element null

    public Item[] items;
    public Recipe[] recipes;
    public Model[] models;

    public Model GetItemModel(int itemType)
    {
        if (itemType == 0) return null;
        int modelType = items[itemType].itemModel;
        return models[modelType];
    }
    public Model GetBlockModel(int itemType)
    {
        if (itemType == 0) return null;
        int modelType = items[itemType].blockModel;
        return models[modelType];
    }
}
