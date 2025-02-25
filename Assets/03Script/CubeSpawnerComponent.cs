using Unity.Entities;
using Unity.Mathematics;

public struct CubeSpawnerComponent : IComponentData {
	public Entity Prefab;
	public float NextSpawnTime;
	public float SpawnRate;
	public float4 Bound;
}