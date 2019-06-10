using System;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : TechnicalBlock
{
    public int[] sectionContains;
    public float[] sectionMovement;

    float transferSpeed = 1;
    float sectionInterval;
    int lastSection;
    protected int midSection = 0;
    protected Vector3 sectionStart;

    // true: main direction io prioritized, false: alternative direction is prioritized
    protected bool inputPriority = true;
    protected bool outputPriority = true;

    ItemMesh[] itemsMesh;



    public Conveyor(Quaternion rotation, int sectionCount = 4) : base(rotation)
    {
        sectionContains = new int[sectionCount];
        sectionMovement = new float[sectionCount];

        sectionInterval = 0.25f;

        updatesNeighbours = true;
        rendersItems = true;
        
        itemsMesh = new ItemMesh[sectionCount];

        requestedNeighbours = new Vector3Int[1];
        requestedNeighbours[0] = ForwardDirection;

        lastSection = sectionCount - 1;
        sectionStart = Vector3.zero;
    }
    


    // on every update also handle transfering items
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        TransferUpdate(deltaTime);
    }
    public override ItemMesh[] GetItemsMesh()
    {
        UpdateItemsMesh();
        return itemsMesh;
    }
    void UpdateItemsMesh()
    {
        Vector3 lookDirectionVec = (Vector3)(ForwardDirection);
        
        for (int i = 0; i < sectionContains.Length; i++)
        {
            if (sectionContains[i] != 0)
            {
                itemsMesh[i].itemType = sectionContains[i];
                Vector3 position = sectionStart + (i*sectionInterval + sectionMovement[i]) * lookDirectionVec;
                itemsMesh[i].transform = Matrix4x4.TRS(position, Quaternion.identity, 0.25f * Vector3.one);
            }
            else { itemsMesh[i].itemType = 0; }
        }
    }
    public override void UpdateNeighbour(float deltaTime, TechnicalBlock[] neighbours)
    {
        if (neighbours.Length > 0)
        {
            TechnicalBlock target = neighbours[0];
            TryTransfer(this, target, ForwardDirection);
        }
    }

    void TransferUpdate(float deltaTime)
    {
        float transfer = deltaTime * transferSpeed;
        MoveSection(lastSection, transfer); // last section
        for (int i = lastSection - 1; i >= 0; i--)
        {
            MoveSection(i, transfer);
            TryTransferSection(i);
        }
    }    
    void MoveSection(int section, float transfer)
    {
        if(SectionIsEmpty(section) == false)
        {
            sectionMovement[section] += transfer;
            if(sectionMovement[section] > sectionInterval)
            {
                sectionMovement[section] = sectionInterval + 0.001f;
            }
        }
    }
    void TryTransferSection(int section) // don't call on last section
    {
        if (SectionIsEmpty(section) == false && SectionIsEmpty(section + 1) == true)
        {
            float surplus = 0;
            if(SectionTransferTimeCompleted(section, out surplus))
            {
                TransferSection(section, surplus);
            }
        }
    }
    void TransferSection(int section, float surplus) // assuming not last section, not empty section
    {
        int contain = sectionContains[section];
        sectionContains[section + 1] = contain;

        sectionContains[section] = 0;
        sectionMovement[section] = 0; // use % ?
        sectionMovement[section + 1] = surplus;
    }
    bool IsLastSection(int section)
    {
        return section == lastSection;
    }
    bool SectionIsEmpty(int section)
    {
        return (sectionContains[section] == 0);
    }
    bool SectionTransferTimeCompleted(int section, out float surplus)
    {
        surplus = sectionMovement[section] - sectionInterval;
        return surplus > 0;
    }

    public override bool CanTake(int itemID, Vector3Int entryDirection)
    {
        if(sectionContains[midSection] == 0)
        {
            return true;
        }
        else { return false; }
    }
    public override void Take(int item, Vector3Int entryDirection)
    {
        inputPriority = !inputPriority;
        sectionContains[midSection] = item;
    }
    public override int CanOutput(Vector3Int exitDirection)
    {
        if(ForwardDirection == exitDirection) {
            return CanRegularOutput();
        }
        else
        {
            return CanMidOutput();
        }
    }
    public override int Output(Vector3Int exitDirection)
    {
        outputPriority = !outputPriority;
        if (ForwardDirection == exitDirection)
        {
            return RegularOutput();
        }
        else
        {
            return MidOutput();
        }
    }

    int CanRegularOutput()
    {
        if (sectionMovement[lastSection] >= sectionInterval)
        {
            return sectionContains[lastSection];
        }
        else
        {
            return 0;
        }
    }
    int RegularOutput()
    {
        int toReturn = sectionContains[lastSection];
        sectionContains[lastSection] = 0;
        sectionMovement[lastSection] = 0;
        return toReturn;
    }
    int CanMidOutput()
    {
        if (sectionMovement[midSection] >= sectionInterval)
        {
            return sectionContains[midSection];
        }
        else
        {
            return 0;
        }
    }
    int MidOutput()
    {
        int toReturn = sectionContains[midSection];
        sectionContains[midSection] = 0;
        sectionMovement[midSection] = 0;
        return toReturn;
    }
}