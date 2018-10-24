using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace Splosions
{
    public class SplosionData
    {
        public float3 OriginPosition;
        public float3 OriginEulerAngles;

        public float3 SpeedsMin;
        public float3 SpeedsMax;

        public bool Rounded;

        public List<Color> ColorProbabilities = new List<Color>();
        public List<Mesh> MeshProbabilities = new List<Mesh>();
    }

    public class SplosionDataWithScale : SplosionData
    {
        public float3 ScalesMin;
        public float3 ScalesMax;

        public float3 ScaleSpeedsMin;
        public float3 ScaleSpeedsMax;
    }

    public class SplosionDataWithRotation : SplosionData
    {
        public float3 RotationsMin;
        public float3 RotationsMax;

        public float3 RotationSpeedsMin;
        public float3 RotationSpeedsMax;
    }

    public class SplosionDataWithRotationAndScale : SplosionDataWithRotation // god damn it c# why u no multiple inheritance
    {
        public float3 ScalesMin;
        public float3 ScalesMax;

        public float3 ScaleSpeedsMin;
        public float3 ScaleSpeedsMax;
    }
}