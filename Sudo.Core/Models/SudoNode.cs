using System.Globalization;
using System.Linq;
namespace Sudo.Core.Models;

public class SudoNode
{
    public int Value { get; set; }
    public int RowIndex { get; set; }
    public int ColumnIndex { get; set; }

    public SudoNode[] Rows { get; set; } = null!;
    public SudoNode[] Columns { get; set; } = null!;
    public SudoNode[] Squared { get; set; } = null!;

    public bool IsVerify
    {
        get
        {
            var row_exists = this.Rows.Any(a => a.Value == this.Value && a.RowIndex != this.RowIndex && a.ColumnIndex != this.ColumnIndex);
            var col_exists = this.Columns.Any(a => a.Value == this.Value && a.RowIndex != this.RowIndex && a.ColumnIndex != this.ColumnIndex);
            var squared_exists = this.Squared.Any(a => a.Value == this.Value && a.RowIndex != this.RowIndex && a.ColumnIndex != this.ColumnIndex);

            if(row_exists || col_exists || squared_exists)
                return false;

            return true;
        }
    }

    private readonly List<int> _allow = new List<int>(){1,2,3,4,5,6,7,8,9};
    public List<int> SuitableNumbers
    {
        get
        {
            var rows = this.Rows.Where(x=>x.Value!=0).Select(a=>a.Value).ToList();
            var columns = this.Columns.Where(x=>x.Value!=0).Select(a=>a.Value).ToList();
            var squareds = this.Squared.Where(x=>x.Value!=0).Select(a=>a.Value).ToList();
            List<int> existsNumbers = new List<int>();
            existsNumbers.AddRange(rows);
            existsNumbers.AddRange(columns);
            existsNumbers.AddRange(squareds);
            existsNumbers = existsNumbers.Distinct().ToList();
            
            return _allow.Except(existsNumbers).ToList();
        }
    }

    public bool CanInsert(int value)
    {
        return !IsExistInRow(value) && !IsExistInColumn(value) && !IsExistInSquared(value);
    }

    public bool IsExistInRow(int target)
    {
        if(!this.Rows.Any())
            throw new ArgumentNullException();

        return this.Rows.Any(a=>a.Value == target);
    }

    public bool IsExistInColumn(int target)
    {
        if(!this.Columns.Any())
            throw new ArgumentNullException();

        return this.Columns.Any(a=>a.Value == target);
    }

    public bool IsExistInSquared(int target)
    {
        if(!this.Squared.Any())
            throw new ArgumentNullException();

        return this.Squared.Any(a=>a.Value == target);
    }
}
