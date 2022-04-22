using Sudo.Core.Models;

namespace Sudo.Core;

public class Common
{
    #region Find link nodes
    public static void LinkNodes(ref SudoNode[,] nodes)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                nodes[row, column].Rows = GetCollection(nodes, Enums.CollectionType.Row, row, column);
                nodes[row, column].Columns = GetCollection(nodes, Enums.CollectionType.Column, row, column);
                nodes[row, column].Squared = GetCollection(nodes, Enums.CollectionType.Squared, row, column);
            }
        }
    }

    public static SudoNode[] GetCollection(SudoNode[,] nodes, Enums.CollectionType ct, int rowIdx, int columnIdx)
    {
        return ct switch
        {
            Enums.CollectionType.Column => GetCollectionByColumn(nodes, columnIdx),
            Enums.CollectionType.Row => GetCollectionByRow(nodes, rowIdx),
            Enums.CollectionType.Squared => GetCollectionBySquared(nodes, rowIdx, columnIdx),
            _ => throw new NotSupportedException()
        };
    }

    private static SudoNode[] GetCollectionBySquared(SudoNode[,] nodes, int rowIdx, int columnIdx)
    {
        var rowStartAt = rowIdx switch
        {
            >= 0 and < 3 => 0,
            >= 3 and < 6 => 1,
            >= 6 => 2,
            _ => throw new ArgumentException()
        };

        var columnStartAt = columnIdx switch
        {
            >= 0 and < 3 => 0,
            >= 3 and < 6 => 1,
            >= 6 => 2,
            _ => throw new ArgumentException()
        };

        List<SudoNode> sudoSquared = new List<SudoNode>();
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                var row = rowStartAt * 3 + r;
                var col = columnStartAt * 3 + c;
                sudoSquared.Add(nodes[row, col]);
            }
        }

        return sudoSquared.ToArray();
    }

    private static SudoNode[] GetCollectionByRow(SudoNode[,] nodes, int rowIdx)
    {
        List<SudoNode> sudoRow = new List<SudoNode>();
        for (int col = 0; col < 9; col++)
        {
            sudoRow.Add(nodes[rowIdx, col]);
        }
        return sudoRow.ToArray();
    }

    private static SudoNode[] GetCollectionByColumn(SudoNode[,] nodes, int columnIdx)
    {
        List<SudoNode> sudoColumn = new List<SudoNode>();
        for (int row = 0; row < 9; row++)
        {
            sudoColumn.Add(nodes[row, columnIdx]);
        }
        return sudoColumn.ToArray();
    }

    #endregion
}
