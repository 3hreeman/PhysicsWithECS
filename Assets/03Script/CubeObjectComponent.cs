using Unity.Entities;
using Unity.Mathematics;

public struct CubeObjectComponent : IComponentData {
	public float Speed;
	public int Hp;
	public float3 Position;
	public float3 TargetVector;
	public float4 Color;
}