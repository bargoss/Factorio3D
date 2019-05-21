using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public int[] inputIDs;
    public int outputID;

    public int[] inputAmounts;
    public int outputAmount;

    public float craftDuration;
}
