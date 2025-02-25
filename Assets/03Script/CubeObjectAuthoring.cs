using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeObjectAuthoring : MonoBehaviour
{
	
	class CubeObjectBaker : Baker<CubeObjectAuthoring> {
		public override void Bake(CubeObjectAuthoring authoring) {
			var entity = GetEntity(TransformUsageFlags.None);
			var data = new CubeObjectComponent() { };
			AddComponent<CubeObjectComponent>(entity, data);
		}
	}
}
