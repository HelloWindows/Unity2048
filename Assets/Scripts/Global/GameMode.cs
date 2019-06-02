public enum GameMode
{
    /// <summary>
    /// 交互模式
    /// </summary>
    Interactive = 0,
    /// <summary>
    /// 观看模式
    /// </summary>
    Viewing = 1,
} // end enum GameStatus 

public static partial class Global
{
    public static GameMode GameMode { get; set; }
} // end class Global

