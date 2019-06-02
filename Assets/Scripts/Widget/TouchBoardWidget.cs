using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDropEventArgs : EventArgs
{
    public Vector3 DownPos { get; private set; }
    public Vector3 UpPos { get; private set; }

    public TouchDropEventArgs(Vector3 downPos, Vector3 upPos)
    {
        DownPos = downPos;
        UpPos = upPos;
    } // end TouchDropEventArgs
} // end class TouchDropEventArgs

public class TouchBoardWidget : MonoBehaviour {

    private Vector3 m_DownPos = Vector3.zero;
    private Vector3 m_UpPos = Vector3.zero;
    private EventHandler<TouchDropEventArgs> m_TouchDropEventHandler;

    public event EventHandler<TouchDropEventArgs> TouchDropEventHandler
    {
        add
        {
            m_TouchDropEventHandler += value;
        }
        remove
        {
            m_TouchDropEventHandler -= value;
        }
    }

    private void OnMouseDown()
    {
        m_DownPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        m_UpPos = Input.mousePosition;
        if (null == m_TouchDropEventHandler) return;
        // end if
        TouchDropEventArgs args = new TouchDropEventArgs(m_DownPos, m_UpPos);
        m_TouchDropEventHandler(this, args);
    }
}
