using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//monoBehaviour脚本的单例模式
//继承自动创建的单例模式基类 不需要手动添加脚本
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        if(instance==null)
        {
            GameObject obj = new GameObject();
            //设置对象的名字为脚本名
            obj.name = typeof(T).ToString();

            //过场景不移除
            GameObject.DontDestroyOnLoad(obj);

            instance = obj.AddComponent<T>();
        }
        return instance;
    }
}
