using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PoolManager
{
    private static Vector3 OutOfRange = new Vector3(9000, 9000, 9000);//超出边界的值 在回收的时候直接设置位置
 
    private static Dictionary<string, Dictionary<int, GameObject>> createPools = new Dictionary<string, Dictionary<int, GameObject>>();
    private static Dictionary<string, Dictionary<int, GameObject>> recyclePools = new Dictionary<string, Dictionary<int, GameObject>>();
 
    private static Transform s_poolParent;
 
 
    //对象池 父对象  也是回收的父对象  防止回收的对象被意外删除
    public static Transform PoolParent
    {
        get
        {
            if (s_poolParent == null)
            {
                GameObject instancePool = new GameObject("ObjectPool");
                s_poolParent = instancePool.transform;
                if (Application.isPlaying) UnityEngine.Object.DontDestroyOnLoad(s_poolParent);
            }
 
            return s_poolParent;
        }
    }
 
    public static Dictionary<string, Dictionary<int, GameObject>> GetCreatePool()
    {
        return createPools;
    }
 
    public static Dictionary<string, Dictionary<int, GameObject>> GetRecyclePool()
    {
        return recyclePools;
    }
 
    #region 创建
 
    //通过创建对象名字 直接实例化
    public static GameObject CreateGameObject(string name, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(true, name, null, parent, isActive);
    }
 
    //通过预制体 直接实例化
    public static GameObject CreateGameObject(GameObject prefab, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(true, null, prefab, parent, isActive);
    }
 
    //通过名字 对象池实例化
    public static GameObject CreateGameObjectByPool(string name, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(false, name, null, parent, isActive);
    }
 
    //通过预制体 直接实例化
    public static GameObject CreateGameObjectByPool(GameObject prefab, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(false, null, prefab, parent, isActive);
    }
 
    //通过创建对象名字 直接实例化
    public static T CreateGameObject<T>(string name, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(true, name, null, parent, isActive).GetComponent<T>();
    }
 
    //通过预制体 直接实例化
    public static T CreateGameObject<T>(GameObject prefab, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(true, null, prefab, parent, isActive).GetComponent<T>(); ;
    }
 
    //通过名字 对象池实例化
    public static T CreateGameObjectByPool<T>(string name, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(false, name, null, parent, isActive).GetComponent<T>(); ;
    }
 
    //通过预制体 直接实例化
    public static T CreateGameObjectByPool<T>(GameObject prefab, GameObject parent = null, bool isActive = true)
    {
        return GetNewObject(false, null, prefab, parent, isActive).GetComponent<T>(); ;
    }
 
 
    /// <summary>
    /// 获取一个新的对象
    /// </summary>
    /// <param name="isNew">是否创建新的对象</param>
    /// <param name="objName">实例化对象的名字</param>
    /// <param name="prefab">实例化预制体</param>
    /// <param name="parent">实例化父对象</param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    private static GameObject GetNewObject(bool isNew, string objName, GameObject prefab, GameObject parent = null, bool isActive = true)
    {
        GameObject go = null;
 
        //获取对象名字
        string name = objName;
        if (string.IsNullOrEmpty(name))
        {
            name = prefab.name;
        }
 
        if (!isNew && IsExist(name))
        {
            if (!recyclePools.ContainsKey(name))
            {
                //创建新的对象
                if (prefab != null)
                {
                    go = InstantiateObject(prefab, parent);
                }
                else
                {
                    go = NewGameObject(name, parent);
                }
            }
            else
            {
                //从回收池中加载
                List<int> ids = new List<int>(recyclePools[name].Keys);
                int id = ids[0];
                go = recyclePools[name][id];
                recyclePools[name].Remove(id);
                if (recyclePools[name].Count == 0) recyclePools.Remove(name);
            }
        }
        else
        {
            //直接创建新的对象
            if (prefab == null && !string.IsNullOrEmpty(objName))
            {
                go = NewGameObject(name, parent);
 
            }
            else if (prefab != null && string.IsNullOrEmpty(objName))
            {
                go = InstantiateObject(prefab, parent);
            }
        }
 
        if (go == null)
        {
            Debug.LogError("PoolManager 加载失败：" + name);
            return go;
        }
 
        //记录保存到createPool
        if (!createPools.ContainsKey(name)) createPools.Add(name, new Dictionary<int, GameObject>());
        createPools[name].Add(go.GetInstanceID(), go);
 
        //初始化
        PoolObject obj = go.GetComponent<PoolObject>();
        if (obj) obj.OnFetch();
 
        go.SetActive(isActive);
        go.transform.SetParent(parent ? parent.transform : null);
 
        return go;
    }
 
    //实例化
    private static GameObject InstantiateObject(GameObject prefab, GameObject parent = null)
    {
        if (prefab == null)
        {
            throw new Exception("CreateGameObject error : prefab  is null");
        }
        Transform transform = parent == null ? null : parent.transform;
        GameObject instanceTmp = UnityEngine.Object.Instantiate(prefab, transform);
        instanceTmp.name = prefab.name;
 
        PoolObject p = instanceTmp.GetComponent<PoolObject>();
        if (p != null) p.OnCreate();
 
        return instanceTmp;
    }
 
    //加载一个对象并实例化
    private static GameObject NewGameObject(string gameObjectName, GameObject parent = null)
    {
        /****暂时从Resources读取****/
        GameObject goTmp = Resources.Load<GameObject>(ResPath.GetPath(gameObjectName));
 
        if (goTmp == null)
        {
            throw new Exception("CreateGameObject error dont find :" + gameObjectName);
        }
 
        return InstantiateObject(goTmp, parent);
    }
 
    #endregion
 
    #region 销毁
 
    //回收对象
    public static void DestoryByRecycle(GameObject gameObject, bool isSetInactive = true)
    {
        if (!gameObject) return;
 
        string key = gameObject.name.Replace("(Clone)", "");
        if (!recyclePools.ContainsKey(key)) recyclePools.Add(key, new Dictionary<int, GameObject>());
 
        if(recyclePools[key].ContainsKey(gameObject.GetInstanceID()))
        {
            Debug.LogError("Recycle repeat by Destory -> " + gameObject.name);
            return;
        }
        recyclePools[key].Add(gameObject.GetInstanceID(), gameObject);
 
        if (isSetInactive) gameObject.SetActive(false);
        else gameObject.transform.position = OutOfRange;
 
        gameObject.name = key;
        gameObject.transform.SetParent(PoolParent);
        PoolObject obj = gameObject.GetComponent<PoolObject>();
        if (obj) obj.OnRecycle();
 
        if (createPools.ContainsKey(key) && createPools[key].ContainsKey(gameObject.GetInstanceID()))
        {
            createPools[key].Remove(gameObject.GetInstanceID());
        }
        else Debug.LogError("对象池不存在GameObject：" + gameObject + " 不能回收！");
    }
 
    ////延时多久 回收
    ////Timer有点问题
    //public static void DestoryByRecycle(GameObject gameObject, float time)
    //{
    //    Timer.DelayCallBack(time, (o) =>
    //    {
    //        DestoryByRecycle(gameObject);
    //    });
    //}
 
    //直接销毁
    public static void Destory(GameObject gameObject)
    {
        if (gameObject == null) return;
 
        string key = gameObject.name.Replace("(Clone)", "");
 
        PoolObject obj = gameObject.GetComponent<PoolObject>();
        if (obj) obj.OnDestory();
 
        //移除
        if(createPools.ContainsKey(key) && createPools[key].ContainsKey(gameObject.GetInstanceID()))
        {
            createPools[key].Remove(gameObject.GetInstanceID());
            if (createPools[key].Count == 0) createPools.Remove(key);
        }
 
        UnityEngine.Object.Destroy(gameObject);
    }
 
    //直接销毁
    public static void Destory(GameObject gameObject, float time)
    {
        if (gameObject == null) return;
 
        string key = gameObject.name.Replace("(Clone)", "");
 
        PoolObject obj = gameObject.GetComponent<PoolObject>();
        if (obj) obj.OnDestory();
 
        //移除
        if (createPools.ContainsKey(key) && createPools[key].ContainsKey(gameObject.GetInstanceID()))
        {
            createPools[key].Remove(gameObject.GetInstanceID());
            if (createPools[key].Count == 0) createPools.Remove(key);
        }
 
        UnityEngine.Object.Destroy(gameObject, time);
    }
 
    #endregion
 
    //判断是否存在改对象
    public static bool IsExist(string objName)
    {
        if(string.IsNullOrEmpty(objName))
        {
            Debug.LogError("PoolManager objName is null");
            return false;
        }
 
        if ((createPools.ContainsKey(objName) && createPools[objName].Count > 0)
            || recyclePools.ContainsKey(objName) && recyclePools[objName].Count > 0)
        {
            return true;
        }
 
        return false;
    }
 
    public static bool IsExist(GameObject gameObject)
    {
        if (gameObject == null) return false;
 
        return IsExist(gameObject.name.Replace("(Clone)", ""));
    }
 
    //对象池回收 已经创建的了的对象池  对没有元素的清空  销毁所有在回收对象池
    public static void PoolClean()
    {
        //清创创建了的对象池
        List<string> cleanPool = new List<string>();
        foreach (var item in createPools)
        {
            if (item.Value.Count == 0) cleanPool.Add(item.Key);
        }
 
        cleanPool.ForEach((s) => createPools.Remove(s));
 
        //清空回收对象池
        List<GameObject> cleanObj = new List<GameObject>();
        foreach (var item in recyclePools)
        {
            foreach (var v in item.Value)
            {
                cleanObj.Add(v.Value);
            }
        }
        cleanObj.ForEach((s) =>
        {
            PoolObject obj = s.GetComponent<PoolObject>();
            if (obj) obj.OnDestory();
            UnityEngine.Object.Destroy(s);
        });
        recyclePools.Clear();
    }
 
    //回收某一对象的池
    public static void PoolClean(string name)
    {
        if (recyclePools.ContainsKey(name))
        {
            foreach (var item in recyclePools[name])
            {
                PoolObject obj = item.Value.GetComponent<PoolObject>();
 
                if (obj) obj.OnDestory();
                UnityEngine.Object.Destroy(item.Value);
 
            }
            recyclePools.Remove(name);
        }
 
        if (createPools.ContainsKey(name) && createPools[name].Count == 0) createPools.Remove(name);
    }
 
}

