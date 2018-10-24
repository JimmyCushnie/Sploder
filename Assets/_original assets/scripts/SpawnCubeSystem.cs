using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using CommandTerminal;

using Random = UnityEngine.Random;

namespace Splosions
{
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
            Instance = this;
            Enabled = false; // only run once. This is like Start() from monobehaviors.
        }

        static SpawnCubeSystem Instance;

        [RegisterCommand()]
        static void SplodeCommand(CommandArg[] args)
        {
            Instance?.Splode(args);
        }

        void Splode(CommandArg[] args)
        {
            UpdateInjectedComponentGroups();

            var spawner = m_Group.Spawner[0];

            var spawnedCubes = new NativeArray<Entity>(spawner.count, Allocator.Temp);
            EntityManager.Instantiate(spawner.prefab, spawnedCubes);

            foreach (var cubeEntity in spawnedCubes)
            {
                MoveCube m = new MoveCube()
                {
                    speed = 4,
                    direction = new float3() { x = Random.Range(-5f, 5f), y = Random.Range(-5f, 5f), z = Random.Range(-5f, 5f) }
                };
                EntityManager.SetComponentData(cubeEntity, m);
            }


            spawnedCubes.Dispose();
        }
    }
}