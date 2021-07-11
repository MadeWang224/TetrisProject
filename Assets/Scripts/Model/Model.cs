using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public const int NORMAL_ROWS = 20;
    /// <summary>
    /// �������
    /// </summary>
	public const int MAX_ROWS = 23;
    /// <summary>
    /// �������
    /// </summary>
	public const int MAX_COLUMNS = 10;
    /// <summary>
    /// ��ά���� ��ͼ
    /// </summary>
	private Transform[,] map = new Transform[MAX_COLUMNS, MAX_ROWS];
    /// <summary>
    /// ����
    /// </summary>
    private int score = 0;
    public int Score { get { return score; } }
    /// <summary>
    /// ��߷�
    /// </summary>
    private int highScore = 0;
    public int HighScore { get { return highScore; } }
    /// <summary>
    /// �������
    /// </summary>
    private int numbersGame = 0;
    public int NumbersGame { get { return numbersGame; } }
    /// <summary>
    /// �����Ƿ���Ҫ����
    /// </summary>
    public bool isDataUpdate = false;

    private void Awake()
    {
        //��������
        LoadData();
    }

    /// <summary>
    /// shape�Ƿ��ڵ�ͼ��
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool IsValidMapPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block")
                continue;
            //ȡ��
            Vector2 pos = child.position.Round();
            //�Ƿ��ڵ�ͼ��
            if (IsInsideMap(pos) == false)
                return false;
            //�����ص�
            if (map[(int)pos.x, (int)pos.y] != null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// �Ƿ��ڵ�ͼ��Χ��
    /// </summary>
    /// <param name="pos"></param>
    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }

    /// <summary>
    /// ������ݵ�map��
    /// </summary>
    /// <param name="t"></param>
    public bool PlaceShape(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block")
                continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }
        return CheckMap();
    }

    /// <summary>
    /// ����ͼ������������������
    /// </summary>
    private bool CheckMap()
    {
        int count = 0;
        for (int i=0; i < MAX_ROWS;i++)
        {
            bool isFull = CheckIsRowFull(i);
            if(isFull)
            {
                count++;
                //ɾ������
                DeleteRow(i);
                //�����������
                MoveDownRowsAbove(i + 1);
                i--;
            }
        }
        if (count > 0)
        {
            score += (count * 100);
            if(score>highScore)
            {
                highScore = score;
            }
            isDataUpdate = true;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// ���һ���Ƿ���
    /// </summary>
    /// <param name="row"></param>
    private bool CheckIsRowFull(int row)
    {
        for(int i=0;i<MAX_COLUMNS;i++)
        {
            if (map[i, row] == null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="row"></param>
    private void DeleteRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    /// <summary>
    /// ����������һ��
    /// </summary>
    /// <param name="row"></param>
    private void MoveDownRowsAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MOveDownRow(i);
        }
    }

    /// <summary>
    /// ����һ��
    /// </summary>
    /// <param name="row"></param>
    private void MOveDownRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                //����
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        numbersGame = PlayerPrefs.GetInt("NumbersGame", 0);
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("NumbersGame", numbersGame);
    }

    /// <summary>
    /// ��Ϸ�Ƿ����
    /// </summary>
    /// <returns></returns>
    public bool IsGameOver()
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j, i] != null)
                {
                    numbersGame++;
                    SaveData();
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Restart()
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            for (int j = 0; j < MAX_ROWS; j++)
            {
                if(map[i,j]!=null)
                {
                    Destroy(map[i, j].gameObject);
                    map[i, j] = null;
                }
            }
        }
        score = 0;
    }

    /// <summary>
    /// �������
    /// </summary>
    public void ClearData()
    {
        score = 0;
        highScore = 0;
        numbersGame = 0;
        SaveData();
    }
}
