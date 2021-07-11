using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬
/// </summary>
public class PlayState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Play;
        //��ӱ任
        AddTransition(Transition.PauseButtonClick, StateID.Menu);
    }
    public override void DoBeforeEntering()
    {
        //��ʾ���,��������
        ctrl.view.ShowGameUI(ctrl.model.Score,ctrl.model.HighScore);
        //�Ŵ�
        ctrl.cameraManager.ZoomIn();
        //��ʼ��Ϸ
        ctrl.gameManager.StartGame();
    }
    public override void DoBeforeLeaving()
    {
        //�������
        ctrl.view.HideGameUI();
        //��ʾ���ð�ť
        ctrl.view.ShowRestartButton();
        //��ͣ��Ϸ
        ctrl.gameManager.PauseGame();
    }

    /// <summary>
    /// ��ͣ��ť
    /// </summary>
    public void OnPauseButtonClick()
    {
        //��Ч
        ctrl.audioManager.PlayCursor();
        //�ı�״̬
        fsm.PerformTransition(Transition.PauseButtonClick);
    }

    /// <summary>
    /// ��Ϸ������������ð�ť
    /// </summary>
    public void OnRestartButtonClick()
    {
        //������Ϸ������ť
        ctrl.view.HideGameOverUI();
        //����
        ctrl.model.Restart();
        //��ʼ
        ctrl.gameManager.StartGame();
        //������ʾ
        ctrl.view.UpdateGameUI(0, ctrl.model.HighScore);
    }
}
