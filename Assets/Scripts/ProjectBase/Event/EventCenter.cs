using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件基类的空接口
/// </summary>
public interface IEventInfo {
    //这是一个空接口
}
public class EventInfo<T> : IEventInfo {
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action) {
        actions += action;
    }
}
public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}


/// <summary>
/// 事件中心模块
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    //字典中，key对应着事件的名字，
    //value对应的是监听这个事件对应的委托方法们（重点圈住：们）
    /// <summary>
    /// 事件与方法字典
    /// </summary>
    private Dictionary<string, IEventInfo> eventDic
        = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <typeparam name="T">委托的参数类型</typeparam>
    /// <param name="name">事件的名字</param>
    /// <param name="action">处理事件的委托</param>
    public void AddEventListener<T>(string name, UnityAction<T> action) 
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            //添加方法
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        //没有的情况
        else 
        {
            //添加新事件和对应方法
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">处理事件的委托</param>
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }

    /// <summary>
    /// 通过事件名字进行事件触发
    /// </summary>
    /// <typeparam name="T">委托的参数类型</typeparam>
    /// <param name="name">事件名字</param>
    /// <param name="info">传入的参数</param>
    public void EventTrigger<T>(string name,T info) {
        //有没有对应的事件监听
        //有的情况（有人关心这个事件）
        if (eventDic.ContainsKey(name))
        {
            //调用委托（依次执行委托中的方法）
            //？是一个C#的简化操作,存在，则直接调用委托
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }
    }
    /// <summary>
    /// 通过事件名字进行事件触发
    /// </summary>
    /// <param name="name">事件名字</param>
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions?.Invoke();
        }
    }
    /// <summary>
    /// 移除对应的事件监听
    /// </summary>
    /// <typeparam name="T">委托的参数类型</typeparam>
    /// <param name="name">事件名字</param>
    /// <param name="action">处理事件的委托</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action) 
    {
        if (eventDic.ContainsKey(name))
        {
            //移除这个委托
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }
    /// <summary>
    /// 移除对应的事件监听
    /// </summary>
    /// <param name="name">事件名字</param>
    /// <param name="action">处理事件的委托</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    /// <summary>
    /// 清空所有事件监听(主要用在切换场景时)
    /// </summary>
    public void Clear() 
    {
        eventDic.Clear();
    }
}
