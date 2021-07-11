using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÿһ��ͼ���ϵ����
/// </summary>
public class Shape : MonoBehaviour
{
    /// <summary>
    /// ��ת�õ����ĵ�
    /// </summary>
    private Transform pivot;
    /// <summary>
    /// �������
    /// </summary>
    private Control ctrl;
    /// <summary>
    /// ��Ϸ�������
    /// </summary>
    private GameManager gameManager;
    /// <summary>
    /// �Ƿ���ͣ
    /// </summary>
    private bool isPause = false;
    /// <summary>
    /// ��ʱ��
    /// </summary>
    private float timer = 0;
    /// <summary>
    /// ÿ���ƶ����
    /// </summary>
    private float stepTime = 0.8f;
    /// <summary>
    /// ���ٱ���
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
    /// ��ʼ��
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
                //�ı���ɫ
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        //��ȡ������
        this.ctrl = ctrl;
        //��ȡ��Ϸ�������
        this.gameManager = gameManager;
    }

    /// <summary>
    /// ����
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
        //��Ч
        ctrl.audioManager.PlayDrop();
    }

    /// <summary>
    /// �������
    /// </summary>
    private void InputControl()
    {
        //����
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
        //�ո� ת��
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
        //��  ��������
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
    /// ��ͣ
    /// </summary>
    public void Pause()
    {
        isPause = true;
    }

    /// <summary>
    /// �ָ�
    /// </summary>
    public void Resume()
    {
        isPause = false;
    }
}
