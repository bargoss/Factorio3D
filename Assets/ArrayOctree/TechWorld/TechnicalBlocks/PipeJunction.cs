using System;
using System.Collections.Generic;
using UnityEngine;

public class PipeJunction : TechnicalBlock
{
    public PipeJunction(Quaternion rotation) : base(rotation)
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours)
    {
        base.UpdateNeighbour(deltaTime, neighbours);
    }

    public override int CanOutput(Vector3Int exitDirection)
    {
        return base.CanOutput(exitDirection);
    }

    public override bool CanTake(int itemID, Vector3Int entryDirection)
    {
        return base.CanTake(itemID, entryDirection);
    }

    public override ItemMesh[] GetItemsMesh()
    {
        return base.GetItemsMesh();
    }

    public override int Output(Vector3Int exitDirection)
    {
        return base.Output(exitDirection);
    }

    public override void Take(int item, Vector3Int entryDirection)
    {
        base.Take(item, entryDirection);
    }

}

