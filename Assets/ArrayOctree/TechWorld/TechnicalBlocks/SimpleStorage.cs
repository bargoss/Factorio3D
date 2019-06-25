using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStorage : IItemInput, IItemOutput
{
    List<int> storage;
    int selector;

    public SimpleStorage(int size)
    {
        storage = new List<int>(size);
    }

    public int CanOutput(Vector3Int exitDirection)
    {
        return storage[selector];
    }

    public bool CanTake(int itemID, Vector3Int entryDirection)
    {
        throw new NotImplementedException();
    }

    public int Output(Vector3Int exitDirection)
    {
        throw new NotImplementedException();
    }

    public void Take(int item, Vector3Int entryDirection)
    {
        throw new NotImplementedException();
    }
}

