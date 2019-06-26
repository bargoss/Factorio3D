using System;
using System.Collections.Generic;
using UnityEngine;

public class TechnicalBlock : IItemInput, IItemOutput
{
    protected Matrix4x4 localTransform; // position is always 0,0,0
    public Vector3Int[] requestedNeighbours;

    public Vector3Int RightDirection { get { return ((Vector3)localTransform.GetColumn(0)).ToVector3Int(); } }
    public Vector3Int UpDirection { get { return ((Vector3)localTransform.GetColumn(1)).ToVector3Int(); } }
    public Vector3Int ForwardDirection { get { return ((Vector3)localTransform.GetColumn(2)).ToVector3Int(); } }


    public TechnicalBlock(Quaternion rotation)
    {
        requestedNeighbours = new Vector3Int[0];
        this.localTransform = Matrix4x4.Rotate(rotation);
    }

    public virtual void Update(float deltaTime)
    {

    }
    public virtual void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours) // reuse neighbours array
    {

    }

    public virtual ItemMesh[] GetStaticMesh()
    {
        return null;
    }

    public virtual ItemMesh[] GetDynamicMesh()
    {
        return new ItemMesh[0];
    }
    
    public virtual bool CanTake(int itemID, Vector3Int entryDirection)
    {
        return false;
    }

    public virtual void Take(int item, Vector3Int entryDirection)
    {
        return;
    }

    public virtual int CanOutput(Vector3Int exitDirection)
    {
        return 0;
    }

    public virtual int Output(Vector3Int exitDirection)
    {
        return 0;
    }

    public struct ItemMesh
    {
        public Matrix4x4 transform; 
        public int itemType;
    }

    
    public static void TryTransfer(TechnicalBlock source, TechnicalBlock destination, Vector3Int entryDirection)
    {
        if (source != null && destination != null)
        {
            int output = source.CanOutput(entryDirection);
            if (output != 0)
            {
                if (destination != null && destination.CanTake(output, entryDirection))
                {
                    source.Output(entryDirection);
                    destination.Take(output, entryDirection);
                }
            }
        }
    }

    public virtual void OnDestroy() // call this before destroying the TechnicalBlock
    {

    }
}

