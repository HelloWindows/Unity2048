using System;
using System.Collections.Generic;

[Serializable]
public class GameRecord {
    private readonly Stack<Matrix<NodeModel>> m_ModelStack;

    public GameRecord()
    {
        m_ModelStack = new Stack<Matrix<NodeModel>>();
    }

    public bool IsEmpty
    {
        get
        {
            return m_ModelStack.Count <= 1;
        }
    }

    public Matrix<NodeModel> CurrentFrame
    {
        get
        {
            if(m_ModelStack.Count > 0)
                return m_ModelStack.Peek();
            // end if
            return null;
        }
    }

    public Matrix<NodeModel> PreviousFrame
    {
        get
        {
            if (m_ModelStack.Count <= 1) return null;
            // end if
            m_ModelStack.Pop();
            return CurrentFrame;
        }
    }

    public void PushFrame(Matrix<NodeModel> model)
    {
        if (null == model) return;
        // end if
        Matrix<NodeModel> copy = CopyUtil.DeepCopyByBin(model);
        m_ModelStack.Push(copy);
    } // end PushFrame
} // end class GameRecord 
