using System;
using System.Collections.Generic;


public class Fabricator : TechnicalBlock
{
    int recipeID;
    Recipe recipe;

    int[] inputIDs;
    int outputID;

    int[] inputAmounts;
    int outputAmount;

    float sinceLastCraft;


    public Fabricator(byte lookDirection) : base(lookDirection)
    {
        inputIDs = new int[4];
        inputAmounts = new int[4];
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
            sinceLastCraft += deltaTime; // craft progress
            TryFabricate();
        }
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
