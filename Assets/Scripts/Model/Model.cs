using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    /// <summary>
    /// 显示行数
    /// </summary>
    public const int NORMAL_ROWS = 20;
    /// <summary>
    /// 最大行数
    /// </summary>
	public const int MAX_ROWS = 23;
    /// <summary>
    /// 最大列数
    /// </summary>
	public const int MAX_COLUMNS = 10;
    /// <summary>
    /// 二维数组 地图
    /// </summary>
	private Transform[,] map = new Transform[MAX_COLUMNS, MAX_ROWS];
    /// <summary>
    /// 分数
    /// </summary>
    private int score = 0;
    public int Score { get { return score; } }
    /// <summary>
    /// 最高分
    /// </summary>
    private int highScore = 0;
    public int HighScore { get { return highScore; } }
    /// <summary>
    /// 游玩次数
    /// </summary>
    private int numbersGame = 0;
    public int NumbersGame { get { return numbersGame; } }
    /// <summary>
    /// 数据是否需要更新
    /// </summary>
    public bool isDataUpdate = false;

    private void Awake()
    {
        //加载数据
        LoadData();
    }

    /// <summary>
    /// shape是否处于地图内
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool IsValidMapPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block")
                continue;
            //取整
            Vector2 pos = child.position.Round();
            //是否在地图内
            if (IsInsideMap(pos) == false)
                return false;
            //不能重叠
            if (map[(int)pos.x, (int)pos.y] != null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 是否在地图范围内
    /// </summary>
    /// <param name="pos"></param>
    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }

    /// <summary>
    /// 添加内容到map中
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
    /// 检查地图中满足消除条件的行
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
                //删除此行
                DeleteRow(i);
                //上面的行下移
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
    /// 检查一行是否满
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
    /// 消行
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
    /// 所有行下移一行
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
    /// 下移一行
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
                //下移
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        numbersGame = PlayerPrefs.GetInt("NumbersGame", 0);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    private void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("NumbersGame", numbersGame);
    }

    /// <summary>
    /// 游戏是否结束
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
    /// 重置
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
    /// 清除数据
    /// </summary>
    public void ClearData()
    {
        score = 0;
        highScore = 0;
        numbersGame = 0;
        SaveData();
    }
}
