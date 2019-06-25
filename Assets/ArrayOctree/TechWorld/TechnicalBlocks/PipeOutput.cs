using UnityEngine;
using System.Collections;

public class PipeOutput : Pipe
{
    public PipeOutput(Quaternion rotation) : base(rotation, 7)
    {
        requestedNeighbours = new Vector3Int[2];
        Vector3Int forwardDir = ForwardDirection;
        requestedNeighbours[0] = forwardDir;
        Vector3Int backDir = forwardDir.Multiply(-1);
        requestedNeighbours[1] = backDir;

        sectionStart = -localTransform.GetColumn(2) * (0.75f);
    }
    
    public override void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours)
    {
        TryTransfer(neighbours[1], this, ForwardDirection);
        base.UpdateNeighbour(deltaTime, neighbours);
    }
}