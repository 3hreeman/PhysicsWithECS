using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

[BurstCompile]
public partial struct CubeSpawnerSystem : ISystem
{

	[BurstCompile]
	public void OnUpdate(ref SystemState state) {
		if (!SystemAPI.TryGetSingletonEntity<CubeSpawnerComponent>(out var spawnerEntity)) {
			return;
		}

		var spawner = SystemAPI.GetComponentRW<CubeSpawnerComponent>(spawnerEntity);

		if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime) {
			var newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
			// var transform = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
			var tr = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
			tr.ValueRW.Position = GetSpawnPosition(spawner.ValueRO.Bound);
			
			var comp = SystemAPI.GetComponentRW<CubeObjectComponent>(newEntity);
			comp.ValueRW.Hp = Random.Range(1, 10);
			comp.ValueRW.Speed = Random.Range(3f, 5f);
			comp.ValueRW.Position = tr.ValueRO.Position;
			var randomVec = Random.insideUnitSphere;
			randomVec.z = 0;
			comp.ValueRW.TargetVector = randomVec;
			comp.ValueRW.Color = new float4(Random.value, Random.value, Random.value, 1);
			tr.ValueRW.RotateX(randomVec.x);
			tr.ValueRW.RotateY(randomVec.y);
			tr.ValueRW.RotateZ(randomVec.z);
			
			//TODO change color
			
			spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
		}
	}

	public float3 GetSpawnPosition(float4 Bound) {
		return new float3(
			UnityEngine.Random.Range(Bound.x, Bound.y), UnityEngine.Random.Range(Bound.z, Bound.w), 0
		);
	}
}
