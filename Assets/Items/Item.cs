using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;

    public int itemModel;

    public int blockModel;

    public TechBlockType techBlockType;

    public enum TechBlockType
    {
        NonTechnical,
        Pipe,
        Assembler,
        PipeOutput,
        Splitter,
        PipeJunction
    };
}
