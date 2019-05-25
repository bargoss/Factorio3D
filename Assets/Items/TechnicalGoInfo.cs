using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TechnicalGoInfo", menuName = "TechnicalGoInfo")]
public class TechnicalGoInfo : ScriptableObject
{
    public GameObject prefab;
    public int[] canTakeIDs;
    public int[] maxStorages;
}
