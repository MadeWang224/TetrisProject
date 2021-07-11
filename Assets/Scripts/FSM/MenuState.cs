using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Menu;
        //��ӱ任
        AddTransition(Transition.StartButtonClick, StateID.Play);
    }
    public override void DoBeforeEntering()
    {
        //��ʾ������
        ctrl.view.ShowMenu();
        //��С
        ctrl.cameraManager.ZoomOut();
    }
    public override void DoBeforeLeaving()
    {
        //����������
        ctrl.view.HideMenu();
    }

    /// <summary>
    /// ��ʼ��Ϸ��ť
    /// </summary>
    public void OnStartButtonClick()
    {
        //��Ч
        ctrl.audioManager.PlayCursor();
        //�ı�״̬
        fsm.PerformTransition(Transition.StartButtonClick);
    }

    /// <summary>
    /// ���ݼ�¼��ť
    /// </summary>
    public void OnRankButtonClick()
    {
        ctrl.view.ShowRankUI(ctrl.model.Score, ctrl.model.HighScore, ctrl.model.NumbersGame);
    }

    /// <summary>
    /// ������ݰ�ť
    /// </summary>
    public void OnDestroyButtonClick()
    {
        //�������
        ctrl.model.ClearData();
        //��������
        OnRankButtonClick();
    }

    /// <summary>
    /// ���������ð�ť
    /// </summary>
    public void OnRestartButtonClick()
    {
        ctrl.model.Restart();
        ctrl.gameManager.ClearShape();
        fsm.PerformTransition(Transition.StartButtonClick);
    }
}
