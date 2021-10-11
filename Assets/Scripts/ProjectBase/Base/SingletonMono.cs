using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//继承了monoBehaviour的单例模式,需要我们自己保证它的唯一性
//局限:当挂载多次时,单例模式失效
public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
	private static T instance;

	public static T GetInstance()
    {
        //继承了mono的脚本,不能直接new
        //只能通过拖动到对象上 或者 addComponent
        //U3D内部进行实例化
        return instance;
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}
