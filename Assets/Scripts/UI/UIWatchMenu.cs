using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWatchMenu : UIForm
{
    private GameObject m_GameObject;
    private GameObject m_CheckerboardGo;
    private Text m_PlayToggleText;
    private Text m_ScoreText;
    private Slider m_ProgressSlider;
    private const string m_PrefabPath = "UIWatchMenu";

    private int m_CurrentFrame;
    private readonly List<Matrix<NodeModel>> m_FrameList;
    private CheckerboardView m_CheckerboardView;

    public UIWatchMenu()
    {
        m_FrameList = new List<Matrix<NodeModel>>();
    } // end UIWatchMenu

    public override void OnEnter()
    {
        m_CurrentFrame = 0;
        Global.GameStatus = GameStatus.Operating;
        GameRecord gameRecord = SerializeUtil.GetDataWithPath<GameRecord>(PathUtil.ViewRecordPath);
        if (null == gameRecord || gameRecord.IsEmpty)
        {
            Debug.LogError("PathUtil.ViewRecordPath get record is null or empty!");
            GameManager.Instance.ExitGame();
            UIManager.Instance.OpenForm(new UIStartMenu());
            return;
        } // end if
        m_FrameList.Add(gameRecord.CurrentFrame);
        while (!gameRecord.IsEmpty)
        {
            m_FrameList.Add(gameRecord.PreviousFrame);
        } // end while
        m_FrameList.Reverse();
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        m_ScoreText = m_GameObject.transform.Find("ScoreText/Text").GetComponent<Text>();
        m_ScoreText.text = "0";
        m_PlayToggleText = m_GameObject.transform.Find("PlayToggle/Label").GetComponent<Text>();
        m_ProgressSlider = m_GameObject.transform.Find("ProgressSlider").GetComponent<Slider>();
        m_ProgressSlider.minValue = 0;
        m_ProgressSlider.maxValue = m_FrameList.Count - 1;
        m_ProgressSlider.interactable = false;
        m_GameObject.transform.Find("PlayToggle").GetComponent<Toggle>().onValueChanged.AddListener(OnClickPlayToggle);
        m_GameObject.transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);

        m_CheckerboardGo = new GameObject("Checkerboard");
        m_CheckerboardGo.transform.SetParent(GameManager.Instance.transform, Vector3.zero, Quaternion.identity, Vector3.one);
        m_CheckerboardView = new CheckerboardView(m_CheckerboardGo.transform);
        m_CheckerboardView.MoveAnimationFinished += OnFrameAnimationFinished;
        m_CheckerboardView.ZoomAnimationFinished += OnFrameAnimationFinished;
        m_CheckerboardView.PlayZoomAnimation(m_FrameList[m_CurrentFrame]);
    } // end OnEnter

    public override void OnExit()
    {
        m_CheckerboardView.MoveAnimationFinished -= OnFrameAnimationFinished;
        m_CheckerboardView.ZoomAnimationFinished -= OnFrameAnimationFinished;
        m_CheckerboardView.Dispose();
        if (null != m_CheckerboardGo) Object.Destroy(m_CheckerboardGo);
        // end if
        if (null != m_GameObject) Object.Destroy(m_GameObject);
        // end if
    } // end OnExit

    private void OnFrameAnimationFinished(object sender, System.EventArgs args)
    {
        m_ScoreText.text = m_FrameList[m_CurrentFrame].Score.ToString();
        if (Global.GameStatus != GameStatus.Operating) return;
        // end if
        m_CurrentFrame++;
        m_ProgressSlider.value = m_CurrentFrame;
        if (m_CurrentFrame >= m_FrameList.Count) return;
        // end if
        m_CheckerboardView.PlayFrameAnimation(m_FrameList[m_CurrentFrame]);
    } // end OnFrameAnimationFinished

    private void OnClickPlayToggle(bool isOn)
    {
        if (isOn)
        {
            Global.GameStatus = GameStatus.Operating;
            m_PlayToggleText.text = "暂停";
            if (m_CheckerboardView.IsPlayingAnimation) return;
            // end if
            m_CurrentFrame++;
            if (m_CurrentFrame >= m_FrameList.Count) return;
            // end if
            m_CheckerboardView.PlayFrameAnimation(m_FrameList[m_CurrentFrame]);
        }
        else
        {
            m_PlayToggleText.text = "播放";
            Global.GameStatus = GameStatus.Pause;
        }// end if
    } // end OnClickPlayToggle

    private void OnClickBackBtn()
    {
        UIManager.Instance.OpenForm(new UIStartMenu());
    } // end OnClickBackBtn
} // end class UIWatchMenu
