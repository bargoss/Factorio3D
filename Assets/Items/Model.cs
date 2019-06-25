using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Model", menuName = "Model")]
public class Model : ScriptableObject
{
    public Mesh mesh;
    public Material material;
}
