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
    }

    public void ToShow(NodeModel nodeModel)
    {
        if (null == nodeModel)
        {
            Debug.LogError("ToShow model is null!");
            m_Sprite.sprite = null;
            return;
        }
        m_Sprite.sprite = SpriteUtil.GetSprite(nodeModel.Number);
        if (false == nodeModel.IsZoom) return;
        // end if
        m_ZoomAnim.Play();
    }
}
