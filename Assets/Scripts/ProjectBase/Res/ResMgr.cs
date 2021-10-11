using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 资源加载模块
/// </summary>
public class ResMgr : BaseManager<ResMgr>
{
    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="name">资源名字/路径</param>
    /// <returns></returns>
    public T Load<T>(string name) where T:Object{
        T res = Resources.Load<T>(name);
        //如果对象是一个GameObject类型的，我把它实例化后，再返回出去直接使用。
        if (res is GameObject) 
            return GameObject.Instantiate(res);
        else //else情况示例：TextAsset、AudioClip
            return res;  
    }

    /// <summary>
    /// 异步加载资源 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">资源名字/路径</param>
    /// <param name="callback">回调函数</param>
    public void LoadAsync<T>(string name,UnityAction<T> callback) where T:Object
    {
        //开启异步加载的协程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync<T>(name,callback));
    }
    /// <summary>
    /// 异步加载的协程
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="name">资源名字/路径</param>
    /// <param name="callback">回调函数</param>
    /// <returns></returns>
    private IEnumerator ReallyLoadAsync<T>(string name,UnityAction<T> callback) where T:Object
    {
        ResourceRequest r=Resources.LoadAsync<T>(name);
        yield return r;

        if (r.asset is GameObject)
        {
            //实例化一下再传给方法
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else {
            //直接传给方法
            callback(r.asset as T);
        }        
    }
}
