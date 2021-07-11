using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 视图
/// </summary>
public class View : MonoBehaviour
{
    /// <summary>
    ///控制器
    /// </summary>
    private Control ctrl;
    #region UI界面
    /// <summary>
    /// 主界面ui
    /// </summary>
	private RectTransform menuUI;
    /// <summary>
    /// 游戏游玩界面ui
    /// </summary>
    private RectTransform gameUI;
    /// <summary>
    /// 游戏结束ui
    /// </summary>
    private GameObject gameOverUI;
    /// <summary>
    /// 设置界面ui
    /// </summary>
    private GameObject settingUI;
    /// <summary>
    /// 数据记录界面ui
    /// </summary>
    private GameObject rankUI;
    #endregion

    #region UI组件
    /// <summary>
    /// 游戏logo的ui
    /// </summary>
    private RectTransform logoName;
    /// <summary>
    /// 重置按钮
    /// </summary>
    private GameObject restartButton;
    /// <summary>
    /// 禁音标志
    /// </summary>
    private GameObject mute;
    #endregion

    #region 游玩界面数据
    /// <summary>
    /// 游玩界面分数
    /// </summary>
    private Text score;
    /// <summary>
    /// 游玩界面最高分数
    /// </summary>
    private Text highScore;
    /// <summary>
    /// 游戏结束分数
    /// </summary>
    private Text gameOverScore;
    #endregion

    #region 记录界面数据
    /// <summary>
    /// 记录界面分数
    /// </summary>
    private Text rankScore;
    /// <summary>
    /// 记录界面最高分数
    /// </summary>
    private Text rankHighScore;
    /// <summary>
    /// 记录界面游玩次数
    /// </summary>
    private Text rankNumbersGame; 
    #endregion

    private void Awake()
    {
        ctrl = GameObject.FindGameObjectWithTag("Control").GetComponent<Control>();

        logoName = transform.Find("Canvas/LogoName") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;

        restartButton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;
        
        score = transform.Find("Canvas/GameUI/ScoreLabel/Text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HighScoreLabel/Text").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/Text").GetComponent<Text>();

        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/Text").GetComponent<Text>();
        rankHighScore = transform.Find("Canvas/RankUI/HighLabel/Text").GetComponent<Text>();
        rankNumbersGame = transform.Find("Canvas/RankUI/NumberGamesLabel/Text").GetComponent<Text>();
    }

    /// <summary>
    /// 显示主界面
    /// </summary>
    public void ShowMenu()
    {
        //logo
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-111.5f, 0.5f);
        //按钮栏
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(81.21298f, 0.5f);
    }

    /// <summary>
    /// 隐藏主界面
    /// </summary>
    public void HideMenu()
    {
        //logo
        logoName.DOAnchorPosY(111.5f, 0.5f).OnComplete(() =>
        {
            logoName.gameObject.SetActive(false);
        });
        //按钮栏
        menuUI.DOAnchorPosY(-81.21298f, 0.5f).OnComplete(() =>
        {
            menuUI.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// 游玩界面
    /// </summary>
    /// <param name="score"></param>
    /// <param name="highScore"></param>
    public void ShowGameUI(int score=0,int highScore=0)
    {
        //获取分数
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        //显示ui
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-155.88f, 0.5f);
    }

    /// <summary>
    /// 更新游玩界面
    /// </summary>
    /// <param name="score"></param>
    /// <param name="highScore"></param>
    public void UpdateGameUI(int score = 0, int highScore = 0)
    {
        //更新分数数据
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
    }

    /// <summary>
    /// 隐藏游玩界面
    /// </summary>
    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(155.89f, 0.5f).OnComplete(() =>
        {
            gameUI.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// 显示重置按钮
    /// </summary>
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    /// <summary>
    /// 显示游戏结束界面
    /// </summary>
    /// <param name="score"></param>
    public void ShowGameOverUI(int score=0)
    {
        gameOverUI.SetActive(true);
        //更新分数
        gameOverScore.text = score.ToString();
    }

    /// <summary>
    /// 隐藏游戏结束界面
    /// </summary>
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    /// <summary>
    /// 返回主页按钮
    /// </summary>
    public void OnHomeButtonClick()
    {
        //音效
        ctrl.audioManager.PlayCursor();
        //重新加载
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 设置按钮
    /// </summary>
    public void OnSettingButtonClick()
    {
        //音效
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }

    /// <summary>
    /// 禁音按钮
    /// </summary>
    /// <param name="isActive"></param>
    public void SetMuteActive(bool isActive)
    {
        mute.SetActive(isActive);
    }

    /// <summary>
    /// 设置页面按钮效果
    /// </summary>
    public void OnSettingUIClick()
    {
        //音效
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(false);
    }

    /// <summary>
    /// 数据记录界面
    /// </summary>
    /// <param name="score"></param>
    /// <param name="highScore"></param>
    /// <param name="numbersGame"></param>
    public void ShowRankUI(int score,int highScore,int numbersGame)
    {
        //数据更新
        this.rankScore.text = score.ToString();
        this.rankHighScore.text = highScore.ToString();
        this.rankNumbersGame.text = numbersGame.ToString();
        rankUI.SetActive(true);
    }

    /// <summary>
    /// 数据记录界面按钮效果
    /// </summary>
    public void OnRankUIClick()
    {
        rankUI.SetActive(false);
    }
}
