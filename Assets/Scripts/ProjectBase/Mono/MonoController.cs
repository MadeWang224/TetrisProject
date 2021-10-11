using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// mono模块
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    private void Start()
    {
        //此对象不可移除
        //从而方便别的对象找到该物体，从而获取脚本，从而添加方法
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (updateEvent != null) {
            updateEvent();
        }
    }
    /// <summary>
    /// 为外部提供的添加帧更新事件的方法
    /// </summary>
    /// <param name="func"></param>
    public void AddUpdateListener(UnityAction func)
    {
        updateEvent += func;
    }
    /// <summary>
    /// 为外部提供的移除帧更新事件的方法
    /// </summary>
    /// <param name="func"></param>
    public void RemoveUpdateListener(UnityAction func) {
        updateEvent -= func;
    } 
}
