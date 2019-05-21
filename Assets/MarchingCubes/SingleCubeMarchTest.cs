using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingCubesProject;

public class SingleCubeMarchTest : MonoBehaviour {
    Marching marching;
    // Use this for initialization

    private void Start()
    {
        Test1();
    }

    void Test0 () {
        Marching marching = new MarchingCubes();
        marching.Surface = 0.0f;
        int width = 32;
        int height = 32;
        int length = 32;


        float[] voxels = new float[width * height * length];


        //marching.Generate(voxels, width, height, length, verts, indices);
        //marching.March(0,0,0,)
    }
	
    void Test1()
    {
        Marching marching = new MarchingCubes();
        marching.Surface = 0.0f;
        int width = 3;
        int height = 3;
        int length = 3;
        float[] voxels = new float[width * height * length];
        for (int i = 0; i < voxels.Length; i++)
        {
            voxels[i] = 1;
        }
        List<Vector3> verts = new List<Vector3>();
        List<int> indices = new List<int>();
        marching.Generate(voxels, width, height, length, verts, indices);
        print("vert: " + verts.Count);
        print("indices: " + indices.Count);
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.RecalculateNormals();

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

    }
	// Update is called once per frame
	void Update () {
		
	}

    
}
