using System;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : TechnicalBlock
{
    int recipeID;
    Recipe recipe;

    int[] inputIDs;
    int outputID;

    int[] inputAmounts;
    int outputAmount;

    float sinceLastCraft;
    float gearRotation;

    ModelInfo[] gearsMesh;

    public Assembler() : base(Quaternion.identity)
    {
        inputIDs = new int[4];
        inputAmounts = new int[4];

        gearsMesh = new ModelInfo[1];
        InitializeAnimatedParts();
    }

    public void SwitchRecipe(int recipeID, ItemsContainer items)
    {
        this.recipeID = recipeID;
        recipe = items.recipes[recipeID];
        for(int i = 0; i < 4; i++)
        {
            inputIDs[i] = recipe.inputIDs[i];
            inputAmounts[i] = 0;
        }
        outputAmount = 0;
        outputID = recipe.outputID;
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (recipeID != 0) // recipe is not null
        {
            if (GotEnoughMaterials() && GotRoomForOutput())
            {
                sinceLastCraft += deltaTime;
                Animate(deltaTime);
                TryFabricate();
            }
        }
    }
    public override ModelInfo[] GetStaticMesh()
    {
        ModelInfo[] itemMesh = new ModelInfo[1];
        itemMesh[0].modelType = 4;
        itemMesh[0].transform = Matrix4x4.Translate(Vector3.zero);
        return itemMesh;
    }
    
    public override ModelInfo[] GetDynamicMesh()
    {
        ModelInfo[] itemMesh = new ModelInfo[1];
        itemMesh[0].modelType = gearsMesh[0].modelType;
        itemMesh[0].transform = gearsMesh[0].transform;
        itemMesh[0].transform = Matrix4x4.Rotate(Quaternion.Euler(0, gearRotation *220, 0)) * itemMesh[0].transform;
        return itemMesh;
    }
    bool GotRoomForOutput()
    {
        return (outputAmount < recipe.outputAmount * 2);
    }
    void InitializeAnimatedParts()
    {
        gearsMesh = new ModelInfo[1];
        gearsMesh[0].modelType = 3;
        gearsMesh[0].transform = Matrix4x4.TRS(Vector3.up * 0.45f, Quaternion.LookRotation(Vector3.up), 0.45f * Vector3.one);
    }
    void Animate(float deltaTime)
    {
        gearRotation += deltaTime;
    }
    void Fabricate(float overflow)
    {
        sinceLastCraft = overflow;
        for (int i = 0; i < 4; i++)
        {
            inputAmounts[i] -= recipe.inputAmounts[i];
        }
        outputAmount += recipe.outputAmount;
    }
    void TryFabricate()
    {
        float overflow = 0;
        if(CooledDown(out overflow) && GotEnoughMaterials())
        {
            Fabricate(overflow);
        }
    }
    bool CooledDown(out float overflow)
    {
        if(sinceLastCraft > recipe.craftDuration)
        {
            overflow = sinceLastCraft - recipe.craftDuration;
            
            return true;
        }
        else
        {
            overflow = 0;
            return false;
        }
    }
    bool GotEnoughMaterials()
    {
        for(int i = 0; i< 4; i++)
        {
            if(inputAmounts[i] < recipe.inputAmounts[i]) { return false; }
        }
        return true;
    }



    public override bool CanTake(int itemID, Vector3Int entryDirection)
    {
        for(int i = 0; i < 4; i++)
        {
            if(inputIDs[i] == itemID && inputAmounts[i] < recipe.inputAmounts[i] * 2)
            {
                return true;
            }
        }
        return false;
    }
    public override void Take(int itemID, Vector3Int entryDirection)
    {
        for (int i = 0; i < 4; i++)
        {
            if (inputIDs[i] == itemID)
            {
                inputAmounts[i] += 1;
            }
        }
    }
    public override int CanOutput(Vector3Int exitDirection)
    {
        if(outputAmount != 0)
        {
            return outputID;
        }
        return 0;
    }
    public override int Output(Vector3Int exitDirection)
    {
        outputAmount -= 1;
        return outputID;
    }
}
