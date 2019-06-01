using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager {

    private UIForm m_CurrentUIForm;
    private readonly Canvas m_Canvas;

    private static UIManager m_UIManager;
    public Canvas Canvas
    {
        get { return m_Canvas; }
    }

    public static UIManager Instance
    {
        get
        {
            if (null == m_UIManager)
                m_UIManager = new UIManager();
            // end if
            return m_UIManager;
        }
    }

    private UIManager()
    {
        m_Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (null == m_Canvas)
            Debug.LogError("UIManager init error!");
        // end if
    } // end UIManager

    public void OpenForm(UIForm uiForm)
    {
        if (null != m_CurrentUIForm) m_CurrentUIForm.OnExit();
        // end if
        m_CurrentUIForm = uiForm;
        if (null != m_CurrentUIForm) m_CurrentUIForm.OnEnter();
        // end if
    } // end OpenForm
} // end class UIManager 
