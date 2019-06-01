using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerboardControl
{
    private Vector3 m_StartPos;
    private Vector3 m_EndPos;
    private readonly CheckerboardModel m_Model;
    private readonly CheckerboardView m_View;

    public CheckerboardControl(Transform parent)
    {
        m_View = new CheckerboardView(parent);
        m_View.AnimationFinished += OnAnimationFinish;
        m_Model = new CheckerboardModel();
        m_Model.Init();
        m_View.UpdateView(m_Model);
    }

    public void Update(float elapseSeconds, float realElapseSeconds)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Model.ToUp();
            m_View.PlayAnimation(m_Model);
        } // end if

        if (Input.GetKeyDown(KeyCode.S))
        {
            m_Model.ToDown();
            m_View.PlayAnimation(m_Model);
        } // end if

        if (Input.GetKeyDown(KeyCode.A))
        {
            m_Model.ToLeft();
            m_View.PlayAnimation(m_Model);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            m_Model.ToRight();
            m_View.PlayAnimation(m_Model);
        } // end if

        if (Input.GetMouseButtonDown(0))
        {
            m_StartPos = Input.mousePosition;
        } // end if

        if (Input.GetMouseButtonUp(0)) {
            m_EndPos = Input.mousePosition;
            ProcessTouch();
        } // end if
    } // end Update

    private void ProcessTouch()
    {
        Vector3 delta = m_EndPos - m_StartPos;
        if (delta == Vector3.zero) return;
        // end if
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                m_Model.ToRight();
                m_View.PlayAnimation(m_Model);
                return;
            } // end if
            m_Model.ToLeft();
            m_View.PlayAnimation(m_Model);
        }
        else
        {
            if (delta.y > 0)
            {
                m_Model.ToUp();
                m_View.PlayAnimation(m_Model);
                return;
            } // end if
            m_Model.ToDown();
            m_View.PlayAnimation(m_Model);
        } // end if
    } // end ProcessTouch

    private void OnAnimationFinish(object sender, EventArgs args)
    {
        m_Model.CheckGameOver();
        m_View.UpdateView(m_Model);
    } // end OnAnimationFinish
} // end class CheckerboardControl
