using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	/// <summary>
	/// 游戏是否暂停
	/// </summary>
	private bool isPause = true;
    /// <summary>
    /// 当前存储的shape
    /// </summary>
    private Shape currentShape = null;
    /// <summary>
    /// 控制器
    /// </summary>
    private Control ctrl;
    /// <summary>
    /// 所有shape
    /// </summary>
    public Shape[] shapes;
    /// <summary>
    /// 所有颜色
    /// </summary>
    public Color[] colors;
    /// <summary>
    /// 父物体
    /// </summary>
    private Transform blocks;

    private void Awake()
    {
        ctrl = GetComponent<Control>();
        blocks = transform.Find("Blocks");
    }

    private void Update()
    {
        if (isPause)
            return;
        if(currentShape==null)
        {
            SpawnShape();
        }
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        isPause = false;
        if (currentShape != null)
        {
            currentShape.Resume();
        }
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void PauseGame()
    {
        isPause = true;
        if(currentShape!=null)
        {
            currentShape.Pause();
        }
    }

    /// <summary>
    /// 产生新图形
    /// </summary>
    private void SpawnShape()
    {
        //随机
        int index = Random.Range(0, shapes.Length);
        int indexColor = Random.Range(0, colors.Length);
        //类型
        currentShape = GameObject.Instantiate(shapes[index],blocks);
        //颜色
        currentShape.Init(colors[indexColor],ctrl,this);
    }

    /// <summary>
    /// 下落结束
    /// </summary>
    public void FallDown()
    {
        currentShape = null;
        if(ctrl.model.isDataUpdate)
        {
            ctrl.view.UpdateGameUI(ctrl.model.Score, ctrl.model.HighScore);
        }
        foreach (Transform item in blocks)
        {
            //删除已经消除的图形
            if (item.childCount <= 1)
                Destroy(item.gameObject);
        }
        if(ctrl.model.IsGameOver())
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }

    /// <summary>
    /// 清除当前存储的图形
    /// </summary>
    public void ClearShape()
    {
        if(currentShape!=null)
        {
            Destroy(currentShape.gameObject);
            currentShape = null;
        }
    }
}
