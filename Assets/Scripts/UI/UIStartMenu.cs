using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartMenu : UIForm {

    private GameObject m_GameObject;
    private const string m_PrefabPath = "UIStartMenu";

    public override void OnEnter()
    {
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        m_GameObject.transform.Find("NewGameBtn").GetComponent<Button>().onClick.AddListener(OnClickNewGame);
        m_GameObject.transform.Find("ContinueGameBtn").GetComponent<Button>().onClick.AddListener(OnClickContinueGame);
        m_GameObject.transform.Find("ReadGameBtn").GetComponent<Button>().onClick.AddListener(OnClickReadGame);
        m_GameObject.transform.Find("AuthorBtn").GetComponent<Button>().onClick.AddListener(OnClickAuthor);
        m_GameObject.transform.Find("ExitBtn").GetComponent<Button>().onClick.AddListener(OnClickExit);
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

    private void OnClickNewGame()
    {
        GameManager.Instance.NewGame();
        UIManager.Instance.OpenForm(new UIGameMenu());
    } // end OnClickNewGame

    private void OnClickContinueGame()
    {
    } // end OnClickContinueGame

    private void OnClickReadGame()
    {
    } // end OnClickReadGame

    private void OnClickAuthor()
    {
    } // end OnClickAuthor

    private void OnClickExit()
    {
        Application.Quit();
    } // end OnClickExit
} // end  class UIStartMenu
