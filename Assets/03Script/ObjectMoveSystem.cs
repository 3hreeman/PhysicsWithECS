using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct ObjectMoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float camHalfHeight = Camera.main.orthographicSize;

        foreach (var (transform, move, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MovableComponent>>().WithEntityAccess())
        {
            float3 position = transform.ValueRO.Position;

            // 이동
            position.xy += move.ValueRO.Direction * move.ValueRO.Speed * SystemAPI.Time.DeltaTime;

            // 화면 경계 검사 후 방향 반전
            if (math.abs(position.x) > camHalfWidth)
            {
                move.ValueRW.Direction.x *= -1;
                position.x = math.sign(position.x) * camHalfWidth;
            }
            if (math.abs(position.y) > camHalfHeight)
            {
                move.ValueRW.Direction.y *= -1;
                position.y = math.sign(position.y) * camHalfHeight;
            }

            transform.ValueRW.Position = position;
        }
    }
}
