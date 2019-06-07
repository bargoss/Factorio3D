using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;

    public Mesh itemMesh;
    public Material itemMaterial;

    public Mesh blockMesh;
    public Material blockMaterial;

    public TechBlockType techBlockType;

    public enum TechBlockType
    {
        NonTechnical,
        Conveyor,
        Assembler,
        Inserter,
        Splitter
    };
}
