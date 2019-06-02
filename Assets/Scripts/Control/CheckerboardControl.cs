using System;
using UnityEngine;

public class ScoreChangedEventArgs : EventArgs
{
    public int Score { get; private set; }

    public ScoreChangedEventArgs(int score)
    {
        Score = score;
    } // end ScoreChangedEventArgs
} // end class ScoreChangedEventArgs

public class CheckerboardControl : IDisposable
{
    private readonly GameObject m_GameObject;
    private readonly CheckerboardModel m_Model;
    private readonly CheckerboardView m_View;
    private readonly GameRecord m_GameRecord;
    private EventHandler<ScoreChangedEventArgs> m_ScoreChangedEventHandler;

    public CheckerboardControl(GameObject gameObject)
    {
        m_GameObject = gameObject;
        m_View = new CheckerboardView(gameObject.transform);
        m_View.AnimationFinished += OnAnimationFinish;
        m_View.TouchDropEvent += OnTouchDropEvent;
        m_Model = new CheckerboardModel();
        m_View.UpdateView(m_Model);
        m_GameRecord = new GameRecord();
        m_GameRecord.Push(m_Model.NodeMatrix);
        SerializeUtil.DataSaveWithPath(m_GameRecord, PathUtil.CurrentRecordPath);
    } // end CheckerboardControl

    public CheckerboardControl(GameObject gameObject, string recordPath)
    {
        m_GameObject = gameObject;
        m_View = new CheckerboardView(gameObject.transform);
        m_View.AnimationFinished += OnAnimationFinish;
        m_View.TouchDropEvent += OnTouchDropEvent;
        m_Model = new CheckerboardModel();
        m_GameRecord = SerializeUtil.GetDataWithPath<GameRecord>(recordPath);
        if (null == m_GameRecord)
        {
            m_GameRecord = new GameRecord();
            m_GameRecord.Push(m_Model.NodeMatrix);
        }
        else
        {
            Matrix<NodeModel> matrix = m_GameRecord.Current;
            if (null == matrix)
            {
                Debug.LogError("CheckerboardControl game record is error!");
                m_GameRecord = new GameRecord();
                m_GameRecord.Push(m_Model.NodeMatrix);
            }
            else
            {
                m_Model.NodeMatrix = matrix;
            } // end if
        }// end if
        m_View.UpdateView(m_Model);
    } // end CheckerboardControl

    public event EventHandler<ScoreChangedEventArgs> OnScoreChanged
    {
        add
        {
            m_ScoreChangedEventHandler += value;
            if (null != m_ScoreChangedEventHandler)
                m_ScoreChangedEventHandler(this, new ScoreChangedEventArgs(m_Model.Score));
            // end if
        }
        remove
        {
            m_ScoreChangedEventHandler -= value;
        }
    }

    public void Undo()
    {
        Matrix<NodeModel> matrix = m_GameRecord.Previous;
        if (null == matrix)
        {
            Debug.LogWarning("Have not record!");
            return;
        } // end if
        m_Model.NodeMatrix = matrix;
        m_View.UpdateView(m_Model);
        SerializeUtil.DataSaveWithPath(m_GameRecord, PathUtil.CurrentRecordPath);
        if (null != m_ScoreChangedEventHandler)
            m_ScoreChangedEventHandler(this, new ScoreChangedEventArgs(m_Model.Score));
        // end if
    } // end Undo

    public void Update(float elapseSeconds, float realElapseSeconds)
    {
#if UNITY_EDITOR
        if (!Global.Interactable) return;
        // end if
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
#endif
    } // end Update

    private void OnTouchDropEvent(object sender, TouchDropEventArgs args)
    {
        if (!Global.Interactable) return;
        // end if
        Vector3 delta = args.UpPos - args.DownPos;
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
    } // end OnTouchDropEvent

    private void OnAnimationFinish(object sender, EventArgs args)
    {
        if (null != m_ScoreChangedEventHandler)
            m_ScoreChangedEventHandler(this, new ScoreChangedEventArgs(m_Model.Score));
        // end if
        bool isOver = m_Model.CheckGameOver();
        m_View.UpdateView(m_Model);
        m_GameRecord.Push(m_Model.NodeMatrix);
        SerializeUtil.DataSaveWithPath(m_GameRecord, PathUtil.CurrentRecordPath);
        if (isOver) UIManager.Instance.OpenForm(new UIGameOver());
        // end if
    } // end OnAnimationFinish

    public void Dispose()
    {
        m_View.AnimationFinished -= OnAnimationFinish;
        m_View.TouchDropEvent -= OnTouchDropEvent;
        m_View.Dispose();
        m_Model.Dispose();
        m_ScoreChangedEventHandler = null;
        if (null == m_GameObject) return;
        // end if
        UnityEngine.Object.Destroy(m_GameObject);
    } // end Dispose
} // end class CheckerboardControl
