using Unity.Entities;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public GameObject testObjectPrefab;

    private void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        // var settings = new GameObjectConversionSettings(World.DefaultGameObjectInjectionWorld, GameObjectConversionUtility.ConversionFlags.AssignName);
        // Entity prefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(testObjectPrefab, settings);
        // entityManager.CreateSingleton(new PrefabSingleton { Prefab = prefabEntity });
    }
}
