using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : UIForm
{
    private GameObject m_GameObject;
    private Text m_ScoreText;
    private const string m_PrefabPath = "UIGameMenu";

    public override void OnEnter()
    {
        Global.GameStatus = GameStatus.Operating;
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        m_GameObject.transform.Find("UndoBtn").GetComponent<Button>().onClick.AddListener(OnClickUndoBtn);
        m_GameObject.transform.Find("SaveBtn").GetComponent<Button>().onClick.AddListener(OnClickSaveBtn);
        m_GameObject.transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);
        m_ScoreText = m_GameObject.transform.Find("ScoreText/Text").GetComponent<Text>();
        if (null != GameManager.Instance.CheckerboardControl)
        {
            GameManager.Instance.CheckerboardControl.OnScoreChanged += OnScoreChanged;
            GameManager.Instance.CheckerboardControl.OnGameOver += OnGameOver;
        } // end if
        // end if
    } // end OnEnter

    public override void OnExit()
    {
        if (null != GameManager.Instance.CheckerboardControl)
        {
            GameManager.Instance.CheckerboardControl.OnScoreChanged -= OnScoreChanged;
            GameManager.Instance.CheckerboardControl.OnGameOver -= OnGameOver;
        } // end if
        if (null == m_GameObject) return;
        // end if
        Object.Destroy(m_GameObject);
    } // end OnExit

    private void OnClickUndoBtn()
    {
        GameManager.Instance.CheckerboardControl.Undo();
    } // end OnClickUndoBtn

    private void OnClickSaveBtn()
    {
        GameManager.Instance.ExitGame();
        UIManager.Instance.OpenForm(new UISaveGameProgress());
    } // end OnClickSaveBtn

    private void OnClickBackBtn()
    {
        GameManager.Instance.ExitGame();
        UIManager.Instance.OpenForm(new UIStartMenu());
    } // end OnClickBackBtn

    private void OnScoreChanged(object sender, ScoreChangedEventArgs args)
    {
        if (null == args)
        {
            Debug.LogError("UIGameMenu OnScoreChanged args is null");
            return;
        } // end if
        m_ScoreText.text = args.Score.ToString();
    } // end OnScoreChanged

    private void OnGameOver(object sender, System.EventArgs args)
    {
        UIManager.Instance.OpenForm(new UIGameOver());
    } // end OnGameOver
} // end  class UIGameMenu
