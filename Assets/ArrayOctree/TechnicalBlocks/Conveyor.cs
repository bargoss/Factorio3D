using System;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : TechnicalBlock
{
    public int[] sectionContains;
    public float[] sectionMovement;

    float transferSpeed = 1;
    float sectionInterval;

    ItemMesh[] itemsMesh;

    

    public Conveyor() : base()
    {
        sectionContains = new int[4];
        sectionMovement = new float[4];
        sectionInterval = 1.0f / sectionMovement.Length;

        updatesNeighbour = true;
        rendersItems = true;

        itemsMesh = new ItemMesh[4];
    }
    

    // on every update also handle transfering items
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        TransferUpdate(deltaTime);
    }
    public override ItemMesh[] GetItemsMesh(Vector3 myPos)
    {
        UpdateItemsMesh(myPos);
        return itemsMesh;
    }
    void UpdateItemsMesh(Vector3 myPos)
    {
        Vector3 lookDirectionVec = (Vector3)(lookDirection.ToVector3Int());
        Vector3 sectionStart = -lookDirectionVec * 0.5f;
        for (int i = 0; i < 4; i++)
        {
            if (sectionContains[i] != 0)
            {
                itemsMesh[i].itemType = sectionContains[i];
                itemsMesh[i].position = sectionStart + (i*sectionInterval + sectionMovement[i]) * lookDirectionVec;
            }
            else { itemsMesh[i].itemType = 0; }
        }
    }
    public override void UpdateNeighbour(float deltaTime, TechnicalBlock neighbour)
    {
        TechnicalBlock target = neighbour;
        TryTransferingOutputToNeighbour(target);
    }
    void TryTransferingOutputToNeighbour(TechnicalBlock target)
    {
        int output = CanOutput();
        if (output != 0)
        {
            if (target.CanTake(output))
            {
                Output();
                target.Take(output);
            }
        }
    }

    void TransferUpdate(float deltaTime)
    {
        float transfer = deltaTime * transferSpeed;
        MoveSection(3, transfer); // last section
        for (int i = 2; i >= 0; i--)
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
    void TryTransferSection(int section) // call on section 0,1,2 not 3
    {
        // also check next section
        if (SectionIsEmpty(section) == false && SectionIsEmpty(section + 1) == true)
        {
            float surplus = 0;
            if(SectionTransferTimeCompleted(section, out surplus))
            {
                TransferSection(section, surplus);
            }
        }
    }
    void TransferSection(int section, float surplus) // not last section, not empty section
    {
        int contain = sectionContains[section];
        sectionContains[section + 1] = contain;

        sectionContains[section] = 0;
        sectionMovement[section] = 0; // use % ?
        sectionMovement[section + 1] = surplus;
    }
    bool IsLastSection(int section)
    {
        return section == 3;
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

    public override bool CanTake(int itemID)
    {
        if(sectionContains[0] == 0)
        {
            return true;
        }
        else { return false; }
    }
    public override void Take(int item)
    {
        sectionContains[0] = item;
    }
    public override int CanOutput()
    {
        if (sectionMovement[3] >= sectionInterval)
        {
            return sectionContains[3];
        }
        else
        {
            return 0;
        }
    }
    public override int Output()
    {
        int toReturn = sectionContains[3];
        sectionContains[3] = 0;
        sectionMovement[3] = 0;
        return toReturn;
    }

    /*
    public bool IsFull()
    {
        for(int i = 0; i < 4; i++)
        {
            if(itemTypes[i] == 0) 
        }
    }
    */
}

/*
Vector3Int DirectionToVec(byte direction)
    {
        Vector3Int result = Vector3Int.zero;
        if((direction & 0b00000100) == 0b00000100) // z+
        {
            result += new Vector3Int(0, 0, 1);
        }
        else // z-
        {
            result += new Vector3Int(0, 0, -1);
        }

        if ((direction & 0b00000010) == 0b00000010) // z+
        {
            result += new Vector3Int(0, 1, 0);
        }
        else // z-
        {
            result += new Vector3Int(0, -1, 0);
        }

        if ((direction & 0b00000001) == 0b00000001) // z+
        {
            result += new Vector3Int(1, 0, 0);
        }
        else // z-
        {
            result += new Vector3Int(-1, 0, 0);
        }

        return result;
    }

*/
