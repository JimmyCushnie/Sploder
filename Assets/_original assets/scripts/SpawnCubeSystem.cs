using System;
using Unity.Entities;
using UnityEngine;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

using Random = UnityEngine.Random;

public class SpawnCubeSystem : ComponentSystem
{
    struct Group
    {
        [ReadOnly]

        public SharedComponentDataArray<SpawnCube> Spawner;

        public EntityArray Entity;
        public readonly int Length;
    }

    [Inject] Group m_Group;

    protected override void OnUpdate()
    {
        while (m_Group.Length != 0)
        {
            var spawner = m_Group.Spawner[0];
            var sourceEntity = m_Group.Entity[0];

            var spawnedCubes = new NativeArray<Entity>(spawner.count, Allocator.Temp);
            EntityManager.Instantiate(spawner.prefab, spawnedCubes);

            foreach(var cubeEntity in spawnedCubes)
            {
                MoveCube m = new MoveCube()
                {
                    speed = 4,
                    direction = new float3() { x = Random.Range(-5f, 5f), y = Random.Range(-5f, 5f), z = Random.Range(-5f, 5f) }
                };
                EntityManager.SetComponentData(cubeEntity, m);
            }


            spawnedCubes.Dispose();

            EntityManager.RemoveComponent<SpawnCube>(sourceEntity);

            // Instantiate & AddComponent & RemoveComponent calls invalidate the injected groups,
            // so before we get to the next spawner we have to reinject them  
            UpdateInjectedComponentGroups();
        }
    }
}