using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private AssetReference gameScene;


    void Start()
    {
        gameScene.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += (AsyncOperationHandle<SceneInstance> obj) =>
        {
            gameScene.ReleaseAsset();
            SceneManager.UnloadSceneAsync(0);
        };
    }


}
