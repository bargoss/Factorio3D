using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Splitter : TechnicalBlock
{
    Vector3Int blockedInputLane;
    
    //Conveyor[] conveyors;

    Queue<int> mainLaneStorage;
    bool mainLaneLastOutputDirection = false;

    Queue<int> alternativeLaneStorage;
    bool alternativeLaneLastOutputDirection = false;

    public Splitter(Quaternion rotation) : base(rotation)
    {
        blockedInputLane = Vector3Int.zero;

        //rendersItems = true;
        updatesNeighbours = true;

        requestedNeighbours = new Vector3Int[2];

        requestedNeighbours[0] = ForwardDirection; // output 0
        requestedNeighbours[1] = RightDirection; // output 1

        //conveyors = new Conveyor[2];
        //conveyors[0] = new Conveyor(Quaternion.LookRotation(requestedNeighbours[0], UpDirection),2);
        //conveyors[1] = new Conveyor(Quaternion.LookRotation(requestedNeighbours[1], UpDirection),2);

        mainLaneStorage = new Queue<int>(2);
        alternativeLaneStorage = new Queue<int>(2);
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }
    public override void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours)
    {
        base.UpdateNeighbour(deltaTime, neighbours);

        UpdateQueues(neighbours);
    }

    void UpdateQueues(TechnicalBlock[] neighbours)
    {
        TryOutputingLane(mainLaneStorage, ref mainLaneLastOutputDirection, neighbours);
        TryOutputingLane(alternativeLaneStorage, ref alternativeLaneLastOutputDirection, neighbours);
    }

    // Call this 2 times. For he times where only one input and output direction is working. Otherwise it will work at %50 speed in that situation.
    void TryOutputingLane(Queue<int> lane, ref bool lastDirection, TechnicalBlock[] neighbours)
    {
        if (lane.Count > 0)
        {
            if (lastDirection)
            {
                if (neighbours[0].CanTake(lane.Peek(), ForwardDirection))
                {
                    neighbours[0].Take(lane.Dequeue(), ForwardDirection);
                }
            }
            else
            {
                if (neighbours[1].CanTake(lane.Peek(), RightDirection))
                {
                    neighbours[1].Take(lane.Dequeue(), RightDirection);
                }
            }
            lastDirection = !lastDirection;
        }
    }

    public override int CanOutput()
    {
        // do stuff here
        return 0;
    }
    public override int Output()
    {
        // do stuff here
        return 0;
    }
    public override bool CanTake(int itemID, Vector3Int entryDirection)
    {
        if (itemID == 0) return false;

        if(entryDirection == ForwardDirection)
        {
            if(mainLaneStorage.Count < 2) { return true; }
        }
        else
        {
            if (alternativeLaneStorage.Count < 2) { return true; }
        }

        return false;
    }
    public override void Take(int item, Vector3Int entryDirection)
    {
        if (entryDirection == ForwardDirection)
        {
            mainLaneStorage.Enqueue(item);
        }
        else
        {
            alternativeLaneStorage.Enqueue(item);
        }
    }
    /*
    */
    // lets make it 4
    // need two conveyors here
    /*
    public Splitter(Quaternion rotation) : base(rotation)
    {
        sectionContains = new int[6];
        sectionMovement = new float[6];

        requestedNeighbours = new Vector3Int[4];
        requestedNeighbours[0] = ForwardDirection; // output 0
        requestedNeighbours[1] = RightDirection; // output 1

        requestedNeighbours[2] = ForwardDirection; // input 0
        requestedNeighbours[3] = RightDirection; // input 1
    }
    */
}
