using System;
using UnityEngine;

public class CheckerboardView : IDisposable
{
    private int m_GridSign;
    private readonly int m_MaxGridSign;
    private readonly NodeView[,] m_NodeMatrix;
    private SpriteRenderer m_BgSprite;
    private EventHandler m_AnimationFinishedEventHandler = null;
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
                m_NodeMatrix[i, j] = nodeView;
            } // end for
        } // end for
    } // end Awake

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

    public void PlayAnimation(CheckerboardModel model)
    {
        m_GridSign = 0;
        Vector3 direction = model.Direction;
        for (int i = 0; i < ValueUtil.GridRow; i++)
        {
            for (int j = 0; j < ValueUtil.GridColumn; j++)
            {
                int step = model.GetNodeModel(i, j).MoveStep;
                if (step < 1)
                {
                    m_GridSign++;
                    continue;
                } // end if
                m_NodeMatrix[i, j].ToMove(direction, step);
            } // end for
        } // end for
    } // end PlayAnimation

    public void UpdateView(CheckerboardModel model)
    {
        float x = ValueUtil.GridInitX;
        float y = ValueUtil.GridInitY;
        NodeView nodeView;
        for (int i = 0; i < ValueUtil.GridRow; i++)
        {
            y = ValueUtil.GridInitY - ValueUtil.GridOffset * i;
            for (int j = 0; j < ValueUtil.GridColumn; j++)
            {
                x = ValueUtil.GridInitX + ValueUtil.GridOffset * j;
                nodeView = m_NodeMatrix[i, j];
                nodeView.transform.localPosition = new Vector3(x, y);
                nodeView.transform.localScale = Vector3.one;
                nodeView.ToShow(model.GetNodeModel(i, j));
            } // end for
        } // end for
    } // end UpdateView

    private void OnGridMoveFinished(object sender, EventArgs args)  
    {
        m_GridSign++;
        if (m_GridSign < m_MaxGridSign) return;
        // end if
        if (null != m_AnimationFinishedEventHandler)
            m_AnimationFinishedEventHandler(this, EventArgs.Empty);
        // end if
    } // end OnGridMoveFinished

    public void Dispose()
    {
        m_AnimationFinishedEventHandler = null;
        foreach (NodeView node in m_NodeMatrix)
        {
            node.AnimationMoveFinished -= OnGridMoveFinished;
        } // end foreach
    } // end Dispose
} // end class CheckerboardView
