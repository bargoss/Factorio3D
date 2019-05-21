using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
public struct Block
{
    public byte blockType; // 0 is air
    public TechnicalBlock technicalBlock; // null if block type is Non-technical
}
