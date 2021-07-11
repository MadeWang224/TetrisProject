using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    /// <summary>
    /// 数据
    /// </summary>
    [HideInInspector]
	public Model model;
    /// <summary>
    /// 视图
    /// </summary>
    [HideInInspector]
	public View view;
    /// <summary>
    /// 状态机
    /// </summary>
    private FSMSystem fsm;
    /// <summary>
    /// 摄像机管理
    /// </summary>
    [HideInInspector]
    public CameraManager cameraManager;
    /// <summary>
    /// 游戏管理
    /// </summary>
    [HideInInspector]
    public GameManager gameManager;
    /// <summary>
    /// 音频管理
    /// </summary>
    [HideInInspector]
    public AudioManager audioManager;

    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        cameraManager = GetComponent<CameraManager>();
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        MakeFSM();
    }

    /// <summary>
    /// 新建状态机
    /// </summary>
    private void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach (FSMState state in states)
        {
            fsm.AddState(state,this);
        }
        MenuState s = GetComponentInChildren<MenuState>();
        fsm.SetCurrentState(s);
    }
}
