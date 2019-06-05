using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeZoomAnim : MonoBehaviour {

    private float smooth;
    private readonly float m_ZoomInSize_1 = 1.2f;
    private readonly float m_ZoomInSize_2 = 1.1f;
    private readonly float m_ZoomOutSize = 1f;
    private EventHandler m_AnimationFinishedEventHandler = null;

    public event EventHandler AnimationFinished
    {
        add
        {
            m_AnimationFinishedEventHandler += value;
        }
        remove
        {
            m_AnimationFinishedEventHandler -= value;
        }
    }

    public float Speed { get; private set; }

    public void Awake()
    {
        enabled = false;
    } // end Awake

    public void Play()
    {
        smooth = 0;
        enabled = true;
    } // end Play

    private void Update()
    {
        smooth += Time.deltaTime * 3f;
        if (ZoomIn_1()) return;
        // end if
        if (ZoomOut_1()) return;
        // end if
        if (ZoomIn_2()) return;
        // end if
        if (ZoomOut_2()) return;
        // end if
        if (null != m_AnimationFinishedEventHandler)
            m_AnimationFinishedEventHandler(this, EventArgs.Empty);
        // end if
        enabled = false;
    } // end Update

    private bool ZoomIn_1()
    {
        if (smooth > 0.4) return false;
        // end if
        transform.localScale = Mathf.Lerp(m_ZoomOutSize, m_ZoomInSize_1, smooth * 2.6f) * Vector3.one;
        return true;
    }

    private bool ZoomOut_1()
    {
        if (smooth > 0.6) return false;
        // end if
        transform.localScale = Mathf.Lerp(m_ZoomInSize_1, m_ZoomOutSize, (smooth - 0.4f) * 5.1f) * Vector3.one;
        return true;
    }

    private bool ZoomIn_2()
    {
        if (smooth > 0.8) return false;
        // end if
        transform.localScale = Mathf.Lerp(m_ZoomOutSize, m_ZoomInSize_2, (smooth - 0.6f) * 5.1f) * Vector3.one;
        return true;
    }

    private bool ZoomOut_2()
    {
        if (smooth > 1) return false;
        // end if
        transform.localScale = Mathf.Lerp(m_ZoomInSize_2, m_ZoomOutSize, (smooth - 0.8f) * 5.1f) * Vector3.one;
        return true;
    }
} // end class NodeZoomAnim 
