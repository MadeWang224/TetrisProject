using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 池子中不同容器的基类
/// </summary>
public class PoolData
{
    /// <summary>
    /// 对象挂载的父节点
    /// </summary>
    public GameObject fatherObj;
    /// <summary>
    /// 对象的容器
    /// </summary>
    public List<GameObject> poolList;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="obj">存入的物体</param>
    /// <param name="poolObj">池子</param>
    public PoolData(GameObject obj, GameObject poolObj)
    {
        //根据obj创建一个同名父类空物体，它的父物体为总Pool空物体
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;

        poolList =  new List<GameObject>() {  };

        PushObj(obj);
    }

    /// <summary>
    /// 存
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.parent = fatherObj.transform;
        //失活，让其隐藏
        obj.SetActive(false);
    }

    /// <summary>
    /// 取
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj() 
    {
        GameObject obj = null;
        //取出第一个
        obj = poolList[0];
        poolList.RemoveAt(0);
        //激活，让其展示
        obj.SetActive(true);
        //断开父子关系
        obj.transform.parent = null;

        return obj;
    }
}


/// <summary>
/// 缓存池模块
/// </summary>
public class PoolMgr : BaseManager<PoolMgr>
{

    /// <summary>
    /// 创建字段存储容器
    /// </summary>
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    /// <summary>
    /// 池子
    /// </summary>
    private GameObject poolObj;

    /// <summary>
    /// 取得游戏物体
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    public void GetObj(string name,UnityAction<GameObject> callback) {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            //通过委托返回给外部，让外部进行使用
            callback(poolDic[name].GetObj());
        }
        else {
            //缓存池中没有该物体，我们去目录中加载
            //外面传一个预设体的路径和名字，我内部就去加载它
            ResMgr.GetInstance().LoadAsync<GameObject>(name,(o)=> {
                o.name = name;
                callback(o);
            });
        }
    }

    /// <summary>
    /// 外界返还游戏物体
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(string name,GameObject obj) 
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");

        }
        //里面有记录这个键
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        //未曾记录这个键
        else 
        {
            poolDic.Add(name, new PoolData(obj,poolObj) { });
        }
    }
    
    //清空缓存池的方法
    //主要用在场景切换时
    public void Clear() {
        poolDic.Clear();
        poolObj = null;
    }
}
