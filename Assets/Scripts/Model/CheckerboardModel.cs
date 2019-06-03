using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerboardModel : System.IDisposable
{
    private Matrix<NodeModel> m_NodeMatrix;

    public CheckerboardModel()
    {
        m_NodeMatrix = new Matrix<NodeModel>(ValueUtil.GridRow, ValueUtil.GridColumn);
        PushNewNumber();
        PushNewNumber();
    } // end CheckerboardModel

    public Matrix<NodeModel> NodeMatrix
    {
        get
        {
            return CopyUtil.DeepCopyByBin(m_NodeMatrix);
        }
        set
        {
            if (null == value) return;
            // end if
            Matrix<NodeModel> matrix = CopyUtil.DeepCopyByBin(value);
            if (null == matrix) return;
            // end if
            m_NodeMatrix = matrix;
        }
    }

    public int Score { get { return m_NodeMatrix.Score; } }

    public NodeModel GetNodeModel(int x, int y)
    {
        return m_NodeMatrix[x, y];
    } // end GetNodeModel


    public void PushNewNumber() 
	{
		List<int> emptyList = new List<int>();
		for (int i = 0; i < ValueUtil.GridRow; i++)
		{
			for (int j = 0; j < ValueUtil.GridColumn; j++)
			{
                if (0 != m_NodeMatrix[i, j].Number) continue;
				// end if
				emptyList.Add(i * ValueUtil.GridColumn + j);
			} // end for
		} // end for
		if(0 == emptyList.Count) return;
		// end if
		int number = Random.Range(0, 2) == 0 ? 2 : 4;
		int index = emptyList[Random.Range(0, emptyList.Count)];
		int x = index / ValueUtil.GridColumn;
		int y = index % ValueUtil.GridColumn;
        NodeModel node = m_NodeMatrix[x, y];
        node.Zoom();
        node.SetNumber(number);
	} // end PushNewNumber

    public void ToUp()
    {
        m_NodeMatrix.Direction = NodeMoveDirection.Up;
        NodeModel[] nodeArr = new NodeModel[ValueUtil.GridRow];
        for (int x = 0; x < ValueUtil.GridColumn; x++)
        {
            for (int y = 0; y < ValueUtil.GridRow; y++)
            {
                nodeArr[y] = m_NodeMatrix[y, x];
                nodeArr[y].Reset();
            } // end for
            ProcessNodeGroup(nodeArr);
        } // end for
    } // end ToUp

    public void ToDown()
    {
        int index = 0;
        m_NodeMatrix.Direction = NodeMoveDirection.Down;
        NodeModel[] nodeArr = new NodeModel[ValueUtil.GridRow];
        for (int x = 0; x < ValueUtil.GridColumn; x++)
        {
            index = 0;
            for (int y = ValueUtil.GridRow - 1; y >= 0; y--)
            {
                nodeArr[index] = m_NodeMatrix[y, x];
                nodeArr[index].Reset();
                index++;
            } // end for
            ProcessNodeGroup(nodeArr);
        } // end for
    } // end ToDown

    public void ToLeft()
    {
        m_NodeMatrix.Direction = NodeMoveDirection.Left;
        NodeModel[] nodeArr = new NodeModel[ValueUtil.GridColumn];
        for (int x = 0; x < ValueUtil.GridRow; x++)
        {
            for (int y = 0; y < ValueUtil.GridColumn; y++)
            {
                nodeArr[y] = m_NodeMatrix[x, y];
                nodeArr[y].Reset();
            } // end for
            ProcessNodeGroup(nodeArr);
        } // end for
    } // end ToLeft

    public void ToRight()
    {
        int index = 0;
        m_NodeMatrix.Direction = NodeMoveDirection.Right;
        NodeModel[] nodeArr = new NodeModel[ValueUtil.GridColumn];
        for (int x = 0; x < ValueUtil.GridRow; x++)
        {
            index = 0;
            for (int y = ValueUtil.GridColumn - 1; y >= 0; y--)
            {
                nodeArr[index] = m_NodeMatrix[x, y];
                nodeArr[index].Reset();
                index++;
            } // end for
            ProcessNodeGroup(nodeArr);
        } // end for
    } // end ToRight

    private void ProcessNodeGroup(NodeModel[] nodeArr)
	{
		if(null == nodeArr || nodeArr.Length < 1) 
		{
			Debug.LogError("ProcessNodeGroup nodeArr is error!");
			return;
		} // end if

		for (int i = 1; i < nodeArr.Length; i++)
		{	
			if(0 == nodeArr[i].Number) continue;
            // end if
            int frameCount = 0;
            for (int j = i - 1; j >= 0; j--)
			{
                if (nodeArr[j].Number == 0)
                {
                    nodeArr[j].SetNumber(nodeArr[j + 1].Number);
                    nodeArr[j + 1].SetZero();
                    nodeArr[i].Move();
                }
                else if (nodeArr[j].Number == nodeArr[j + 1].Number && !nodeArr[j].IsMerge && !nodeArr[j + 1].IsMerge)
                {
                    m_NodeMatrix.Score += nodeArr[j].Number;
                    nodeArr[j].Merge();
                    nodeArr[j + 1].SetZero();
                    nodeArr[j].Zoom();
                    nodeArr[i].Move();
                } // end if
                frameCount++;
            } // end for
		} // end for
	} // end ProcessNodeGroup

    public bool IsGameOver()
    {
        int row = ValueUtil.GridRow;
        int column = ValueUtil.GridColumn - 1;
        for (int i = 0; i < row; i++)
        {
            if (0 == m_NodeMatrix[i, column].Number) return false;
            // end if
            for (int j = 0; j < column; j++)
            {
                if (0 == m_NodeMatrix[i, j].Number) return false;
                // end if
                if (m_NodeMatrix[i, j].Number == m_NodeMatrix[i, j + 1].Number) return false;
                // end if
                if (m_NodeMatrix[j, i].Number == m_NodeMatrix[j + 1, i].Number) return false;
                // end if
            } // end for
        } // end for
        return true;
    } // end IsGameOver

    public void Log()
	{
		string str = "";
		for (int i = 0; i < ValueUtil.GridRow; i++)
		{
			for (int j = 0; j < ValueUtil.GridColumn; j++)
			{
				str += m_NodeMatrix[i, j].Number + " ";
			}
			str += '\n';
		}
		Debug.Log(str);
	}

    public void Dispose()
    {
    } // end Dispose
} // end CheckerboardModel
