using System.Diagnostics;

namespace SudoDemo;

public partial class Form1 : Form
{
    private readonly Color _silverColor = Color.Gainsboro;
    private readonly Color _multipleNumberColor = Color.OrangeRed;
    private readonly Color _errorColor = Color.DarkRed;
    private Font font = new Font("Microsoft YaHei", 20f, FontStyle.Bold);

    private List<Label> _multipleNumberCtrls = new List<Label>();
    private List<Label> _relationCtrls = new List<Label>();
    private List<Label> _errors = new List<Label>();

    private Label? _current_Label = null;
    private Sudo.Core.Sudoku _sudo = null!;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        _sudo = new Sudo.Core.Sudoku();
    }

    private Control CreateItem(int row,int col)
    {
        var val = _sudo.OriginalSudoNodes[row, col].Value;

        Label label = new Label();
        if (IsDisplaySilver(row, col))
        {
            label.BackColor = _silverColor;
            label.ForeColor = Color.Black;
        }
        else
        {
            label.BackColor = Color.White;
            label.ForeColor = Color.Black;
        }
        label.Size = new Size(90, 90);
        label.Margin = new Padding(0);
        label.BorderStyle = BorderStyle.FixedSingle;
        label.Text = val == 0? "" : val.ToString();
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.Font = font;
        label.Name = $"{row}_{col}";
        label.Click += SudoItem_Clicked;

        var location_x = col * 90;
        var location_y = row * 90;
        label.Location = new Point(location_x, location_y);

        return label;
    }

    private bool IsDisplaySilver(int row,int col)
    {
        if (row / 3 != 1 && col / 3 != 1)
            return true;
        else if (row / 3 == 1 && col / 3 == 1)
            return true;

        return false;
    }

    private void SudoItem_Clicked(object? sender, EventArgs e)
    {
        ResetCtrlColor();

        this._current_Label = (Label)sender!;
        FindRelationControls(_current_Label.Name);
        FindMutipleNumberCtrls(_current_Label.Text);

        DrawingCtrlColor();


        this._current_Label.BackColor = Color.Green;
        this._current_Label.ForeColor = Color.White;
    }

    private void FindMutipleNumberCtrls(string text)
    {
        this._multipleNumberCtrls = new List<Label>();
        foreach (Label item in this.panel1.Controls)
        {
            if (item.Text == text)
                this._multipleNumberCtrls.Add(item);
        }
    }

    private void DrawingCtrlColor()
    {
        if (this._relationCtrls.Any())
        {
            foreach (var item in this._relationCtrls)
            {
                item.BackColor = Color.MediumSeaGreen;
                item.ForeColor = Color.White;
            }
        }

        if (this._multipleNumberCtrls.Any())
        {
            foreach (var item in this._multipleNumberCtrls)
            {
                item.ForeColor = _multipleNumberColor;
            }
        }

        if(this._errors.Any())
        {
            foreach (var item in this._multipleNumberCtrls)
            {
                item.ForeColor = _errorColor;
            }
        }
    }

    private void ResetCtrlColor()
    {
        if (this._current_Label != null)
        {
            ResetCtrlColor(this._current_Label.Name, this._current_Label);
        }

        if (this._relationCtrls.Any())
        {
            foreach (var item in this._relationCtrls)
            {
                ResetCtrlColor(item.Name, item);
            }
        }
        if (this._multipleNumberCtrls.Any())
        {
            foreach (var item in this._multipleNumberCtrls)
            {
                ResetCtrlColor(item.Name, item);
            }
        }
    }

    private void ResetCtrlColor(string name, Label ctrl)
    {
        var (row, col) = GetRowColumnByName(name);
        var specDisplay = IsDisplaySilver(row, col);
        if (specDisplay)
        {
            ctrl.BackColor = _silverColor;
            ctrl.ForeColor = Color.Black;
        }
        else
        {
            ctrl.BackColor = Color.White;
            ctrl.ForeColor = Color.Black;
        }
    }

    private (int Row, int Column) GetRowColumnByName(string name)
    {
        var row = Convert.ToInt32(name[0].ToString());
        var col = Convert.ToInt32(name[2].ToString());

        return (row, col);
    }

    private void FindRelationControls(string sourceCtrName)
    {
        if (!sourceCtrName.Contains('_'))
            return;

        var relationControls = new List<Label>();
        
        var (row,col) = GetRowColumnByName(sourceCtrName);

        foreach (var ctrl in this.panel1.Controls)
        {
            relationControls.Add((Label)ctrl);
        }

        var rows = relationControls.Where(a => a.Name.Contains($"{row}_")).ToList();
        var cols = relationControls.Where(b => b.Name.Contains($"_{col}")).ToList();

        relationControls.Clear();
        relationControls.AddRange(rows);
        relationControls.AddRange(cols);
        relationControls.Distinct();

        _relationCtrls = relationControls;
    }

    private void Form1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if(this._current_Label is null)
             return;

        var (rowIdx, colIdx) = GetRowColumnByName(this._current_Label.Name);

        if ( 49 <= e.KeyChar && e.KeyChar <= 57)
        {
            var inputNum = int.Parse(e.KeyChar.ToString());
            this._current_Label.Text = e.KeyChar.ToString();
            
            var gameStatus = _sudo.SetValue(rowIdx, colIdx, inputNum);
            if (gameStatus == Sudo.Core.Enums.GameStatusEnum.Failed)
                MessageBox.Show("Failed");
            else if (gameStatus == Sudo.Core.Enums.GameStatusEnum.Win)
                MessageBox.Show("You win the game!");
        }

        if(e.KeyChar == 8)
        {
            this._current_Label.Text = "";
            _sudo.SetValue(rowIdx, colIdx, 0);
        }
    }

    private void ctl_new_game_Click(object sender, EventArgs e)
    {
        StartNewGame(11);
    }

    private void StartNewGame(int level)
    {
        _sudo.Level = level;
        _ =_sudo.TryResolve(out Sudo.Core.Models.SudoNode[,] resolved);

        string resultMsg = "";
        for (int row = 0; row < 9; row++)
        {
            int[] res = new int[9];
            for (int col = 0; col < 9; col++)
            {
                res[col] = resolved[row, col].Value;
            }
            resultMsg += string.Join(',', res);
            resultMsg += "\r\n";
        }
        MessageBox.Show(resultMsg);

        this.panel1.Controls.Clear();

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var label = CreateItem(row, col);

                this.panel1.Controls.Add(label);
            }
        }
    }

    private void ctrl_level_low_Click(object sender, EventArgs e)
    {
        StartNewGame(20);
    }

    private void ctrl_level_middle_Click(object sender, EventArgs e)
    {
        StartNewGame(15);
    }

    private void ctrl_level_high_Click(object sender, EventArgs e)
    {
        StartNewGame(11);
    }

    private void ctrl_resolver_Click(object sender, EventArgs e)
    {
        SudoRecognize sudoRecognize = new SudoRecognize();
        sudoRecognize.Show();
    }
}
