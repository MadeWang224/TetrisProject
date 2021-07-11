using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    /// <summary>
    /// ����
    /// </summary>
    [HideInInspector]
	public Model model;
    /// <summary>
    /// ��ͼ
    /// </summary>
    [HideInInspector]
	public View view;
    /// <summary>
    /// ״̬��
    /// </summary>
    private FSMSystem fsm;
    /// <summary>
    /// ���������
    /// </summary>
    [HideInInspector]
    public CameraManager cameraManager;
    /// <summary>
    /// ��Ϸ����
    /// </summary>
    [HideInInspector]
    public GameManager gameManager;
    /// <summary>
    /// ��Ƶ����
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
    /// �½�״̬��
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
