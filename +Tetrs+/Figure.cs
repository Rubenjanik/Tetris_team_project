using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _Tetrs_
{
    /// <summary>
    /// Здесь описываются все фигурки, которые появляются
    /// </summary>

    struct L
    {
        public int i;
        public int j;
    }

    struct StartPosition
    {
        public int I;
        public int J;
    }

    enum FigureLocation
    {
        Up, Right, Down, Left
    }

    class baseFigure
    {
        protected int number;
        protected Square[,] fi = new Square[4, 4];
        protected FigureLocation lockation;
        protected StartPosition position;

        public baseFigure(int N, StartPosition position)
        {
            number = N;
            lockation = FigureLocation.Up;
            this.position = position;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    fi[i, j] = Square.Empty;
        }

        public baseFigure(int N, Color c, FigureLocation f, StartPosition position)
        {
            number = N;
            lockation = f;
            this.position = position;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    fi[i, j] = Square.Empty;
        }

        public Square[,] Fi
        {
            get
            {
                return fi;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }
        }

        public StartPosition Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public int PositionI
        {
            get
            {
                return position.I;
            }
            set
            {
                position.I = value;
            }
        }

        public int PositionJ
        {
            get
            {
                return position.J;
            }
            set
            {
                position.J = value;
            }
        }

        public FigureLocation Location
        {
            get
            {
                return lockation;
            }
        }

        public virtual baseFigure CreateF()
        {
            return this;
        }

        public virtual baseFigure NextLocation()
        {
            return this;
        }

        public Color RandomColor(Random r)
        {
            Color c = Color.Violet;

            switch (r.Next(1, 8))
            {
                case 1: c = Color.Red; break;
                case 2: c = Color.Orange; break;
                case 3: c = Color.DarkGoldenrod; break;
                case 4: c = Color.Green; break;
                case 5: c = Color.CornflowerBlue; break;
                case 6: c = Color.DarkBlue; break;
                case 7: c = Color.BlueViolet; break;
            }
            return c;
        }

        public void ShangeFi(L[] a, Color c)
        {
            for (int h = 0; h < a.Length; h++)
            {
                fi[a[h].i, a[h].j] = new Square(true, c);
            }
        }
    }
}
