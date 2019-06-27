using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
public struct Block
{
    public byte blockType; // 0 is air (doesn't matter if technicalBlock is not null)
    public TechnicalBlock technicalBlock; // null if block type is Non-technical
}
