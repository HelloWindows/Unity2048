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
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        m_GameObject.transform.Find("UndoBtn").GetComponent<Button>().onClick.AddListener(OnClickUndoBtn);
        m_GameObject.transform.Find("SaveBtn").GetComponent<Button>().onClick.AddListener(OnClickSaveBtn);
        m_GameObject.transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);
        m_ScoreText = m_GameObject.transform.Find("ScoreText/Text").GetComponent<Text>();
    } // end OnEnter

    public override void OnExit()
    {
        if (null == m_GameObject) return;
        // end if
        Object.Destroy(m_GameObject);
    } // end OnExit

    public override void OnUpdate()
    {

    } // end OnUpdate

    private void OnClickUndoBtn()
    {
    } // end OnClickUndoBtn

    private void OnClickSaveBtn()
    {
    } // end OnClickSaveBtn

    private void OnClickBackBtn()
    {
        UIManager.Instance.OpenForm(new UIStartMenu());
    } // end OnClickBackBtn
} // end  class UIStartMenu
