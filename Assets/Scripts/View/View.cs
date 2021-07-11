using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ͼ
/// </summary>
public class View : MonoBehaviour
{
    /// <summary>
    ///������
    /// </summary>
    private Control ctrl;
    #region UI����
    /// <summary>
    /// ������ui
    /// </summary>
	private RectTransform menuUI;
    /// <summary>
    /// ��Ϸ�������ui
    /// </summary>
    private RectTransform gameUI;
    /// <summary>
    /// ��Ϸ����ui
    /// </summary>
    private GameObject gameOverUI;
    /// <summary>
    /// ���ý���ui
    /// </summary>
    private GameObject settingUI;
    /// <summary>
    /// ���ݼ�¼����ui
    /// </summary>
    private GameObject rankUI;
    #endregion

    #region UI���
    /// <summary>
    /// ��Ϸlogo��ui
    /// </summary>
    private RectTransform logoName;
    /// <summary>
    /// ���ð�ť
    /// </summary>
    private GameObject restartButton;
    /// <summary>
    /// ������־
    /// </summary>
    private GameObject mute;
    #endregion

    #region �����������
    /// <summary>
    /// ����������
    /// </summary>
    private Text score;
    /// <summary>
    /// ���������߷���
    /// </summary>
    private Text highScore;
    /// <summary>
    /// ��Ϸ��������
    /// </summary>
    private Text gameOverScore;
    #endregion

    #region ��¼��������
    /// <summary>
    /// ��¼�������
    /// </summary>
    private Text rankScore;
    /// <summary>
    /// ��¼������߷���
    /// </summary>
    private Text rankHighScore;
    /// <summary>
    /// ��¼�����������
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
    /// ��ʾ������
    /// </summary>
    public void ShowMenu()
    {
        //logo
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-111.5f, 0.5f);
        //��ť��
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(81.21298f, 0.5f);
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void HideMenu()
    {
        //logo
        logoName.DOAnchorPosY(111.5f, 0.5f).OnComplete(() =>
        {
            logoName.gameObject.SetActive(false);
        });
        //��ť��
        menuUI.DOAnchorPosY(-81.21298f, 0.5f).OnComplete(() =>
        {
            menuUI.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="score"></param>
    /// <param name="highScore"></param>
    public void ShowGameUI(int score=0,int highScore=0)
    {
        //��ȡ����
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        //��ʾui
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-155.88f, 0.5f);
    }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="score"></param>
    /// <param name="highScore"></param>
    public void UpdateGameUI(int score = 0, int highScore = 0)
    {
        //���·�������
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
    }

    /// <summary>
    /// �����������
    /// </summary>
    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(155.89f, 0.5f).OnComplete(() =>
        {
            gameUI.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// ��ʾ���ð�ť
    /// </summary>
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    /// <summary>
    /// ��ʾ��Ϸ��������
    /// </summary>
    /// <param name="score"></param>
    public void ShowGameOverUI(int score=0)
    {
        gameOverUI.SetActive(true);
        //���·���
        gameOverScore.text = score.ToString();
    }

    /// <summary>
    /// ������Ϸ��������
    /// </summary>
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    /// <summary>
    /// ������ҳ��ť
    /// </summary>
    public void OnHomeButtonClick()
    {
        //��Ч
        ctrl.audioManager.PlayCursor();
        //���¼���
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ���ð�ť
    /// </summary>
    public void OnSettingButtonClick()
    {
        //��Ч
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }

    /// <summary>
    /// ������ť
    /// </summary>
    /// <param name="isActive"></param>
    public void SetMuteActive(bool isActive)
    {
        mute.SetActive(isActive);
    }

    /// <summary>
    /// ����ҳ�水ťЧ��
    /// </summary>
    public void OnSettingUIClick()
    {
        //��Ч
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(false);
    }

    /// <summary>
    /// ���ݼ�¼����
    /// </summary>
    /// <param name="score"></param>
    /// <param name="highScore"></param>
    /// <param name="numbersGame"></param>
    public void ShowRankUI(int score,int highScore,int numbersGame)
    {
        //���ݸ���
        this.rankScore.text = score.ToString();
        this.rankHighScore.text = highScore.ToString();
        this.rankNumbersGame.text = numbersGame.ToString();
        rankUI.SetActive(true);
    }

    /// <summary>
    /// ���ݼ�¼���水ťЧ��
    /// </summary>
    public void OnRankUIClick()
    {
        rankUI.SetActive(false);
    }
}
