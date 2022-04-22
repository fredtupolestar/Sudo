using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudoDemo
{
    public partial class OCRResult : Form
    {
        private bool _isResolved = false;
        private int[] _ocr_sudo_numbers;
        private int[] _src;

        public OCRResult(int[] ocr_numbers, Mat[] mats)
        {
            InitializeComponent();
            this._ocr_sudo_numbers = ocr_numbers;
            Span<int> tmp = new Span<int>(ocr_numbers);
            this._src = tmp.ToArray();

            Init(ocr_numbers);
            this.Location = new System.Drawing.Point(0, 0);
        }

        private void Init(Span<int> mats)
        {
            this.panel1.Controls.Clear();
            Span<int> srcArray = _src.AsSpan();

            for (int row = 0; row < 9; row++)
            {
                var line = mats.Slice(row * 9, 9);
                var src_line = srcArray.Slice(row * 9, 9);
                for (int col = 0; col < line.Length; col++)
                {
                    Label label = new Label();
                    if (IsDisplaySilver(row, col))
                    {
                        label.BackColor = Color.Silver;
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.BackColor = Color.White;
                        label.ForeColor = Color.Black;
                    }
                    label.Size = new System.Drawing.Size(90, 90);
                    label.Location = new System.Drawing.Point(col * 90, row * 90);
                    label.Text = line[col].ToString();
                    label.BorderStyle = BorderStyle.FixedSingle;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    if (line[col] != 0)
                    {
                        if (src_line[col] == 0)
                        {
                            label.ForeColor = Color.DarkRed;
                            label.Font = new Font("Microsoft YaHei", 20f, FontStyle.Bold);
                        }
                        else
                        {
                            if (line.SequenceEqual(src_line))
                            {
                                label.ForeColor = Color.DarkRed;
                                label.Font = new Font("Microsoft YaHei", 20f, FontStyle.Bold);
                            }
                        }
                    }
                    this.panel1.Controls.Add(label);
                }
            }
        }

        private void OCRResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                this.Close();

            if (e.KeyChar == 13)
            {
                if (_isResolved)
                    return;

                Sudo.Core.Sudoku sudoku = new Sudo.Core.Sudoku();
                var isResolved = sudoku.TryResolve(out Sudo.Core.Models.SudoNode[,] resolvedDatas, this._ocr_sudo_numbers);
                if (isResolved)
                {
                    for (int row = 0; row < 9; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            this._ocr_sudo_numbers[(row * 9) + col] = resolvedDatas[row, col].Value;
                        }
                    }

                    Init(_ocr_sudo_numbers);
                    this._isResolved = true;
                }
                else
                {
                    MessageBox.Show("数独无解");
                }
            }
        }

        private bool IsDisplaySilver(int row, int col)
        {
            if (row / 3 != 1 && col / 3 != 1)
                return true;
            else if (row / 3 == 1 && col / 3 == 1)
                return true;

            return false;
        }
    }
}
