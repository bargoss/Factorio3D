using UnityEngine;
using System.Collections;
using System;

public class Tests : MonoBehaviour
{
    public Mesh testMesh;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    

    public int TestVoxel()
    {
        Voxel vox0 = new Voxel();
        vox0.IsAir = false;
        if (vox0.IsAir == true) return 2;

        vox0.IsSurface = true;
        if (vox0.IsSurface == false) return 3;
        vox0.Color = 0b10111111;
        if (vox0.Color != 0b00111111) return 5;
        vox0.IsAir = true;
        if (vox0.IsAir == false) return 1;
        vox0.IsSurface = false;
        if (vox0.IsSurface == true) return 4;


        return 0;
    }
    public void TestWorld()
    {
        //World<Voxel> world = new World<Voxel>(16, 1, new Vector3Int(256, 64, 256));
        //print("done");
    }
    public double CalculateWorldSize(double xLen, double yLen, double zLen, double voxSize)
    {
        double xSize = xLen / voxSize;
        double ySize = yLen / voxSize;
        double zSize = zLen / voxSize;

        double totalVoxels = (1.0 / 1024.0) * xSize * ySize * zSize; // to kb

        return totalVoxels * (1.0 / 1024.0) * 2; // to mb
    }
    public double CalculateWorldSize(double xDepth, double yDepth, double zDepth) 
    {
        
        
        double xSize = Math.Pow(2, xDepth);
        double ySize = Math.Pow(2, yDepth);
        double zSize = Math.Pow(2, zDepth);

        double totalVoxels = (1.0 / 1024.0) * xSize * ySize * zSize; // to kb

        return totalVoxels * (1.0/1024.0) * 2; // to mb
    }
}
