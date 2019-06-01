using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeModel {

    public NodeModel()
	{
		Number = 0;
		IsMerge = false;
    } // end NodeModel
    public int Number { get; private set; }
    public int MoveStep { get; private set; }
    public bool IsMerge { get; private set; }
    public bool IsZoom { get; private set; }

    public void Move()
    {
        MoveStep++;
    } // end Move

    public void Zoom()
    {
        IsZoom = true;
    }

    public void Reset()
    {
        MoveStep = 0;
        IsZoom = false;
        IsMerge = false;
    } // end Reset

    public void SetZero()
	{
		Number = 0;
    } // end SetZero

    public void SetNumber(int number)
    {
        Number = number;
    }

    public void Merge()
	{
		IsMerge = true;
		Number = Number * 2;
    } // end Merge
} // end class NodeModel
