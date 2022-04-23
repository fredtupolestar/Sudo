using System.Linq;
namespace Game.Service.Sudo;

public partial class Sudoku
{
    private Models.SudoModel _sudoModel;
    public Models.SudoModel SudoModel=> _sudoModel;

    public Sudoku()
    {
        _sudoModel = new Models.SudoModel();
    }

    private Sudoku(int[] sudoNodes)
    {
        _sudoModel = new Models.SudoModel(sudoNodes);
    }

    public void SetValue(int rowIdx, int colIdx, int targetValue)
    {
        _ = _sudoModel.SetValue(rowIdx,colIdx, targetValue);
    }
}

public partial class Sudoku
{
    public static Sudoku Load(int[] sudoNodes)
    {
        return new Sudoku(sudoNodes);
    }
}