using System.Globalization;
using Game.Service.Sudo.Models;

namespace Game.Service.Sudo.Common;

public static class Extension
{
    public static IEnumerable<T> SliceRow<T>(this T[,] array, int row)
    {
        for (int col = 0; col < 9; col++)
        {
            yield return array[row,col];
        }
    }

    public static IEnumerable<T> SliceColumn<T>(this T[,] array, int column)
    {
        for (int row = 0; row < 9; row++)
        {
            yield return array[row, column];
        }
    }

    public static IEnumerable<T> SliceSquared<T>(this T[,] array, int rowIdx, int colIdx)
    {
        var row_start = rowIdx switch{
            >=0 and <3 => 0,
            >=3 and <6 => 1,
            >=6 =>2,
            _=> throw new Exception()
        };
        var column_start = colIdx switch{
            >=0 and <3 => 0,
            >=3 and <6 => 1,
            >=6 =>2,
            _=> throw new Exception()
        };

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var v = array[(row_start*3) + row, (column_start*3) + col];
                yield return v;
            }
        }
    }

    public static IEnumerable<T> SliceSquared<T>(this T[,] array, int idx)
    {
        var (row_idx, col_idx) = idx switch
        {
            0 => (0, 0),
            1 => (0, 3),
            2 => (0, 6),
            3 => (3, 0),
            4 => (3, 3),
            5 => (3, 6),
            6 => (6, 0),
            7 => (6, 3),
            8 => (6, 6),
            _ => throw new NotImplementedException()
        };

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                yield return array[row_idx + row, col_idx + col];
            }
        }
    }

    public static SudoNode[,] Copy(this SudoNode[,] array)
    {
        SudoNode[,] tmp = new SudoNode[9,9];
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var node = array[row,col];
                tmp[row,col] = new SudoNode()
                {
                    Value = node.Value,
                    RowIdx= node.RowIdx,
                    ColumnIdx = node.ColumnIdx
                };
            }
        }
        return tmp;
    }
}
