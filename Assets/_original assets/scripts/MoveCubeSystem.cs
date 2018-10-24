using System;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using UnityEngine;
using CommandTerminal;

public class MoveCubeSystem : ComponentSystem
{
    struct Group
    {
        [ReadOnly]

        public ComponentDataArray<MoveCube> Cube;
        public ComponentDataArray<Position> Position;

        public EntityArray Entity;
        public readonly int Length;
    }

    [Inject] Group m_Group;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < m_Group.Length; i++)
        {
            var cube = m_Group.Cube[i];
            var position = m_Group.Position[i];
            var entity = m_Group.Entity[i];

            position.Value += cube.direction * cube.speed * dt;

            EntityManager.SetComponentData(entity, position);
        }
    }

    static MoveCubeSystem Instance;
    protected override void OnCreateManager()
    {
        Instance = this;
    }

    [RegisterCommand()]
    static void ResetCommand(CommandArg[] args)
    {
        Instance.UpdateInjectedComponentGroups();

        var entities = Instance.m_Group.Entity.ToArray();

        for (int i = 0; i < entities.Length; i++)
            Instance.EntityManager.DestroyEntity(entities[i]);
    }

    [RegisterCommand()]
    static void Pause(CommandArg[] args)
    {
        Instance.Enabled = !Instance.Enabled;
    }
}
