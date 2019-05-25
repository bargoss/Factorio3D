using UnityEngine;
using System.Collections;

public class GoConnection : TechnicalBlock
{
    public GameObject gameObject; // a two way connection
    public RestrictedStorage restrictedStorage;

    public GoConnection(Vector3 position, Quaternion rotation, TechnicalGoInfo technicalGoInfo) : base(0b010101)
    {
        restrictedStorage = new RestrictedStorage(technicalGoInfo.canTakeIDs, technicalGoInfo.maxStorages);
        InitializeGameobject(position, rotation, technicalGoInfo.prefab);
    }

    public void InitializeGameobject(Vector3 position, Quaternion rotation, GameObject prefab)
    {
        gameObject = GameObject.Instantiate(prefab, position, rotation);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        GameObject.Destroy(gameObject);
    }

    public override int CanOutput()
    {
        return restrictedStorage.CanOutput();
    }

    public override bool CanTake(int itemID)
    {
        return restrictedStorage.CanTake(itemID);
    }

    public override int Output()
    {
        return restrictedStorage.Output();
    }

    public override void Take(int itemID)
    {
        restrictedStorage.Take(itemID);
    }
}
