using System;
using System.Collections.Generic;
using UnityEngine;

public interface IItemInput
{
    bool CanTake(int itemID);
    void Take(int item);
}
public interface IItemOutput
{
    int CanOutput();
    int Output();
}