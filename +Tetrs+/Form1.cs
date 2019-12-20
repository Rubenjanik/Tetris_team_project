using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace _Tetrs_
{
    public partial class Form1 : Form
    {
        Square[,] desk = new Square[10, 20];

        int[] records = new int[10];

        bool ProcessGame;
        bool ifod;
        int BaseLVL;

        Random rnd = new Random(DateTime.Now.Millisecond);

        baseFigure f, fNext;

        int level = 0, lines = 0, points = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size s = new Size(800, 660);
            this.MinimumSize = s;
            this.MaximumSize = s;
            this.Location = new Point(50, 50);

            GetRecords();

            ProcessGame = false;
            ifod = false;

            for (int i = 0; i < desk.GetLength(0); i++)
                for (int j = 0; j < desk.GetLength(1); j++)
                    desk[i, j] = Square.Empty;
            BaseLVL = 0;

        }

        int DeleteLine()
        {
            int Lines = 0;
            bool thisline = true;
            for (int i = 0; i < desk.GetLength(1); i++)
            {
                if (desk[0, i].Flag)
                    thisline = true;
                else thisline = false;
                for (int j = 0; j < desk.GetLength(0); j++)
                {
                    if (desk[j, i].Flag && thisline)
                        thisline = true;
                    else
                    {
                        thisline = false;
                        break;
                    }
                }
                if (thisline)
                {
                    Lines++;
                    for (int q = i; q >= 1; q--)
                        for (int w = 0; w < desk.GetLength(0); w++)
                        {
                            desk[w, q] = desk[w, q - 1];
                        }
                    for (int w = 0; w < desk.GetLength(0); w++)
                        desk[w, 0] = Square.Empty;
                }
            }
            return Lines;
        }

        bool CanCreate()
        {
            if (f is _1)
            {
                if (desk[4, 0].Flag || desk[4, 1].Flag || desk[4, 2].Flag || desk[4, 3].Flag)
                    return false;
                else return true;
            }
            if (f is _2)
            {
                if (desk[4, 0].Flag || desk[4, 1].Flag || desk[5, 0].Flag || desk[5, 1].Flag)
                    return false;
                else return true;
            }
            if (f is _3)
            {
                if (desk[4, 0].Flag || desk[4, 1].Flag || desk[5, 1].Flag || desk[5, 2].Flag)
                    return false;
                return true;
            }
            if (f is _4)
            {
                if (desk[4, 1].Flag || desk[4, 2].Flag || desk[5, 0].Flag || desk[5, 1].Flag)
                    return false;
                return true;
            }
            if (f is _5)
            {
                if (desk[4, 0].Flag || desk[4, 1].Flag || desk[4, 2].Flag || desk[5, 2].Flag)
                    return false;
                return true;
            }
            if (f is _6)
            {
                if (desk[5, 0].Flag || desk[5, 1].Flag || desk[5, 2].Flag || desk[4, 2].Flag)
                    return false;
                return true;
            }
            if (f is _7)
            {
                if (desk[4, 0].Flag || desk[4, 1].Flag || desk[4, 2].Flag || desk[5, 1].Flag)
                    return false;
                return true;
            }
            return false;
        }

        private void GetRecords()
        {
            if (!File.Exists("records.txt"))
                for (int i = 0; i < 10; i++)
                    records[i] = -1000;
            else
            {
                string[] arr = File.ReadAllLines("records.txt");
                int i;
                for (i = 0; i < 10 && i < arr.Length; i++)
                    records[i] = Convert.ToInt32(arr[i]);
                for (; i < 10; i++)
                    records[i] = -1000;
            }
            rec1.Text = records[0] == -1000 ? "" : Convert.ToString(records[0]);
            rec2.Text = records[1] == -1000 ? "" : Convert.ToString(records[1]);
            rec3.Text = records[2] == -1000 ? "" : Convert.ToString(records[2]);
            rec4.Text = records[3] == -1000 ? "" : Convert.ToString(records[3]);
            rec5.Text = records[4] == -1000 ? "" : Convert.ToString(records[4]);
            rec6.Text = records[5] == -1000 ? "" : Convert.ToString(records[5]);
            rec7.Text = records[6] == -1000 ? "" : Convert.ToString(records[6]);
            rec8.Text = records[7] == -1000 ? "" : Convert.ToString(records[7]);
            rec9.Text = records[8] == -1000 ? "" : Convert.ToString(records[8]);
            rec10.Text = records[9] == -1000 ? "" : Convert.ToString(records[9]);

        }

        private void WriteRecords()
        {
            for (int i = 0; i < 10; i++)
                if (points > records[i])
                {
                    for (int j = 9; j > i; j--)
                        records[j] = records[j - 1];

                    records[i] = points;
                    break;
                }

            StreamWriter sw = new StreamWriter("records.txt");
            for (int i = 0; i < 10; i++)
            {
                if (records[i] == -1000)
                    break;
                sw.WriteLine(records[i]);
            }
            sw.Close();
        }
        bool MoveDown()
        {
            bool con = false;

            if (f is _1)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.J == 16 || desk[f.Position.I, f.Position.J + 4].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.J == 19 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 3, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 3, f.Position.J] = Square.Empty;
                    }
                }
            }

            if (f is _2)
            {
                if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                    con = false;
                else con = true;
                if (con)
                {
                    desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty;
                }
            }

            if (f is _3)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
            }

            if (f is _4)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _5)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
            }
            if (f is _6)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 2] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
            }
            if (f is _7)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }

            if (con)
            {
                f.PositionJ++;
            }

            return con;
        }

        bool MoveDown(int e)
        {
            {
                bool con = false;

                if (f is _1)
                {
                    if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                    {
                        if (f.Position.J == 16 || desk[f.Position.I, f.Position.J + 4].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                    {
                        if (f.Position.J == 19 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 3, f.Position.J + 1].Flag)
                            con = false;
                        else con = true;
                    }
                }
                if (f is _2)
                {
                    if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                }

                if (f is _3)
                {
                    if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                            con = false;
                        else con = true;
                    }
                }
                if (f is _4)
                {
                    if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                            con = false;
                        else con = true;
                    }
                }
                if (f is _5)
                {
                    if (f.Location == FigureLocation.Up)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Right)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Down)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Left)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                            con = false;
                        else con = true;
                    }
                }
                if (f is _6)
                {
                    if (f.Location == FigureLocation.Up)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Right)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Down)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Left)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                            con = false;
                        else con = true;
                    }
                }
                if (f is _7)
                {
                    if (f.Location == FigureLocation.Up)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 3].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Right)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Down)
                    {
                        if (f.Position.J == 17 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                            con = false;
                        else con = true;
                    }
                    if (f.Location == FigureLocation.Left)
                    {
                        if (f.Position.J == 18 || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                            con = false;
                        else con = true;
                    }
                }
                return con;
            }
        }

        void Turn()
        {
            bool con = false;

            if (f is _1)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 3, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty; desk[f.Position.I, f.Position.J + 3] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.J > 16 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 3, f.Position.J] = Square.Empty;
                    }
                }
            }
            if (f is _2)
            {
                con = true;
            }
            if (f is _3)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.J > 17 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
            }
            if (f is _4)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.J > 17 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _5)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.J > 17 || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.J > 17 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _6)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I > 7 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.J > 17 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.J > 17 || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _7)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.J > 17 || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.J > 17 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (con)
                f = f.NextLocation();
        }

        void MoveLeft()
        {
            bool con = false;

            if (f is _1)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I - 1, f.Position.J + 2].Flag || desk[f.Position.I - 1, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty; desk[f.Position.I, f.Position.J + 3] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right || f.Location == FigureLocation.Left)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 3, f.Position.J] = Square.Empty;
                    }
                }
            }
            if (f is _2)
            {
                if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag)
                    con = false;
                else con = true;
                if (con)
                {
                    desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty;
                }
            }
            if (f is _3)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.I < 1 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _4)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I - 1, f.Position.J + 2].Flag || desk[f.Position.I, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left || f.Location == FigureLocation.Right)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _5)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I - 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 1].Flag || desk[f.Position.I, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _6)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J + 2].Flag || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I - 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _7)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I - 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.I < 1 || desk[f.Position.I - 1, f.Position.J].Flag || desk[f.Position.I, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 2, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I < 1 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag || desk[f.Position.I, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.I < 1 || desk[f.Position.I, f.Position.J].Flag || desk[f.Position.I - 1, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (con)
                f.PositionI--;
        }

        void MoveRight()
        {
            bool con = false;

            if (f is _1)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 8 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag || desk[f.Position.I + 1, f.Position.J + 3].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty; desk[f.Position.I, f.Position.J + 3] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right || f.Location == FigureLocation.Left)
                {
                    if (f.Position.I > 5 || desk[f.Position.I + 4, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty;
                    }
                }
            }
            if (f is _2)
            {
                if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                    con = false;
                else con = true;
                if (con)
                {
                    desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                }
            }
            if (f is _3)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right || f.Location == FigureLocation.Left)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 3, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _4)
            {
                if (f.Location == FigureLocation.Up || f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right || f.Location == FigureLocation.Left)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 3, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _5)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 3, f.Position.J].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 3, f.Position.J].Flag || desk[f.Position.I + 3, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 2, f.Position.J] = Square.Empty;
                    }
                }
            }
            if (f is _6)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 3, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 1, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 3, f.Position.J].Flag || desk[f.Position.I + 3, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 2, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (f is _7)
            {
                if (f.Location == FigureLocation.Up)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 1, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 1, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Right)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 3, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I, f.Position.J] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 1] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Down)
                {
                    if (f.Position.I > 7 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 2, f.Position.J + 1].Flag || desk[f.Position.I + 2, f.Position.J + 2].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty; desk[f.Position.I + 1, f.Position.J + 2] = Square.Empty;
                    }
                }
                if (f.Location == FigureLocation.Left)
                {
                    if (f.Position.I > 6 || desk[f.Position.I + 2, f.Position.J].Flag || desk[f.Position.I + 3, f.Position.J + 1].Flag)
                        con = false;
                    else con = true;
                    if (con)
                    {
                        desk[f.Position.I + 1, f.Position.J] = Square.Empty; desk[f.Position.I, f.Position.J + 1] = Square.Empty;
                    }
                }
            }
            if (con)
                f.PositionI++;
        }
    }
}
