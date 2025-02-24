using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[BurstCompile]
public partial struct ObjectSpawnSystem : ISystem
{
    private Entity _testObjectPrefab;
    private bool _initialized;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PrefabSingleton>();
    }

    public void OnUpdate(ref SystemState state)
    {
        if (!_initialized)
        {
            _testObjectPrefab = SystemAPI.GetSingleton<PrefabSingleton>().Prefab;
            _initialized = true;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

            // 마우스 위치 가져오기
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            worldPos.z = 0;

            Entity entity = ecb.Instantiate(_testObjectPrefab);
            ecb.SetComponent(entity, LocalTransform.FromPosition(worldPos));

            // 랜덤한 방향 및 속도 설정
            float2 randomDirection = math.normalize(new float2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            float speed = UnityEngine.Random.Range(1f, 3f);
            ecb.AddComponent(entity, new MovableComponent { Direction = randomDirection, Speed = speed });

            ecb.Playback(state.EntityManager);
        }
    }
}

// 프리팹을 저장하는 싱글톤 컴포넌트
public struct PrefabSingleton : IComponentData
{
    public Entity Prefab;
}
