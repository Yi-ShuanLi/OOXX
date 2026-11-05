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
            string[] OOButtonTag = button.Tag.ToString().Split(',');
            int[] OOAddress = OOButtonTag.Select(x => int.Parse(x)).ToArray();
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
            ComputerPlay();
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
        /// <summary>
        /// 電腦XX劃格子，在所有目前空的格子裡，去模擬把XX劃下去
        /// 因為XX贏所得的分數是-100，所以是設定9999最大去找以XX為立場的最小值。
        /// </summary>
        private void ComputerPlay()
        {
            int bestScore = 9999;
            int bestRow = 0;
            int bestCol = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Record[i, j] == 0)
                    {
                        Record[i, j] = 2;
                        int scroe = MiniMaxFindMax(i, j); // 換玩家跑，所有還沒選的格子，
                                                          // 每格丟下去評分，取最大
                        Record[i, j] = 0;
                        if (scroe < bestScore)
                        {
                            bestScore = scroe;
                            bestRow = i;
                            bestCol = j;
                        }
                    }
                }
            }
            Button XXButton = this.Controls.OfType<Button>().FirstOrDefault(x => x.Tag.ToString() == $"{bestRow},{bestCol}");
            XXButton.Text = "X";
            XXButton.ForeColor = Color.Red;
            Record[bestRow, bestCol] = 2;
        }

        /// <summary>
        /// 電腦模擬玩家OO劃框，玩家OO立場 return 值越大越好
        /// 因為OO贏是+100，所以設定-9999去找最大值。
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private int MiniMaxFindMax(int rowIndex, int colIndex)
        {
            if (CheckWin(1)) // O是1
            {
                return 100;
            }

            if (CheckWin(2)) // X是2
            {
                return -100;
            }
            if (CheckTie()) //平手
            {
                return 0;
            }

            int bestScord = -9999; //因為 MiniMaxFindMin 會返回最小
            int bestRow = 0;
            int bestCol = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Record[i, j] == 0)
                    {
                        Record[i, j] = 1;
                        int score = MiniMaxFindMin(i, j);  // 下一步電腦預測玩家user的框
                                                           // 
                        Record[i, j] = 0;
                        if (score > bestScord)
                        {
                            bestScord = score;
                            bestRow = i;
                            bestCol = j;
                        }
                    }
                }
            }
            return bestScord;
        }

        /// <summary>
        /// 電腦XX劃框，因為XX贏是-100，所以用9999去找最小值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>

        private int MiniMaxFindMin(int row, int col)
        {
            if (CheckWin(1)) // O是1
            {
                return 100;
            }

            if (CheckWin(2)) // X是2
            {
                return -100;
            }
            if (CheckTie()) //平手
            {
                return 0;
            }

            int worseScore = 9999;
            int bestRow = 0;
            int bestCol = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Record[i, j] == 0)
                    {
                        Record[i, j] = 2;
                        int score = MiniMaxFindMax(i, j); // 電腦模擬玩家OO劃框
                                                          // 
                        Record[i, j] = 0;
                        if (score < worseScore)
                        {
                            worseScore = score;
                            bestRow = i;
                            bestCol = j;
                        }
                    }
                }
            }
            return worseScore;

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
