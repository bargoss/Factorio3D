using System;
using System.Collections.Generic;
using UnityEngine;

public class TechnicalBlock : IItemInput, IItemOutput
{
    protected bool updatesNeighbour; // pass in neighbours if true
    protected bool rendersItems;
    protected byte lookDirection = 255;

    public bool UpdatesNeighbour { get => updatesNeighbour; }
    public byte TargetNeighbour { get => lookDirection; }
    public bool RendersItems { get => rendersItems; }

    public TechnicalBlock()
    {
        updatesNeighbour = false;
        rendersItems = false;
    }

    public virtual void Update(float deltaTime)
    {

    }
    public virtual void UpdateNeighbour(float deltaTime, TechnicalBlock neighbour) // reuse neighbours array
    {

    }
    public virtual ItemMesh[] GetItemsMesh(Vector3 myPos)
    {
        return null;
    }

    public virtual void SetLookDirection(byte lookDirection)
    {
        this.lookDirection = lookDirection;
    }
    public virtual byte GetLookDirection()
    {
        return lookDirection;
    }

    public virtual bool CanTake(int itemID)
    {
        return false;
    }

    public virtual void Take(int item)
    {
        return;
    }

    public virtual int CanOutput()
    {
        return 0;
    }

    public virtual int Output()
    {
        return 0;
    }

    public struct ItemMesh
    {
        public Vector3 position;
        public int itemType;
    }
}

