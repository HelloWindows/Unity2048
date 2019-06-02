using System;

[Serializable]
public class Matrix<T>
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

    public Matrix(int row, int column) {
        matrix = new T[row, column];
    } // end Matrix
} // end class Matrix<T>
