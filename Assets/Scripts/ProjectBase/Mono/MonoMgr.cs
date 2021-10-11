using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// mono模块的管理类,继承单例
/// </summary>
public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController controller;
    public MonoMgr() {
        //新建一个物体
        GameObject obj = new GameObject("MonoController");
        //给物体添加组件
        controller = obj.AddComponent<MonoController>();
    }
    /// <summary>
    /// 为外部提供的添加帧更新事件的方法
    /// </summary>
    /// <param name="func"></param>
    public void AddUpdateListener(UnityAction func)
    {
        controller.AddUpdateListener(func);

    }
    /// <summary>
    /// 为外部提供的移除帧更新事件的方法
    /// </summary>
    /// <param name="func"></param>
    public void RemoveUpdateListener(UnityAction func)
    {
        controller.RemoveUpdateListener(func);
    }

    /// <summary>
    /// 开启协程
    /// </summary>
    /// <param name="routine">协程</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator routine) {
        return controller.StartCoroutine(routine);
    }
    /// <summary>
    /// 开启协程
    /// </summary>
    /// <param name="methodName">协程名字</param>
    /// <param name="value">传入的值</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value) {
        return controller.StartCoroutine(methodName,value);
    }
    /// <summary>
    /// 开启协程
    /// </summary>
    /// <param name="methodName">协程名字</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(string methodName) {
        return controller.StartCoroutine(methodName);
    }
}
