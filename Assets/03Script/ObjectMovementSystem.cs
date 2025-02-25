using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ObjectMovementSystem : ISystem {
	
	[BurstCompile]
	public void OnUpdate(ref SystemState state) {
		var rect = new float4(-10, 10, -10, 10);
		if (!SystemAPI.TryGetSingletonEntity<CubeSpawnerComponent>(out Entity spawner)) {
			var sp =SystemAPI.GetComponentRO<CubeSpawnerComponent>(spawner);
			rect = sp.ValueRO.Bound;
		}
		
		foreach (var (obj, tr) in SystemAPI.Query<RefRW<CubeObjectComponent>, RefRW<LocalTransform>>()) {
			var newPos = tr.ValueRO.Position + obj.ValueRO.TargetVector * obj.ValueRO.Speed * SystemAPI.Time.DeltaTime; 
			tr.ValueRW.Position = newPos;
			var vector = obj.ValueRO.TargetVector;
			if(newPos.x < rect.x || newPos.x > rect.y) {
				vector.x = -vector.x;
			}
			if(newPos.y < rect.z || newPos.y > rect.w) {
				vector.y = -vector.y;
			}
			obj.ValueRW.TargetVector = vector;
		}
	}
}
