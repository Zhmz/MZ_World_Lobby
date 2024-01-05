using System.Collections;
using System.Collections.Generic;
using Tuwan;
using UnityEngine;
using UnityEngine.UI;

public class TuwanFramework : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        TuwanSceneUtils.AdjustScreenToPortrait();
#if UNITY_EDITOR
        ScreenUtils.SetPortraitResolution();
#endif
    }

}
