using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour {

    private static GameManager m_GameManager;
    private CheckerboardControl m_CheckerboardControl;

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

    private void Awake()
    {
        m_GameManager = this;
    }

    // Use this for initialization
    void Start () 
	{
        UIManager.Instance.OpenForm(new UIStartMenu());
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (null == m_CheckerboardControl) return;
        // end if
        m_CheckerboardControl.Update(Time.deltaTime, Time.unscaledDeltaTime);
    }

    public void NewGame()
    {
        m_CheckerboardControl = new CheckerboardControl(transform);
    }

    public void ExitGame()
    {

    }

}
