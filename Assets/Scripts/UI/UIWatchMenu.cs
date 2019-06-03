using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWatchMenu : UIForm
{
    private GameObject m_GameObject;
    private GameObject m_CheckerboardGo;
    private Text m_PlayToggleText;
    private const string m_PrefabPath = "UIWatchMenu";

    private int m_CurrentFrame;
    private readonly List<Matrix<NodeModel>> m_FrameList;

    private Slider m_ProgressSlider;
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
        m_GameObject.transform.Find("PlayToggle").GetComponent<Toggle>().onValueChanged.AddListener(OnClickPlayToggle);
        m_PlayToggleText = m_GameObject.transform.Find("PlayToggle/Label").GetComponent<Text>();
        m_GameObject.transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);

        m_CheckerboardGo = new GameObject("Checkerboard");
        m_CheckerboardGo.transform.SetParent(GameManager.Instance.transform, Vector3.zero, Quaternion.identity, Vector3.one);
        m_CheckerboardView = new CheckerboardView(m_CheckerboardGo.transform);
        m_CheckerboardView.MoveAnimationFinished += OnMoveAnimationFinished;
        m_CheckerboardView.ZoomAnimationFinished += OnZoomAnimationFinished;
        m_CheckerboardView.PlayZoomAnimation(m_FrameList[m_CurrentFrame]);
    } // end OnEnter

    public override void OnExit()
    {
        m_CheckerboardView.MoveAnimationFinished -= OnMoveAnimationFinished;
        m_CheckerboardView.ZoomAnimationFinished -= OnZoomAnimationFinished;
        m_CheckerboardView.Dispose();
        if (null != m_CheckerboardGo) Object.Destroy(m_CheckerboardGo);
        // end if
        if (null != m_GameObject) Object.Destroy(m_GameObject);
        // end if
    } // end OnExit

    private void OnMoveAnimationFinished(object sender, System.EventArgs args)
    {
        if (Global.GameStatus != GameStatus.Operating) return;
        // end if
        m_CurrentFrame++;
        if (m_CurrentFrame >= m_FrameList.Count) return;
        // end if
        m_CheckerboardView.PlayFrameAnimation(m_FrameList[m_CurrentFrame]);
    } // end OnMoveAnimationFinished

    private void OnZoomAnimationFinished(object sender, System.EventArgs args)
    {
        if (Global.GameStatus != GameStatus.Operating) return;
        // end if
        m_CurrentFrame++;
        if (m_CurrentFrame >= m_FrameList.Count) return;
        // end if
        m_CheckerboardView.PlayFrameAnimation(m_FrameList[m_CurrentFrame]);
    } // end OnZoomAnimationFinished


    private void OnClickPlayToggle(bool isOn)
    {
        if (isOn)
        {
            Global.GameStatus = GameStatus.Operating;
            m_PlayToggleText.text = "暂停";
            return;
        } // end if
        m_PlayToggleText.text = "播放";
        Global.GameStatus = GameStatus.Pause;
    } // end OnClickPlayToggle

    private void OnClickBackBtn()
    {
        UIManager.Instance.OpenForm(new UIStartMenu());
    } // end OnClickBackBtn
} // end class UIWatchMenu
