using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace P18
{
    public partial class Form1 : Form
    {

        public class cube
        {
            public int X, Y, W, H;
            public Color cl;
            public int speed;
            public int dirX, dirY;
        }

        public class Map
        {
            public int X1, Y1, X2, Y2;
            public Color cl;
        }

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;

            tt.Interval = 100;
            tt.Start();
            tt.Tick += Tt_Tick;

            Xball = 100;
            Yball = 330;
            Wball = 25;
            Hball = 25;

        }

        Bitmap off;
        Timer tt = new Timer();
        List<Map> actors = new List<Map>();
        List<cube> Cubes = new List<cube>();
        int maxX1 = 300, maxY1 = 180, maxX2 = 550, maxY2 = 180;
        int maxX3 = 300, maxY3 = 540, maxX4 = 550, maxY4 = 540;
        int Xball, Yball, Wball, Hball;


        private void Tt_Tick(object sender, EventArgs e)
        {
            MoveRec();
            BallHitRec();
            BallHitPath();

            DrawDubb(this.CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Yball -= 2;
            }
            else if (e.KeyCode == Keys.Down)
            {
                Yball += 2;
            }
            else if (e.KeyCode == Keys.Left)
            {
                Xball -= 2;
            }
            else if (e.KeyCode == Keys.Right)
            {
                Xball += 2;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            createMap();
            createCubes();

        }



        void createMap()
        {
            Map pnn = new Map();
            pnn.X1 = 70;
            pnn.Y1 = 300;
            pnn.X2 = 70;
            pnn.Y2 = 420;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 70;
            pnn.Y1 = 300;
            pnn.X2 = 300;
            pnn.Y2 = 300;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 70;
            pnn.Y1 = 420;
            pnn.X2 = 300;
            pnn.Y2 = 420;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 300;
            pnn.Y1 = 180;
            pnn.X2 = 300;
            pnn.Y2 = 300;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 300;
            pnn.Y1 = 180;
            pnn.X2 = 550;
            pnn.Y2 = 180;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 550;
            pnn.Y1 = 180;
            pnn.X2 = 550;
            pnn.Y2 = 300;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 300;
            pnn.Y1 = 420;
            pnn.X2 = 300;
            pnn.Y2 = 540;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 300;
            pnn.Y1 = 540;
            pnn.X2 = 550;
            pnn.Y2 = 540;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 550;
            pnn.Y1 = 420;
            pnn.X2 = 550;
            pnn.Y2 = 540;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 550;
            pnn.Y1 = 300;
            pnn.X2 = 800;
            pnn.Y2 = 300;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 800;
            pnn.Y1 = 300;
            pnn.X2 = 800;
            pnn.Y2 = 420;
            pnn.cl = Color.White;
            actors.Add(pnn);

            pnn = new Map();
            pnn.X1 = 550;
            pnn.Y1 = 420;
            pnn.X2 = 800;
            pnn.Y2 = 420;
            pnn.cl = Color.White;
            actors.Add(pnn);

        }

        void createCubes()
        {
            cube pnn = new cube();
            pnn.X = 300;
            pnn.Y = 180;
            pnn.W = 120;
            pnn.H = 120;
            pnn.cl = Color.White;
            pnn.speed = 2;
            pnn.dirX = 1;
            pnn.dirY = 0;

            Cubes.Add(pnn);

            pnn = new cube();
            pnn.X = 430;
            pnn.Y = 420;
            pnn.W = 120;
            pnn.H = 120;
            pnn.cl = Color.White;
            pnn.speed = 2;
            pnn.dirX = -1;
            pnn.dirY = 0;

            Cubes.Add(pnn);

        }


        void BallHitPath()
        {
            int ballRight = Xball + Wball;
            int ballLeft = Xball;
            int ballTop = Yball;
            int ballBottom = Yball + Hball;

            for (int i = 0; i < actors.Count; i++)
            {
                Map path = actors[i];

                if ((ballRight >= path.X1 && ballLeft <= path.X1 && ballTop <= path.Y2 && ballBottom >= path.Y1) ||
                    (ballLeft <= path.X2 && ballRight >= path.X2 && ballTop <= path.Y2 && ballBottom >= path.Y1) ||
                    (ballTop <= path.Y1 && ballBottom >= path.Y1 && ballLeft <= path.X2 && ballRight >= path.X1) ||
                    (ballBottom >= path.Y2 && ballTop <= path.Y2 && ballLeft <= path.X2 && ballRight >= path.X1))
                {
                    tt.Stop();
                }
            }
        }

        void BallHitRec()
        {
            for (int i = 0; i < Cubes.Count; i++)
            {
                int ballRight = Xball + Wball;
                int ballLeft = Xball;
                int ballTop = Yball;
                int ballBottom = Yball + Hball;

                int recRight = Cubes[i].X + Cubes[i].W;
                int recLeft = Cubes[i].X;
                int recTop = Cubes[i].Y;
                int recBottom = Cubes[i].Y + Cubes[i].H;

                if (ballRight >= recLeft && ballLeft <= recRight && ballBottom >= recTop && ballTop <= recBottom)
                {
                    tt.Stop();
                }
            }
        }
        void MoveRec()
        {
            for (int i = 0; i < Cubes.Count; i++)
            {
                if (Cubes[i].X == maxX1 && Cubes[i].Y == maxY1)
                {
                    Cubes[i].dirX = 1;
                    Cubes[i].dirY = 0;
                }
                else if (Cubes[i].X + Cubes[i].W == maxX2 && Cubes[i].Y == maxY2)
                {
                    Cubes[i].dirX = 0;
                    Cubes[i].dirY = 1;
                }
                else if (Cubes[i].X == maxX3 && Cubes[i].Y + Cubes[i].H == maxY3)
                {
                    Cubes[i].dirX = 0;
                    Cubes[i].dirY = -1;
                }
                else if (Cubes[i].X + Cubes[i].W == maxX4 && Cubes[i].Y + Cubes[i].H == maxY4)
                {
                    Cubes[i].dirX = -1;
                    Cubes[i].dirY = 0;
                }

                if (Cubes[i].dirX == 0 && Cubes[i].dirY == 1)
                {
                    Cubes[i].Y += Cubes[i].speed;
                }
                else if (Cubes[i].dirX == 0 && Cubes[i].dirY == -1)
                {
                    Cubes[i].Y -= Cubes[i].speed;

                }
                else if (Cubes[i].dirX == 1 && Cubes[i].dirY == 0)
                {
                    Cubes[i].X += Cubes[i].speed;

                }
                else if (Cubes[i].dirX == -1 && Cubes[i].dirY == 0)
                {
                    Cubes[i].X -= Cubes[i].speed;

                }
            }
        }


        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);




            SolidBrush brush = new SolidBrush(Color.Yellow);
            g.FillEllipse(brush, Xball, Yball, Wball, Hball);

            for (int i = 0; i < Cubes.Count; i++)
            {
                Pen pn = new Pen(Cubes[i].cl);
                g.DrawRectangle(pn, Cubes[i].X, Cubes[i].Y, Cubes[i].W, Cubes[i].H);

                brush = new SolidBrush(Cubes[i].cl);
                g.FillRectangle(brush, Cubes[i].X, Cubes[i].Y, Cubes[i].W, Cubes[i].H);

            }

            for (int i = 0; i < actors.Count; i++)
            {
                Pen pn = new Pen(actors[i].cl, 5);
                g.DrawLine(pn, actors[i].X1, actors[i].Y1, actors[i].X2, actors[i].Y2);
            }

        }

        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);

        }
    }
}
