using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : UIForm
{
    private GameObject m_GameObject;
    private const string m_PrefabPath = "UIGameOver";

    public override void OnEnter()
    {
        Global.GameStatus = GameStatus.Pause;
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        m_GameObject.transform.Find("ContinueBtn").GetComponent<Button>().onClick.AddListener(OnClickContinueBtn);
        m_GameObject.transform.Find("ReplayBtn").GetComponent<Button>().onClick.AddListener(OnClickReplayBtn);
        m_GameObject.transform.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);
    } // end OnEnter

    public override void OnExit()
    {
        if (null == m_GameObject) return;
        // end if
        Object.Destroy(m_GameObject);
    } // end OnExit

    private void OnClickContinueBtn()
    {
        GameManager.Instance.CheckerboardControl.Undo();
        UIManager.Instance.OpenForm(new UIGameMenu());
    } // end OnClickContinueBtn

    private void OnClickReplayBtn()
    {
        GameManager.Instance.ExitGame();
        GameManager.Instance.NewGame();
    } // end OnClickReplayBtn

    private void OnClickBackBtn()
    {
        GameManager.Instance.ExitGame();
        File.Delete(PathUtil.CurrentRecordPath);
        UIManager.Instance.OpenForm(new UIStartMenu());
    } // end OnClickBackBtn
} // end  class UIGameOver
