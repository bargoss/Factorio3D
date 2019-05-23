using System;
using System.Collections.Generic;
using UnityEngine;

public class TechnicalBlock : IItemInput, IItemOutput
{
    protected bool updatesNeighbours; // pass in neighbours if true
    protected bool rendersItems;
    protected byte lookDirection = 255;
    public byte[] requestedNeighbours;

    public bool UpdatesNeighbour { get => updatesNeighbours; }
    public byte LookDirection { get => lookDirection; }
    public bool RendersItems { get => rendersItems; }


    public TechnicalBlock(byte lookDirection)
    {
        updatesNeighbours = false;
        rendersItems = false;
        this.lookDirection = lookDirection;
    }

    public virtual void Update(float deltaTime)
    {

    }
    public virtual void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours) // reuse neighbours array
    {

    }
    public virtual ItemMesh[] GetItemsMesh(Vector3 myPos)
    {
        return null;
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

