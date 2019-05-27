using UnityEngine;
using System.Collections;

public class GoConnection : TechnicalBlock
{
    public GameObject gameObject; // a two way connection
    public RestrictedStorage restrictedStorage;

    public GoConnection(Vector3 worldPosition, Quaternion rotation, TechnicalGoInfo technicalGoInfo) : base(Quaternion.identity)
    {
        restrictedStorage = new RestrictedStorage(technicalGoInfo.canTakeIDs, technicalGoInfo.maxStorages);
        Matrix4x4 worldTransform = Matrix4x4.TRS(worldPosition, rotation, Vector3.one);
        InitializeGameobject(worldTransform, technicalGoInfo.prefab);
    }

    public void InitializeGameobject(Matrix4x4 worldTransform, GameObject prefab)
    {
        gameObject = GameObject.Instantiate(prefab, worldTransform.GetColumn(3), worldTransform.rotation);
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