using System;
using System.Collections.Generic;
using UnityEngine;

public class ResPath
{

    private static Dictionary<string, string> pathDic = new Dictionary<string, string>();
    const string pathFilePath = "PrefabPath";

    //初始化
    public static void Init()
    {
        LoadPath();
    }

    //加载本地路径
    static void LoadPath()
    {
        try
        {
            TextAsset pathFile = Resources.Load<TextAsset>(pathFilePath);
            string[] pathes = pathFile.text.Split("\r\n".ToCharArray());
            for (int i = 0; i < pathes.Length; i++)
            {
                if (string.IsNullOrEmpty(pathes[i])) continue;
                string[] path = pathes[i].Split('\t');
                //Debug.Log(path[0] + " " + path[1]);
                pathDic.Add(path[0], path[1]);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("check filePath --->" + pathFilePath + "   " + e.ToString());
        }
    }

    /// <summary>
    /// 获取资源路径
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetPath(string name)
    {
        if (pathDic.ContainsKey(name))
        {
            return pathDic[name];
        }

        Debug.Log("get path name is not exist ->" + name);
        return null;
    }
}

