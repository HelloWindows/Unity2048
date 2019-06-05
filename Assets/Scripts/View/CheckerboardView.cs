using System;
using UnityEngine;

public class CheckerboardView : IDisposable
{
    private int m_GridSign;
    private readonly int m_MaxGridSign;
    private readonly NodeView[,] m_NodeMatrix;
    private SpriteRenderer m_BgSprite;
    private EventHandler m_MoveAnimationFinishedEventHandler = null;
    private EventHandler m_ZoomAnimationFinishedEventHandler = null;
    private readonly TouchBoardWidget m_TouchBoardWidget;

    public CheckerboardView(Transform parent)
    {
        m_BgSprite = new GameObject("Checkerboard").AddComponent<SpriteRenderer>();
        m_BgSprite.transform.SetParent(parent, Vector3.zero, Quaternion.identity, Vector3.one);
        m_BgSprite.sprite = SpriteUtil.BgSprite;
        m_BgSprite.sortingOrder = -1;
        m_BgSprite.gameObject.AddComponent<BoxCollider2D>();
        m_TouchBoardWidget = m_BgSprite.gameObject.AddComponent<TouchBoardWidget>();
        Transform nodeParent = new GameObject("nodeMatrix").transform;
        nodeParent.SetParent(parent, Vector3.zero, Quaternion.identity, Vector3.one);
        m_NodeMatrix = new NodeView[ValueUtil.GridRow, ValueUtil.GridColumn];
        m_MaxGridSign = ValueUtil.GridRow * ValueUtil.GridColumn;

        float x = ValueUtil.GridInitX;
        float y = ValueUtil.GridInitY;
        for (int i = 0; i < ValueUtil.GridRow; i++)
        {
            y = ValueUtil.GridInitY - ValueUtil.GridOffset * i;
            for (int j = 0; j < ValueUtil.GridColumn; j++)
            {
                x = ValueUtil.GridInitX + ValueUtil.GridOffset * j;
                Transform nodeTrans = new GameObject("Node_" + i + "_" + j).transform;
                nodeTrans.SetParent(nodeParent, new Vector3(x, y), Quaternion.identity, Vector3.one);
                NodeView nodeView = nodeTrans.gameObject.AddComponent<NodeView>();
                nodeView.AnimationMoveFinished += OnGridMoveFinished;
                nodeView.AnimationZoomFinished += OnGridZoomFinish;
                m_NodeMatrix[i, j] = nodeView;
            } // end for
        } // end for
    } // end CheckerboardView

    public bool IsPlayingAnimation { get; private set; }

    public event EventHandler MoveAnimationFinished
    {
        add
        {
            m_MoveAnimationFinishedEventHandler += value;
        }
        remove
        {
            m_MoveAnimationFinishedEventHandler -= value;
        }
    }

    public event EventHandler ZoomAnimationFinished
    {
        add
        {
            m_ZoomAnimationFinishedEventHandler += value;
        }
        remove
        {
            m_ZoomAnimationFinishedEventHandler -= value;
        }
    }

    public event EventHandler<TouchDropEventArgs> TouchDropEvent
    {
        add
        {
            m_TouchBoardWidget.TouchDropEventHandler += value;
        }
        remove
        {
            m_TouchBoardWidget.TouchDropEventHandler -= value;
        }
    }

    public void PlayFrameAnimation(Matrix<NodeModel> matrix)
    {
        PlayMoveAnimation(matrix);
        PlayZoomAnimation(matrix);
    } // end PlayFrameAnimation

    public void PlayMoveAnimation(Matrix<NodeModel> matrix)
    {
        if (IsPlayingAnimation) {
            return;
        } // end if
        Vector3 direction = matrix.Direction.ToVector3();
        if (direction.Equals(Vector3.zero)) return;
        // end if
        m_GridSign = 0;
        for (int i = 0; i < ValueUtil.GridRow; i++)
        {
            for (int j = 0; j < ValueUtil.GridColumn; j++)
            {
                int step = matrix[i, j].MoveStep;
                if (step < 1)
                {
                    m_GridSign++;
                    continue;
                } // end if
                m_NodeMatrix[i, j].ToMove(direction, step);
            } // end for
        } // end for
        if (m_GridSign < m_MaxGridSign) IsPlayingAnimation = true;
        // end if
    } // end PlayMoveAnimation

    public void PlayZoomAnimation(Matrix<NodeModel> matrix)
    {
        if (IsPlayingAnimation)
        {
            return;
        } // end if
        Vector3 direction = matrix.Direction.ToVector3();
        if (!direction.Equals(Vector3.zero)) return;
        // end if
        float x = ValueUtil.GridInitX;
        float y = ValueUtil.GridInitY;
        m_GridSign = 0;
        NodeView nodeView = null;
        NodeModel nodeModel = null;
        for (int i = 0; i < ValueUtil.GridRow; i++)
        {
            y = ValueUtil.GridInitY - ValueUtil.GridOffset * i;
            for (int j = 0; j < ValueUtil.GridColumn; j++)
            {
                x = ValueUtil.GridInitX + ValueUtil.GridOffset * j;
                nodeModel = matrix[i, j];
                nodeView = m_NodeMatrix[i, j];
                nodeView.transform.localPosition = new Vector3(x, y);
                nodeView.transform.localScale = Vector3.one;
                nodeView.Sprite = SpriteUtil.GetSprite(nodeModel.Number);
                if (false == nodeModel.IsZoom)
                {
                    m_GridSign++;
                    continue;
                } // end if
                nodeView.ToZoom();
            } // end for
        } // end for
        if (m_GridSign < m_MaxGridSign) IsPlayingAnimation = true;
        // end if
    } // end PlayZoomAnimation

    private void OnGridMoveFinished(object sender, EventArgs args)  
    {
        m_GridSign++;
        if (m_GridSign < m_MaxGridSign) return;
        // end if
        m_GridSign = 0;
        IsPlayingAnimation = false;
        if (null != m_MoveAnimationFinishedEventHandler)
            m_MoveAnimationFinishedEventHandler(this, EventArgs.Empty);
        // end if
    } // end OnGridMoveFinished

    private void OnGridZoomFinish(object sender, EventArgs args)
    {
        m_GridSign++;
        if (m_GridSign < m_MaxGridSign) return;
        // end if
        m_GridSign = 0;
        IsPlayingAnimation = false;
        if (null != m_ZoomAnimationFinishedEventHandler)
            m_ZoomAnimationFinishedEventHandler(this, EventArgs.Empty);
        // end if
    } // end OnGridZoomFinish

    public void Dispose()
    {
        m_MoveAnimationFinishedEventHandler = null;
        m_ZoomAnimationFinishedEventHandler = null;
        foreach (NodeView node in m_NodeMatrix)
        {
            node.AnimationMoveFinished -= OnGridMoveFinished;
        } // end foreach
    } // end Dispose
} // end class CheckerboardView
