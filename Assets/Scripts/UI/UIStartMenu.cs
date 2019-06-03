using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIStartMenu : UIForm {

    private GameObject m_GameObject;
    private const string m_PrefabPath = "UIStartMenu";

    public override void OnEnter()
    {
        Global.GameStatus = GameStatus.Initial;
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        m_GameObject.transform.Find("NewGameBtn").GetComponent<Button>().onClick.AddListener(OnClickNewGame);
        if (File.Exists(PathUtil.CurrentRecordPath))
            m_GameObject.transform.Find("ContinueGameBtn").GetComponent<Button>().onClick.AddListener(OnClickContinueGame);
        else
            m_GameObject.transform.Find("ContinueGameBtn").GetComponent<Button>().interactable = false;
        // end if
        m_GameObject.transform.Find("ReadGameBtn").GetComponent<Button>().onClick.AddListener(OnClickReadGame);
        if (File.Exists(PathUtil.ViewRecordPath))
            m_GameObject.transform.Find("AuthorBtn").GetComponent<Button>().onClick.AddListener(OnClickAuthor);
        else
            m_GameObject.transform.Find("AuthorBtn").GetComponent<Button>().interactable = false;
        // end if
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
    } // end OnClickNewGame

    private void OnClickContinueGame()
    {
        GameManager.Instance.ContinueGame(PathUtil.CurrentRecordPath);
    } // end OnClickContinueGame

    private void OnClickReadGame()
    {
        UIManager.Instance.OpenForm(new UISelectGameProgress());
    } // end OnClickReadGame

    private void OnClickAuthor()
    {
        GameManager.Instance.WatchVideo();
    } // end OnClickAuthor

    private void OnClickExit()
    {
        Application.Quit();
    } // end OnClickExit
} // end  class UIStartMenu
