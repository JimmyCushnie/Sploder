using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct SpawnCube : ISharedComponentData
{
    public GameObject prefab;
    public int count;
}

public class SpawnCubeComponent : SharedComponentDataWrapper<SpawnCube> { }