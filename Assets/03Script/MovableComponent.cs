using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MovableAuthoring : MonoBehaviour {
	public Vector2 Direction;
	public float Speed;
	public Color SpriteColor;
	public SpriteRenderer SprRenderer;
	
	class Baker : Baker<MovableAuthoring>
	{
		public override void Bake(MovableAuthoring authoring)
		{
			var entity = GetEntity(TransformUsageFlags.Renderable | TransformUsageFlags.WorldSpace);
			AddComponent(entity, new MovableComponent
			{
				Direction = authoring.Direction,
				Speed = authoring.Speed,
				SpriteColor = authoring.SpriteColor
			});
		}
	}
}

public struct MovableComponent : IComponentData
{
	public float2 Direction;
	public float Speed;
	public Color SpriteColor;
}

public struct TestObjectTag : IComponentData { }