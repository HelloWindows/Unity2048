using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerboardModel 
{
    private readonly NodeModel[,] m_NodeMatrix;

	public CheckerboardModel() 
	{
        Direction = Vector3.zero;
        m_NodeMatrix = new NodeModel[4, 4];
        for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
				m_NodeMatrix[x, y] = new NodeModel();
            } // end for
		} // end for
	} // end CheckerboardModel

    public Vector3 Direction { get; private set; }

    public void Init() 
	{
		PushNewNumber();
        PushNewNumber();
    } // end Init

    public NodeModel GetNodeModel(int x, int y)
    {
        return m_NodeMatrix[x, y];
    } // end GetNodeModel


    private void PushNewNumber() 
	{
		List<int> emptyList = new List<int>();
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
                if (0 != m_NodeMatrix[i, j].Number) continue;
				// end if
				emptyList.Add(i * 4 + j);
			} // end for
		} // end for
		if(0 == emptyList.Count) return;
		// end if
		int number = Random.Range(0, 2) == 0 ? 2 : 4;
		int index = emptyList[Random.Range(0, emptyList.Count)];
		int x = index / 4;
		int y = index % 4;
        NodeModel node = m_NodeMatrix[x, y];
        node.Zoom();
        node.SetNumber(number);
	} // end PushNewNumber

    public void ToUp()
    {
        Direction = Vector3.up;
        NodeModel[] nodeArr = new NodeModel[4];
        for (int x = 0; x < nodeArr.Length; x++)
        {
            for (int y = 0; y < nodeArr.Length; y++)
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
        Direction = Vector3.down;
        NodeModel[] nodeArr = new NodeModel[4];
        for (int x = 0; x < nodeArr.Length; x++)
        {
            index = 0;
            for (int y = 3; y >= 0; y--)
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
        Direction = Vector3.left;
        NodeModel[] nodeArr = new NodeModel[4];
        for (int x = 0; x < nodeArr.Length; x++)
        {
            for (int y = 0; y < nodeArr.Length; y++)
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
        Direction = Vector3.right;
        NodeModel[] nodeArr = new NodeModel[4];
        for (int x = 0; x < nodeArr.Length; x++)
        {
            index = 0;
            for (int y = 3; y >= 0; y--)
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
                    nodeArr[j].Merge();
                    nodeArr[j + 1].SetZero();
                    nodeArr[j].Zoom();
                    nodeArr[i].Move();
                } // end if
                frameCount++;
            } // end for
		} // end for
	} // end ProcessNodeGroup

    public bool CheckGameOver()
    {
        PushNewNumber();
        for (int i = 0; i < 4; i++)
        {
            if (0 == m_NodeMatrix[i, 3].Number) return false;
            // end if
            for (int j = 0; j < 3; j++)
            {
                if (0 == m_NodeMatrix[i, j].Number) return false;
                // end if
                if (m_NodeMatrix[i, j].Number == m_NodeMatrix[i, j + 1].Number) return false;
                // end if
                if (m_NodeMatrix[j, i].Number == m_NodeMatrix[j + 1, i].Number) return false;
                // end if
            } // end for
        } // end for
        Debug.Log("GameOver");
        return true;
    } // end CheckGameOver

    public void Log()
	{
		string str = "";
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				str += m_NodeMatrix[i, j].Number + " ";
			}
			str += '\n';
		}
		Debug.Log(str);
	}
} // end CheckerboardModel
