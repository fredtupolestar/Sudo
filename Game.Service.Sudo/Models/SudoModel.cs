using Game.Service.Sudo.Common;

namespace Game.Service.Sudo.Models;

public partial class SudoModel
{
    private int[,] _unsolved;
    private SudoNode[,] _sudoNodes;

    public SudoNode[,] SudoNodes => _sudoNodes;

    public List<SudoNode[,]> Resolves { get; set; } = new List<SudoNode[,]>();

    public SudoModel()
    {
        this._unsolved = Spatial( new int[81]{0,0,0,0,0,0,0,0,0,0,0,0,1,7,0,0,6,0,0,0,0,4,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,6,4,0,0,7,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0});
        this._sudoNodes = ToSudoNode();
        LinkSudoNodeCollections();
    }

    public SudoModel(int[] sudoNodes)
    {
        this._unsolved = Spatial(sudoNodes);
        this._sudoNodes = ToSudoNode();
        LinkSudoNodeCollections();
    }


    public bool SetValue(int rowIdx, int colIdx, int targetValue)
    {
        var node = _sudoNodes[rowIdx,colIdx];
        var isCorrect = node.SetValue(targetValue);
        return isCorrect;
    }

    public SudoNode GetNode(int rowIdx, int colIdx)
    {
        return _sudoNodes[rowIdx,colIdx];
    }

    public SudoNode? NextEmpty(bool isUseSuggest)
    {
        if (isUseSuggest)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var firstColumnNode = this._sudoNodes[row, col];
                    FilterSuggest(firstColumnNode);
                }
            }
        }
        SudoNode? emptyNode = null;
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if(this._sudoNodes[row, col].Value == 0)
                {
                    var tmpNode = this._sudoNodes[row,col];
                    if(emptyNode is null)
                        emptyNode = tmpNode;
                    
                    if(emptyNode.Suggest.Length == 2)
                        break;

                    if(emptyNode.Suggest.Length > tmpNode.Suggest.Length)
                        emptyNode = tmpNode;
                }
            }
        }
        return emptyNode;
    }

    public bool VerifySudoNodes()
    {
        bool isVerify = true;
        for (int idx = 0; idx < 9; idx++)
        {
            var line = this._sudoNodes.SliceRow(idx);
            var column = this._sudoNodes.SliceColumn(idx);
            var squared = this._sudoNodes.SliceSquared(idx);
            var line_verify = line.Select(a => a.Value).Distinct().Count() == 9;
            var column_verify = column.Select(a=>a.Value).Distinct().Count() == 9;
            var squared_verify = squared.Select(a=>a.Value).Distinct().Count() ==9;

            if(!squared_verify || !column_verify || !line_verify)
            {
                isVerify=false;
                break;
            }
        }
        return isVerify;
    }
}

public partial class SudoModel
{
    private void FilterSuggest(SudoNode node) 
    {
        if(node.Suggest.Length == 1){
            var from = node.Value;
            node.Value = node.Suggest.First();
        }
    }

    /// <summary>
    /// Convert int[] to int[9,9]
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    private int[,] Spatial(int[] sources)
    {
        if(sources is null || sources.Length != 81)
            throw new ArgumentException();

        int[,] spatial_array = new int[9,9];
        var sourcesSpan = sources.AsSpan();
        for (int row = 0; row < 9; row++)
        {
            var line = sourcesSpan.Slice(row*9, 9);
            for (int col = 0; col < line.Length; col++)
            {
                spatial_array[row,col] = line[col];
            }
        }
        return spatial_array;
    }

    /// <summary>
    /// Convert int[,] to SudoNode[,]
    /// </summary>
    /// <returns></returns>
    private SudoNode[,] ToSudoNode()
    {
        SudoNode[,] sudoNodes = new SudoNode[9,9];
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var sudoNode = new SudoNode()
                {
                    Value = this._unsolved[row,col],
                    RowIdx = row,
                    ColumnIdx = col
                };
                sudoNodes[row,col] = sudoNode;
            }
        }
        return sudoNodes;
    }

    private void LinkSudoNodeCollections()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                GetLinkCollections(ref this._sudoNodes[row,col]);
            }
        }
    }

    private void GetLinkCollections(ref SudoNode sudoNode)
    {
        var row = _sudoNodes.SliceRow(sudoNode.RowIdx).ToList();
        var column = _sudoNodes.SliceColumn(sudoNode.ColumnIdx).ToList();
        var squared = _sudoNodes.SliceSquared(sudoNode.RowIdx,sudoNode.ColumnIdx).ToList();
        
        sudoNode.ColumnCollection = column;
        sudoNode.RowCollection = row;
        sudoNode.SquaredCollection = squared;
    }

}

public class SudoNode
{
    public int Value { get; set; }
    public int RowIdx { get; set; }
    public int ColumnIdx { get; set; }
    public int SquaredId
    {
        get
        {
            // return 3x3 squared index
            var row_start = this.RowIdx switch{
                >=0 and <3 => 1,
                >=3 and <6 => 2,
                >=6 => 3,
                _=>throw new NotSupportedException()
            };
            var column_start = this.ColumnIdx switch{
                >=0 and <3 => 1,
                >=3 and <6 => 2,
                >=6 => 3,
                _=>throw new NotSupportedException()
            };

            var squared_idx = row_start * column_start;
            return squared_idx;
        }
    }

    public List<SudoNode> RowCollection { get; set; } = new List<SudoNode>();
    public List<SudoNode> ColumnCollection { get; set; } = new List<SudoNode>();
    public List<SudoNode> SquaredCollection { get; set; } = new List<SudoNode>();

    private readonly int[] _suggestValues = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public int[] Suggest
    {
        get
        {
            if (this.Value != 0)
                return new int[0] { };

            var notSuggestValues = new List<int>();
            notSuggestValues.AddRange(RowCollection.Select(a=>a.Value));
            notSuggestValues.AddRange(ColumnCollection.Select(a=>a.Value));
            notSuggestValues.AddRange(SquaredCollection.Select(a=>a.Value));
            return _suggestValues.Except(notSuggestValues.Distinct()).ToArray();
        }
    }

    public bool SetValue(int targetValue)
    {
        var isInRow = this.RowCollection.Where(a => a.RowIdx != this.RowIdx && a.ColumnIdx != this.ColumnIdx).Any(x => x.Value == targetValue);
        var isInColumn = this.ColumnCollection.Where(a => a.RowIdx != this.RowIdx && a.ColumnIdx != this.ColumnIdx).Any(x => x.Value == targetValue);
        var isInSquared = this.SquaredCollection.Where(a => a.RowIdx != this.RowIdx && a.ColumnIdx != this.ColumnIdx).Any(x => x.Value == targetValue);

        this.Value = targetValue;
        if(!isInRow && !isInColumn && !isInSquared)
            return false;

        return true;
    }
}