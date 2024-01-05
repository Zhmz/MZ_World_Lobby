using UnityEditor;

[InitializeOnLoad]
public class GlobalConfig
{
    static GlobalConfig()
    {
        PlayerSettings.Android.keystorePass = "tuwan2020*&";
        PlayerSettings.Android.keyaliasName = "tuwan";
        PlayerSettings.Android.keyaliasPass = "tuwan2020*&";
    }
}

