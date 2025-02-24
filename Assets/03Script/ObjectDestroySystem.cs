using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[BurstCompile]
public partial struct ObjectDestroySystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var query = SystemAPI.QueryBuilder().WithAll<TestObjectTag>().Build();

            NativeArray<Entity> entities = query.ToEntityArray(Allocator.Temp);
            if (entities.Length > 0)
            {
                ecb.DestroyEntity(entities[0]); // 가장 먼저 생성된 엔티티 삭제
            }

            ecb.Playback(state.EntityManager);
            entities.Dispose();
        }
    }
}
