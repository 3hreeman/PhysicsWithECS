using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawnerAuthoring : MonoBehaviour {
    public GameObject Prefab;
    public float SpawnRate;
    public float2 LimitX;
    public float2 LimitY;
    
    class CubeSpawnerBaker : Baker<CubeSpawnerAuthoring> {
        public override void Bake(CubeSpawnerAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);
            var spawner = new CubeSpawnerComponent {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                NextSpawnTime = 0f,
                SpawnRate = authoring.SpawnRate,
                Bound = new float4(authoring.LimitX.x, authoring.LimitX.y, authoring.LimitY.x, authoring.LimitY.y)
            }; 
            
            AddComponent(entity, spawner);
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3((LimitX.x + LimitX.y) / 2, (LimitY.x + LimitY.y) / 2, 0), new Vector3(LimitX.y - LimitX.x, LimitY.y - LimitY.x, 0));
    }
}
