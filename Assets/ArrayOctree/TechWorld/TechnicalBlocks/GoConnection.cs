using UnityEngine;
using System.Collections;

public class GoConnection : TechnicalBlock
{
    public GameObject gameObject; // a two way connection
    public RestrictedStorage restrictedStorage;

    public GoConnection(Matrix4x4 transform, TechnicalGoInfo technicalGoInfo) : base(Matrix4x4.identity)
    {
        restrictedStorage = new RestrictedStorage(technicalGoInfo.canTakeIDs, technicalGoInfo.maxStorages);
        InitializeGameobject(transform, technicalGoInfo.prefab);
    }

    public void InitializeGameobject(Matrix4x4 transform, GameObject prefab)
    {
        gameObject = GameObject.Instantiate(prefab, transform.GetColumn(3), transform.rotation);
        gameObject.GetComponent<TechnicalGo>().Initialize(this);
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