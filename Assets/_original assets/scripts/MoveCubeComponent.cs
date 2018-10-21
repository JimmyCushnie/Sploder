using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct MoveCube : IComponentData
{
    public float speed;
}

public class MoveCubeComponent : ComponentDataWrapper<MoveCube> { }