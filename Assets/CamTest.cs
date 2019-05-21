using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTest : MonoBehaviour
{
    public WorldMono worldMono;
    public Transform crossair;
    

    // Update is called once per frame
    void Update()
    {
        crossair.transform.rotation = Quaternion.identity;

        if (Input.GetKeyDown(KeyCode.F))
        {
            worldMono.SetVoxel(crossair.transform.position, 0b00100101);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            worldMono.SetVoxel(crossair.transform.position, 0b01000000);
        }
    }
}
