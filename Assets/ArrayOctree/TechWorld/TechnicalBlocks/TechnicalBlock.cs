using System;
using System.Collections.Generic;
using UnityEngine;

public class TechnicalBlock : IItemInput, IItemOutput
{
    protected bool updatesNeighbours; // pass in neighbours if true
    protected bool rendersItems;
    protected Matrix4x4 transform;
    public Vector3Int[] requestedNeighbours;

    public bool UpdatesNeighbour { get => updatesNeighbours; }
    public Vector3Int ForwardDirection { get { return ((Vector3)transform.GetColumn(2)).ToVector3Int(); } }
    public Vector3Int UpDirection { get { return ((Vector3)transform.GetColumn(0)).ToVector3Int(); } }
    public bool RendersItems { get => rendersItems; }


    public TechnicalBlock(Matrix4x4 transform)
    {
        updatesNeighbours = false;
        rendersItems = false;
        this.transform = transform;
    }

    public virtual void Update(float deltaTime)
    {

    }
    public virtual void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours) // reuse neighbours array
    {

    }
    public virtual ItemMesh[] GetItemsMesh()
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
        public Matrix4x4 transform;
        public int itemType;
    }

    
    public static void TryTransfer(TechnicalBlock source, TechnicalBlock destination)
    {
        int output = source.CanOutput();
        if (output != 0)
        {
            if (destination != null && destination.CanTake(output))
            {
                source.Output();
                destination.Take(output);
            }
        }
    }

    public virtual void OnDestroy() // call this before destroying the TechnicalBlock
    {

    }
}

