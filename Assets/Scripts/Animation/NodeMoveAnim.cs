using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMoveAnim : MonoBehaviour {

    private float m_Speed;
    private Vector3 m_Target;
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

    public void Awake()
    {
        m_Speed = ValueUtil.GridSpeed;
        enabled = false;
    } // end Awake

    public void Play(Vector3 target)
    {
        m_Target = target;
        enabled = true;
    } // end Play

	private void Update () {
        if (transform.localPosition != m_Target) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, m_Target, m_Speed);
        }
        else
        {
            if (null != m_AnimationFinishedEventHandler)
                m_AnimationFinishedEventHandler(this, null);
            // end if
            enabled = false;
        } // end if
	} // end Update
} // end class NodeMoveAnim 
