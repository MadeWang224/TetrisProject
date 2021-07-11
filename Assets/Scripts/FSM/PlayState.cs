using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游玩状态
/// </summary>
public class PlayState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Play;
        //添加变换
        AddTransition(Transition.PauseButtonClick, StateID.Menu);
    }
    public override void DoBeforeEntering()
    {
        //显示面板,更新数据
        ctrl.view.ShowGameUI(ctrl.model.Score,ctrl.model.HighScore);
        //放大
        ctrl.cameraManager.ZoomIn();
        //开始游戏
        ctrl.gameManager.StartGame();
    }
    public override void DoBeforeLeaving()
    {
        //隐藏面板
        ctrl.view.HideGameUI();
        //显示重置按钮
        ctrl.view.ShowRestartButton();
        //暂停游戏
        ctrl.gameManager.PauseGame();
    }

    /// <summary>
    /// 暂停按钮
    /// </summary>
    public void OnPauseButtonClick()
    {
        //音效
        ctrl.audioManager.PlayCursor();
        //改变状态
        fsm.PerformTransition(Transition.PauseButtonClick);
    }

    /// <summary>
    /// 游戏结束界面的重置按钮
    /// </summary>
    public void OnRestartButtonClick()
    {
        //隐藏游戏结束按钮
        ctrl.view.HideGameOverUI();
        //重置
        ctrl.model.Restart();
        //开始
        ctrl.gameManager.StartGame();
        //更新显示
        ctrl.view.UpdateGameUI(0, ctrl.model.HighScore);
    }
}
