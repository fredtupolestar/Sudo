using System.Diagnostics;
using System.Reflection.Metadata;
using Sudo.Core.Models;
namespace Sudo.Core;

public class Sudoku
{
    private SudoNode[,] _originalSudoNodes = new SudoNode[9, 9];
    public SudoNode[,] OriginalSudoNodes => _originalSudoNodes;

    private SudoNode[,] _sudoNodes = new SudoNode[9,9];

    private Resolver resolver = null!;

    public int Level = 11;

    public Sudoku():this(new int[81])
    {
    }

    public Sudoku(int[] sudoArray, int level = 11)
    {
        this.Level = level;
        InitSudoNodes(sudoArray);
        resolver = new Resolver(this._sudoNodes);
    }

    public int GetValue(int rowIdx, int colIdx)
    {
        return this._originalSudoNodes[rowIdx,colIdx].Value;
    } 

    public Enums.GameStatusEnum SetValue(int rowIdx,int colIdx, int targetValue)
    {
        this.OriginalSudoNodes[rowIdx, colIdx].Value = targetValue;
        Common.LinkNodes(ref _originalSudoNodes);
        Enums.GameStatusEnum gameResult = resolver.TestSudo(_originalSudoNodes);
        return gameResult;
    }

    public bool TryResolve(out SudoNode[,] resolved, int[] array)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var isResolved = false;
        InitSudoNodes(array);
        isResolved = resolver.ResolveSudo(this._sudoNodes);
        resolved = resolver.SudoNodes;

        stopwatch.Stop();
        Console.WriteLine($"Recursion:{resolver.RecursionTimes } times, Cost: {stopwatch.ElapsedMilliseconds}ms");
        return isResolved;
    }


    public bool TryResolve(out SudoNode[,] resolved)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int tryCnt = 0;
        var isResolved = false;
        while (!isResolved)
        {
            InitSudoNodes();
            isResolved = resolver.ResolveSudo(this._sudoNodes);
            tryCnt += 1;
            Debug.Write($"Try:{tryCnt}");
        }
        resolved = resolver.SudoNodes;

        stopwatch.Stop();
        Console.WriteLine($"Try: {tryCnt} times. Cost: {stopwatch.ElapsedMilliseconds}ms");
        return isResolved;
    }

    #region SudoNode init
    private void InitSudoNodes(int[]? array = null)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                _sudoNodes[row,col] = new SudoNode()
                {
                    Value = array is null ? 0 : array[(row*9)+col],
                    ColumnIndex = col,
                    RowIndex = row
                };
            }
        }

        Common.LinkNodes(ref _sudoNodes);

        if (array is null)
            InitBaseDataToSudoNode(this.Level);

        CopySudoNodesToOriginal();
    }

    private void CopySudoNodesToOriginal()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var source = this._sudoNodes[row,col];
                this.OriginalSudoNodes[row, col] = new SudoNode()
                {
                    Value = source.Value,
                    ColumnIndex = source.ColumnIndex,
                    RowIndex = source.RowIndex,
                    Columns = source.Columns,
                    Rows = source.Rows,
                    Squared = source.Squared
                };
            }
        }
    }

    private void InitBaseDataToSudoNode(int level = 11)
    {
        for (int i = 0; i < level; i++)
        {
            Random idxRandom = new Random();
            Random valueRandom = new Random();

            RANDOM_INDEX:
            var rowIdx = idxRandom.Next(0,9); 
            var colIdx = idxRandom.Next(0,9);
            var node = _sudoNodes[rowIdx,colIdx];
            if(node.Value!= 0)
                goto RANDOM_INDEX;
            
            RANDOM_VALUE:
            var targetValue = valueRandom.Next(1,10);
            if(!node.CanInsert(targetValue))
                goto RANDOM_VALUE;

            _sudoNodes[rowIdx, colIdx].Value = targetValue;
            // relink
            Common.LinkNodes(ref _sudoNodes);
        }
    }
    #endregion
}
