using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Splosions
{
    [Serializable]
    public struct MoveCube : IComponentData
    {
        public float speed;
        public float3 direction;
    }

    public class MoveCubeComponent : ComponentDataWrapper<MoveCube> { }
}