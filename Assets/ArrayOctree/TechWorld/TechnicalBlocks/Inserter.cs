using UnityEngine;
using System.Collections;

public class Inserter : Conveyor
{
    public Inserter(Quaternion rotation) : base(rotation)
    {
        requestedNeighbours = new Vector3Int[2];
        Vector3Int forwardDir = ForwardDirection;
        requestedNeighbours[0] = forwardDir;
        Vector3Int backDir = forwardDir.Multiply(-1);
        requestedNeighbours[1] = backDir;
    }
    
    public override void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours)
    {
        TryTransfer(neighbours[1], this);
        base.UpdateNeighbour(deltaTime, neighbours);
    }
}
/*
public class A
{
    public int myValue;

    public A(int a)
    {
        myValue = 1;
    }
}
public class B : A
{
    public B(int a) : base(a)
    {
        //myValue = 2;
    }
}
*/
