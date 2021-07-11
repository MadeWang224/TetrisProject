using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每一个图形上的组件
/// </summary>
public class Shape : MonoBehaviour
{
    /// <summary>
    /// 旋转用的轴心点
    /// </summary>
    private Transform pivot;
    /// <summary>
    /// 控制组件
    /// </summary>
    private Control ctrl;
    /// <summary>
    /// 游戏管理组件
    /// </summary>
    private GameManager gameManager;
    /// <summary>
    /// 是否暂停
    /// </summary>
    private bool isPause = false;
    /// <summary>
    /// 计时器
    /// </summary>
    private float timer = 0;
    /// <summary>
    /// 每次移动间隔
    /// </summary>
    private float stepTime = 0.8f;
    /// <summary>
    /// 加速比例
    /// </summary>
    private int multiple = 8;

    private void Awake()
    {
        pivot = transform.Find("Pivot");
    }

    private void Update()
    {
        if (isPause)
            return;
        timer += Time.deltaTime;
        if(timer>stepTime)
        {
            timer = 0;
            Fall();
        }
        InputControl();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="color"></param>
    /// <param name="ctrl"></param>
    /// <param name="gameManager"></param>
    public void Init(Color color,Control ctrl,GameManager gameManager)
    {
        foreach (Transform t in transform)
        {
            if(t.tag=="Block")
            {
                //改变颜色
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        //获取控制器
        this.ctrl = ctrl;
        //获取游戏控制组件
        this.gameManager = gameManager;
    }

    /// <summary>
    /// 下落
    /// </summary>
    private void Fall()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        if(ctrl.model.IsValidMapPosition(this.transform)==false)
        {
            pos.y += 1;
            transform.position = pos;
            isPause = true;
            bool isLineClear = ctrl.model.PlaceShape(this.transform);
            if (isLineClear)
                ctrl.audioManager.PlayLineClear();
            gameManager.FallDown();
            return;
        }
        //音效
        ctrl.audioManager.PlayDrop();
    }

    /// <summary>
    /// 输入控制
    /// </summary>
    private void InputControl()
    {
        //左右
        float h = 0;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            h = -1;
        }else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            h = 1;
        }
        if(h!=0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if(ctrl.model.IsValidMapPosition(this.transform)==false)
            {
                pos.x -= h;
                transform.position = pos;
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
        }
        //空格 转动
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if(ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
        }
        //下  加速下落
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            stepTime /= multiple;
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            stepTime = 0.8f;
        }
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        isPause = true;
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public void Resume()
    {
        isPause = false;
    }
}
