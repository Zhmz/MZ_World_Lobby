using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceGameObject prefab;


    void Start()
    {

        prefab.InstantiateAsync().Completed += (AsyncOperationHandle<GameObject> obj) =>
        {
            prefab.ReleaseAsset();
        }; 
    }
}
