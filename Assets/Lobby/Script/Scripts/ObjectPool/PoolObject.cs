using UnityEngine;

//对象池对象
//预制体对象需继承
public class PoolObject : MonoBehaviour
{
    //对象初始化时调用
    public virtual void OnCreate()
    {

    }

    //从对象池中取出调用
    public virtual void OnFetch()
    {

    }

    //回收时调用
    public virtual void OnRecycle()
    {

    }

    //销毁时调用
    public virtual void OnDestory()
    {

    }
}
