using System;
using System.Collections.Generic;
using UnityEngine;

public interface IItemInput
{
    bool CanTake(int itemID, Vector3Int entryDirection);
    void Take(int item, Vector3Int entryDirection);
}
public interface IItemOutput
{
    int CanOutput(Vector3Int exitDirection);
    int Output(Vector3Int exitDirection);
}