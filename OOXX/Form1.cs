using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOXX
{
    public partial class Form1 : Form
    {
        //bool Oo = true;
        int[,] Record = new int[3, 3];
        // 使用int 2維陣列是因為int的預設是0
        // 把O設定為1去儲存，把X設定為2去儲存
        public Form1()
        {
            InitializeComponent();
        }

        private void Choose_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string[] buttonTag = button.Tag.ToString().Split(',');
            int[] OOAddress = buttonTag.Select(x => int.Parse(x)).ToArray();
            //int currentWrite = Oo == true ? 1 : 2;
            //string currentText = Oo == true ? "O" : "X";
            if (Record[OOAddress[0], OOAddress[1]] != 0)
            {
                return;
            }
            button.ForeColor = Color.Blue;
            button.Text = "O";
            Record[OOAddress[0], OOAddress[1]] = 1;

            if (CheckWin(1))
            {
                MessageBox.Show($"O贏了!!");
                this.Controls.OfType<Button>().ToList().ForEach(x => x.Text = "");
                Record = new int[3, 3];
                return;
            }
            if (CheckTie())
            {
                MessageBox.Show($"平手!!");
                this.Controls.OfType<Button>().ToList().ForEach(x => x.Text = "");
                Record = new int[3, 3];
                return;
            }

            List<Button> notSelectedButton = this.Controls.OfType<Button>().Where(x => x.Text == "").ToList();
            List<int> notSelectedScore = CalculateNotSelectedButton();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int index = random.Next(0, notSelectedButton.Count);
            notSelectedButton[index].ForeColor = Color.Red;
            notSelectedButton[index].Text = "X";
            string[] XXButtonTag = notSelectedButton[index].Tag.ToString().Split(',');
            int[] XXButtonAddress = XXButtonTag.Select(x => int.Parse(x)).ToArray();
            Record[XXButtonAddress[0], XXButtonAddress[1]] = 2;

            if (CheckWin(2))
            {
                MessageBox.Show($"X贏了!!");
                this.Controls.OfType<Button>().ToList().ForEach(x => x.Text = "");
                Record = new int[3, 3];
                return;
            }
            if (CheckTie())
            {
                MessageBox.Show($"平手!!");
                this.Controls.OfType<Button>().ToList().ForEach(x => x.Text = "");
                Record = new int[3, 3];
                return;
            }

        }

        private List<int> CalculateNotSelectedButton()
        {
        }

        private bool CheckWin(int writeNum)
        {
            int row = Record.GetLength(0);
            int col = Record.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Record[i, j] == 0)
                    {
                        break;
                    }
                    if (Record[i, j] != writeNum)
                    {
                        break;
                    }
                    if (j == col - 1)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (Record[j, i] == 0)
                    {
                        break;
                    }
                    if (Record[j, i] != writeNum)
                    {
                        break;
                    }
                    if (j == row - 1)
                    {
                        return true;
                    }
                }
            }
            if (Record[0, 0] == writeNum && Record[1, 1] == writeNum && Record[2, 2] == writeNum)
            {
                return true;
            }
            if (Record[0, 2] == writeNum && Record[1, 1] == writeNum && Record[2, 0] == writeNum)
            {
                return true;
            }
            return false;
        }

        private bool CheckTie()
        {
            int row = Record.GetLength(0);
            int col = Record.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Record[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
