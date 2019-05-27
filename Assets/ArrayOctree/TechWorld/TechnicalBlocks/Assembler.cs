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

    ItemMesh[] gearsMesh;

    public Assembler() : base(0b010101, 0b011001)
    {
        inputIDs = new int[4];
        inputAmounts = new int[4];

        rendersItems = true;
        gearsMesh = new ItemMesh[1];
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
            if (GotEnoughMaterials())
            {
                sinceLastCraft += deltaTime;
                Animate(deltaTime);
                TryFabricate();
            }
        }
    }
    public override ItemMesh[] GetItemsMesh()
    {
        ItemMesh[] itemMesh = new ItemMesh[1];
        itemMesh[0].itemType = gearsMesh[0].itemType;
        itemMesh[0].transform = gearsMesh[0].transform;
        itemMesh[0].transform = Matrix4x4.Rotate(Quaternion.Euler(0, gearRotation *220, 0)) * itemMesh[0].transform;
        return itemMesh;
    }
    void InitializeAnimatedParts()
    {
        gearsMesh = new ItemMesh[1];
        gearsMesh[0].itemType = 3;
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



    public override bool CanTake(int itemID)
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
    public override void Take(int itemID)
    {
        for (int i = 0; i < 4; i++)
        {
            if (inputIDs[i] == itemID)
            {
                inputAmounts[i] += 1;
            }
        }
    }
    public override int CanOutput()
    {
        if(outputAmount != 0)
        {
            return outputID;
        }
        return 0;
    }
    public override int Output()
    {
        outputAmount -= 1;
        return outputID;
    }
}
