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


    class _1 : baseFigure
    {
        public _1(Random r, StartPosition position)
            : base(1, position)
        {
            L[] a = new L[4];

            a[0].i = 0; a[0].j = 0;
            a[1].i = 0; a[1].j = 1;
            a[2].i = 0; a[2].j = 2;
            a[3].i = 0; a[3].j = 3;

            Color c = this.RandomColor(r);

            this.ShangeFi(a, c);
        }

        _1(int N, Color c, FigureLocation f, StartPosition position)
            : base(N, c, f, position)
        {
        }

        public override baseFigure NextLocation()
        {
            FigureLocation l = this.lockation;
            FigureLocation newLocation = (int)l == 3 ? FigureLocation.Up : l + 1;
            Color c = Color.Violet;
            for (int i = 0; i < this.fi.GetLength(0); i++)
                for (int j = 0; j < this.fi.GetLength(1); j++)
                    if (fi[i, j].Color != Color.White)
                        c = fi[i, j].Color;
            StartPosition position = this.position;

            _1 bf = new _1(1, c, newLocation, position);
            L[] a = new L[4];
            if (l == FigureLocation.Up || l == FigureLocation.Down)
            {
                a[0].i = 0; a[0].j = 0;
                a[1].i = 1; a[1].j = 0;
                a[2].i = 2; a[2].j = 0;
                a[3].i = 3; a[3].j = 0;
            }
            if (l == FigureLocation.Left || l == FigureLocation.Right)
            {
                a[0].i = 0; a[0].j = 0;
                a[1].i = 0; a[1].j = 1;
                a[2].i = 0; a[2].j = 2;
                a[3].i = 0; a[3].j = 3;
            }
            bf.ShangeFi(a, c);
            return bf;
        }
    }

    class _2 : baseFigure
    {
        public _2(Random r, StartPosition position)
            : base(2, position)
        {
            L[] a = new L[4];
            a[0].i = 0; a[0].j = 0;
            a[1].i = 1; a[1].j = 0;
            a[2].i = 0; a[2].j = 1;
            a[3].i = 1; a[3].j = 1;

            Color c = this.RandomColor(r);

            this.ShangeFi(a, c);
        }

        _2(int N, Color c, FigureLocation f, StartPosition position)
            : base(N, c, f, position)
        {
        }

        public override baseFigure NextLocation()
        {
            return this;
        }
    }

    class _3 : baseFigure
    {
        public _3(Random r, StartPosition position)
            : base(3, position)
        {
            L[] a = new L[4];
            a[0].i = 0; a[0].j = 0;
            a[1].i = 0; a[1].j = 1;
            a[2].i = 1; a[2].j = 1;
            a[3].i = 1; a[3].j = 2;

            Color c = this.RandomColor(r);

            this.ShangeFi(a, c);
        }

        _3(int N, Color c, FigureLocation f, StartPosition position)
            : base(N, c, f, position)
        {
        }

        public override baseFigure NextLocation()
        {
            FigureLocation l = this.lockation;
            FigureLocation newLocation = (int)l == 3 ? FigureLocation.Up : l + 1;
            Color c = Color.Violet;
            for (int i = 0; i < this.fi.GetLength(0); i++)
                for (int j = 0; j < this.fi.GetLength(1); j++)
                    if (fi[i, j].Color != Color.White)
                        c = fi[i, j].Color;

            StartPosition position = this.position;

            _3 bf = new _3(3, c, newLocation, position);
            L[] a = new L[4];
            if (l == FigureLocation.Up || l == FigureLocation.Down)
            {
                a[0].i = 0; a[0].j = 1;
                a[1].i = 1; a[1].j = 0;
                a[2].i = 1; a[2].j = 1;
                a[3].i = 2; a[3].j = 0;
            }
            if (l == FigureLocation.Left || l == FigureLocation.Right)
            {
                a[0].i = 0; a[0].j = 0;
                a[1].i = 0; a[1].j = 1;
                a[2].i = 1; a[2].j = 1;
                a[3].i = 1; a[3].j = 2;
            }
            bf.ShangeFi(a, c);
            return bf;
        }
    }

    class _4 : baseFigure
    {
        public _4(Random r, StartPosition position)
            : base(4, position)
        {
            L[] a = new L[4];
            a[0].i = 0; a[0].j = 1;
            a[1].i = 0; a[1].j = 2;
            a[2].i = 1; a[2].j = 0;
            a[3].i = 1; a[3].j = 1;

            Color c = this.RandomColor(r);

            this.ShangeFi(a, c);
        }

        _4(int N, Color c, FigureLocation f, StartPosition position)
            : base(N, c, f, position)
        {
        }

        public override baseFigure NextLocation()
        {
            FigureLocation l = this.lockation;
            FigureLocation newLocation = (int)l == 3 ? FigureLocation.Up : l + 1;
            Color c = Color.Violet;
            for (int i = 0; i < this.fi.GetLength(0); i++)
                for (int j = 0; j < this.fi.GetLength(1); j++)
                    if (fi[i, j].Color != Color.White)
                        c = fi[i, j].Color;
            StartPosition position = this.position;

            _4 bf = new _4(4, c, newLocation, position);
            L[] a = new L[4];
            if (l == FigureLocation.Up || l == FigureLocation.Down)
            {
                a[0].i = 0; a[0].j = 0;
                a[1].i = 1; a[1].j = 0;
                a[2].i = 1; a[2].j = 1;
                a[3].i = 2; a[3].j = 1;
            }
            if (l == FigureLocation.Left || l == FigureLocation.Right)
            {
                a[0].i = 0; a[0].j = 1;
                a[1].i = 0; a[1].j = 2;
                a[2].i = 1; a[2].j = 0;
                a[3].i = 1; a[3].j = 1;
            }
            bf.ShangeFi(a, c);
            return bf;
        }
    }
}
