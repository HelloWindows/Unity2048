using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour {
    private static GameManager m_GameManager;

    public static GameManager Instance
    {
        get
        {
            if (null == m_GameManager)
                m_GameManager = new GameObject("GameManager").AddComponent<GameManager>();
            // end if
            return m_GameManager;
        }
    }

    public CheckerboardControl CheckerboardControl { get; private set; }

    private void Awake()
    {
        m_GameManager = this;
    }

    // Use this for initialization
    void Start () 
	{
        UIManager.Instance.OpenForm(new UIStartMenu());
    } // end Start
	
	// Update is called once per frame
	void Update () 
	{
        if (null == CheckerboardControl) return;
        // end if
        CheckerboardControl.Update(Time.deltaTime, Time.unscaledDeltaTime);
    } // end Update

    public void NewGame()
    {
        Global.GameMode = GameMode.Interactive;
        GameObject checkerboardGo = new GameObject("Checkerboard");
        checkerboardGo.transform.SetParent(transform, Vector3.zero, Quaternion.identity, Vector3.one);
        CheckerboardControl = new CheckerboardControl(checkerboardGo);
        UIManager.Instance.OpenForm(new UIGameMenu());
    } // end NewGame

    public void ContinueGame(string recordPath)
    {
        Global.GameMode = GameMode.Interactive;
        GameObject checkerboardGo = new GameObject("Checkerboard");
        checkerboardGo.transform.SetParent(transform, Vector3.zero, Quaternion.identity, Vector3.one);
        CheckerboardControl = new CheckerboardControl(checkerboardGo, recordPath);
        UIManager.Instance.OpenForm(new UIGameMenu());
    } // end ContinueGame

    public void ExitGame()
    {
        if (null == CheckerboardControl) return;
        // end if
        CheckerboardControl.Dispose();
        CheckerboardControl = null;
    } // end ExitGame
} // end class GameManager
