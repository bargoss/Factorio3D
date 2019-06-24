using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedStorage : IItemInput, IItemOutput
{
    public struct ItemStack
    {
        public int itemID;
        public int amount;
    }

    public int[] canTakeIDs;
    public int[] maxStorages;

    ItemStack[] itemStacks;

    public RestrictedStorage(int[] canTakeIDs, int[] maxStorages)
    {
        this.canTakeIDs = canTakeIDs;
        this.maxStorages = maxStorages;

        InitializeStacks();
    }
    void InitializeStacks()
    {
        itemStacks = new ItemStack[canTakeIDs.Length];
        for (int i = 0; i < itemStacks.Length; i++)
        {
            itemStacks[i].itemID = canTakeIDs[i];
        }
    }

    public int CanOutput(Vector3Int exitDirection)
    {
        int stackToOutput = FindStackForOutput();
        if (stackToOutput != -1)
        {
            return itemStacks[stackToOutput].itemID;
        }
        return 0;
    }

    public bool CanTake(int itemID, Vector3Int entryDirection)
    {
        int stackToInsert = FindStackForInput(itemID);
        if(stackToInsert != -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Output(Vector3Int exitDirection)
    {
        int stackToOutput = FindStackForOutput();
        if(stackToOutput != -1)
        {
            itemStacks[stackToOutput].amount--;
            return itemStacks[stackToOutput].itemID;
        }
        return 0;
    }

    public void Take(int itemID, Vector3Int entryDirection)
    {
        int stackToInsert = FindStackForInput(itemID);
        if (stackToInsert != -1)
        {
            itemStacks[stackToInsert].amount += 1;
        }
    }

    bool CorrectInputID(int inputID)
    {
        for(int i = 0; i < canTakeIDs.Length; i++)
        {
            if (inputID == canTakeIDs[i])
            {
                return true;
            }
        }
        return false;
    }
    int FindStackForInput(int inputID)
    {
        for(int i = 0; i < itemStacks.Length; i++)
        {
            if(itemStacks[i].itemID == inputID && itemStacks[i].amount < maxStorages[i])
            {
                return i;
            }
        }
        return -1;
    }
    int FindStackForOutput()
    {
        for(int i = 0; i < itemStacks.Length; i++)
        {
            if(itemStacks[i].amount > 0)
            {
                return i;
            }
        }
        return -1;
    }
    
}