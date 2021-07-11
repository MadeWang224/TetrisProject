using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Menu;
        //添加变换
        AddTransition(Transition.StartButtonClick, StateID.Play);
    }
    public override void DoBeforeEntering()
    {
        //显示主界面
        ctrl.view.ShowMenu();
        //缩小
        ctrl.cameraManager.ZoomOut();
    }
    public override void DoBeforeLeaving()
    {
        //隐藏主界面
        ctrl.view.HideMenu();
    }

    /// <summary>
    /// 开始游戏按钮
    /// </summary>
    public void OnStartButtonClick()
    {
        //音效
        ctrl.audioManager.PlayCursor();
        //改变状态
        fsm.PerformTransition(Transition.StartButtonClick);
    }

    /// <summary>
    /// 数据记录按钮
    /// </summary>
    public void OnRankButtonClick()
    {
        ctrl.view.ShowRankUI(ctrl.model.Score, ctrl.model.HighScore, ctrl.model.NumbersGame);
    }

    /// <summary>
    /// 清除数据按钮
    /// </summary>
    public void OnDestroyButtonClick()
    {
        //清除数据
        ctrl.model.ClearData();
        //更新数据
        OnRankButtonClick();
    }

    /// <summary>
    /// 主界面重置按钮
    /// </summary>
    public void OnRestartButtonClick()
    {
        ctrl.model.Restart();
        ctrl.gameManager.ClearShape();
        fsm.PerformTransition(Transition.StartButtonClick);
    }
}
