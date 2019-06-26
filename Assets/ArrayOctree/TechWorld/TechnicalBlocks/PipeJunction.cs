using System;
using System.Collections.Generic;
using UnityEngine;

public class PipeJunction : TechnicalBlock
{
    Queue<int> storage;
    int neighbourPriorityIndex; // neighbours will be traversed starting from this index
    static readonly Vector3Int[] allNeighbours =
    {
        new Vector3Int(-1,0,0),
        new Vector3Int(1,0,0),

        new Vector3Int(0,-1,0),
        new Vector3Int(0,1,0),

        new Vector3Int(0,0,-1),
        new Vector3Int(0,0,1)
    };
    public PipeJunction(Quaternion rotation) : base(rotation)
    {
        storage = new Queue<int>(10);
        requestedNeighbours = allNeighbours;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours)
    {
        base.UpdateNeighbour(deltaTime, neighbours);
        OutputToNeighbourPipes(neighbours);
    }

    public override ItemMesh[] GetStaticMesh()
    {
        ItemMesh[] itemMesh = new ItemMesh[1];
        itemMesh[0].modelType = 8;
        itemMesh[0].transform = Matrix4x4.identity;
        return itemMesh;
    }

    public override ItemMesh[] GetDynamicMesh()
    {
        return base.GetDynamicMesh();
    }

    public override int CanOutput(Vector3Int exitDirection)
    {
        if(storage.Count > 0)
        {
            return storage.Peek();
        }
        else
        {
            return 0;
        }
    }

    public override bool CanTake(int itemID, Vector3Int entryDirection)
    {
        if(itemID != 0 && storage.Count < 10)
        {
            return true;
        }
        else { return false; }
    }

    public override int Output(Vector3Int exitDirection)
    {
        if (storage.Count > 0)
        {
            return storage.Dequeue();
        }
        else
        {
            return 0;
        }
    }

    public override void Take(int item, Vector3Int entryDirection)
    {
        if (item != 0)
        {
            storage.Enqueue(item);
        }
    }

    void OutputToNeighbourPipes(TechnicalBlock[] neighbours)
    {
        if (storage.Count == 0) return;

        int nextNeighbourPriorityIndex = 0;
        for(int i = 0; i < 6; i++)
        {
            int neighbourIndex = (neighbourPriorityIndex + i) % 6;
            TechnicalBlock technicalBlock = neighbours[neighbourIndex];
            if(technicalBlock is Pipe)
            {
                Pipe pipe = (Pipe)technicalBlock;
                Vector3Int direction = requestedNeighbours[neighbourIndex];
                if (CanOutput(direction) != 0 && pipe.CanTake(CanOutput(direction), direction))
                {
                    pipe.Take(Output(direction), direction);
                    nextNeighbourPriorityIndex = (neighbourIndex + 1) % 6;
                }
            }
        }
        neighbourPriorityIndex = nextNeighbourPriorityIndex;
    }
}

