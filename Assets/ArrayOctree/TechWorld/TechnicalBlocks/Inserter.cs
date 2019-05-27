using UnityEngine;
using System.Collections;

public class Inserter : Conveyor
{
    public Inserter(byte lookDirection) : base(lookDirection, 0b011001)
    {
        requestedNeighbours = new byte[2];
        requestedNeighbours[0] = lookDirection;
        Vector3Int lookVector = lookDirection.ToVector3Int();
        Vector3Int invertedLookVector = lookVector.Multiply(-1);
        requestedNeighbours[1] = invertedLookVector.ToDirection();
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
