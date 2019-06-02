using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    /// <summary>
    /// 初始状态
    /// </summary>
    Initial = 0,
    /// <summary>
    /// 运行状态
    /// </summary>
    Operating = 1,
    /// <summary>
    /// 暂停状态
    /// </summary>
    Pause = 2,
    /// <summary>
    /// 终止状态
    /// </summary>
    Termination = 3
} // end enum GameStatus 

public static partial class Global
{
    public static GameStatus GameStatus { get; set; }
} // end class Global
