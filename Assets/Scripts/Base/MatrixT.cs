using System;

[Serializable]
public class Matrix<T> where T : new()
{
    private readonly T[,] matrix;

    public T this[int x, int y]
    {
        get
        {
            return matrix[x, y];
        }
        set
        {
            matrix[x, y] = value;
        }
    }
    public int Score { get; set; }
    public NodeMoveDirection Direction { get; set; }

    public Matrix(int row, int column) {
        Score = 0;
        matrix = new T[row, column];
        Direction = NodeMoveDirection.Null;
        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                matrix[x, y] = null == default(T) ? new T() : default(T);
            } // end for
        } // end for
    } // end Matrix
} // end class Matrix<T>
