using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    private SpriteRenderer m_Sprite;
    private NodeMoveAnim m_MoveAnim;
    private NodeZoomAnim m_ZoomAnim;

    private void Awake()
    {
        m_Sprite = gameObject.AddComponent<SpriteRenderer>();
        m_MoveAnim = gameObject.AddComponent<NodeMoveAnim>();
        m_ZoomAnim = gameObject.AddComponent<NodeZoomAnim>();
    } // end Awake

    public Sprite Sprite
    {
        set
        {
            m_Sprite.sprite = value;
        }
    }

    public event EventHandler AnimationMoveFinished
    {
        add
        {
            m_MoveAnim.AnimationFinished += value;
        }
        remove
        {
            m_MoveAnim.AnimationFinished -= value;
        }
    }

    public event EventHandler AnimationZoomFinished
    {
        add
        {
            m_ZoomAnim.AnimationFinished += value;
        }
        remove
        {
            m_ZoomAnim.AnimationFinished -= value;
        }
    }

    public void ToMove(Vector3 direction, int step)
    {
        if (step < 1) return;
        // end if
        Vector3 target = transform.localPosition + direction * step * ValueUtil.GridOffset;
        m_MoveAnim.Play(target);
    } // end ToMove

    public void ToZoom()
    {
        m_ZoomAnim.Play();
    } // end ToZoom
} // end class  
