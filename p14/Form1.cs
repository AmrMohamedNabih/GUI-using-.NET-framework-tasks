using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p20
{
    public class character
    {
        public int x, y;
        public Bitmap img;
    }
    public class Line
    {
        public int x , y ;
    }
    public class twoPoints
    {
        public int x1, y1 , x2 , y2;
        public bool check;
    }
    public partial class Form1 : Form
    {
        Bitmap off;
        List<Line> lines = new List<Line>();
        List<twoPoints> points = new List<twoPoints>();
        List<character> tom = new List<character>();
        List<character> jerry = new List<character>();
        bool isDrag = false;
        bool doingOnce = false;
        int x1, y1 , x2 ,y2;
        bool TomIsMoving = false;
        Timer tt = new Timer();
        int Move = 0;
        int target = 0;
        int linex1 = 0;
        int linex2 = 0;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            tt.Start();
            tt.Tick += Tt_Tick;
            tt.Interval = 100;
         
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if(TomIsMoving)
            {
                MoveTom();
            }
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            for (int i = 0; i < lines.Count && !doingOnce; i++)
            {
                if (e.X < (90 + (100 * i)) && e.X > (110 + (100 * i)) && e.Y < 600 && e.Y > 100)
                {
                    x2 = 100 + (100 * i);
                    if(x1 < x2)
                    {
                        x1 = x2 - 100;
                    }
                    else if(x2 > x1)
                    {
                        x2 = x1 + 100;
                    }
                    for(int k =1; i <= 4; k++)
                    {
                        if (x2 > 90 * i && x2 < 110 * i)
                        {
                            x2 = i * 100;
                        }
                        if (x1 > 90 * i && x1 < 110 * i)
                        {
                            x1 = i * 100;
                        }
                    }
                    
                    points[points.Count-1].x2 = x2;
                    points[points.Count - 1].x1 = x1;
                    
                    points[points.Count - 1].y2 = y1;
                    points[points.Count - 1].y1 = y1;

                    this.Text = $"{points[points.Count - 1].x1}  ,  {points[points.Count - 1].x2}";

                    doingOnce = true;
                    DrawDubb(this.CreateGraphics());
                }
            }
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(isDrag)
            {
                if (e.X < x1)
                {
                    points[points.Count - 1].x1 = e.X;
                    points[points.Count - 1].x2 = x1;
                    points[points.Count - 1].y1 = e.Y;

                }
                else
                {
                    points[points.Count - 1].x2 = e.X;
                    points[points.Count - 1].y2 = e.Y;

                }
//

                doingOnce = true;
                DrawDubb(this.CreateGraphics());

            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
            x1 = 0;
            x2 = 0;
            y1 = 0;
            y2 = 0;
            for (int i =0; i < lines.Count && !doingOnce; i++)
            {
                if(e.X > (90 + (100*i)) && e.X < (110 + (100 * i )) && e.Y < 600 && e.Y > 100 )
                {
                    isDrag = true;
                    linex1 = 100 + (100 * i);
                    x1 = 100 + (100 * i);
                    x2 = x1;
                    y1 = e.Y;
                    y2 = e.Y;
                    twoPoints pnn = new twoPoints();
                    pnn.x1 = x1;
                    pnn.y1 = y1;
                    pnn.x2 = x2;
                    pnn.y2 = y2;
                    pnn.check = false;
                    points.Add(pnn);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    doingOnce = false;
                    TomIsMoving = false;
                    switchLines();
                    switchPoints();
                    switchCharacters();
                    break;
                case Keys.Enter:
                    TomIsMoving = true;
                    break;
                case Keys.K:
                    this.Text = $"{points[points.Count - 1].x1}  ,  {points[points.Count - 1].x2}";
                    break;
            }
            DrawDubb(this.CreateGraphics());

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Green);

            for(int i=0; i< lines.Count; i++)
            {
                Pen p = new Pen(Color.Black, 5);
                g.DrawLine(p, lines[i].x, lines[i].y , lines[i].x , lines[i].y + 500);
            }
            //this.Text = $"{points.Count}";
            for ( int i = 0; i < points.Count; i++)
            {
                Pen p = new Pen(Color.Red, 5);
                Brush b = new SolidBrush(Color.Blue);

                g.DrawLine(p, points[i].x1, points[i].y1, points[i].x2, points[i].y2);
                g.FillEllipse(b, points[i].x1 - 5, points[i].y1 - 5, 10, 10);
                g.FillEllipse(b, points[i].x2 - 5, points[i].y2 - 5, 10, 10);

            }

            for (int i = 0; i < tom.Count; i++)
            {
                g.DrawImage(tom[i].img, tom[i].x, tom[i].y,65,58);
            }
            for (int i = 0; i < jerry.Count; i++)
            {
                g.DrawImage(jerry[i].img, jerry[i].x, jerry[i].y, 84, 96);
            }
        }
        void switchLines()
        {
 
           lines.RemoveRange(0 , lines.Count);

            Random rr = new Random();
            
            for(int i = 0; i < rr.Next(2, 5); i++)
            {
                Line pnn = new Line();
                pnn.x = 100 + (100 * i);
                pnn.y = 100;
                lines.Add(pnn);
            }
            
        }
        void switchPoints()
        {
            points.RemoveRange(0, points.Count);

            Random dd = new Random();
            Random bb = new Random();
            for (int i =0; i< lines.Count-1; i++)
            {
                int limt = dd.Next(1, 4);
                for (int k =0; k < limt; k++)
                {
                    twoPoints pnn = new twoPoints();
                    pnn.x1 = lines[i].x;
                    pnn.x2 = lines[i+1].x;
                    pnn.y1 = bb.Next(150 , 490);
                    pnn.y2 = pnn.y1;
                    pnn.check = false;

                    points.Add(pnn);

                }
            }
            //this.Text = $"{points.Count}";
        }
        void switchCharacters()
        {
            tom.RemoveRange(0 , tom.Count);
            jerry.RemoveRange(0, jerry.Count);

            Random rr = new Random();
            rr.Next(1, lines.Count);
            character pnn = new character();
            pnn.x = (rr.Next(1, lines.Count) * 100) - 32;
            pnn.y = 42;
            pnn.img = new Bitmap("tom.bmp");
            pnn.img.MakeTransparent(pnn.img.GetPixel(0,0));
            tom.Add(pnn);

            rr.Next(1, lines.Count);
            pnn = new character();
            pnn.x = rr.Next(1, lines.Count) * 100 - 44;
            pnn.y = 601;
            pnn.img = new Bitmap("jerry.bmp");
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));

            jerry.Add(pnn);
        }
        void MoveTom()
        {
            if(tom[0].y >=  600 && tom[0].x >= jerry[0].x - 20 && tom[0].x <= jerry[0].x + 20)
            {
                tt.Stop();

                MessageBox.Show("win");
            }else if(tom[0].y >= 600)
            {
                tt.Stop();

                MessageBox.Show("ma winatsh");

            }
            if (Move == 0)
            {
                tom[0].y += 5;
            }
            if (Move == 1)
            {
                tom[0].x += 5;
                if(target <= tom[0].x)
                {
                    Move = 0;
                    target = 0;
                }
            }
            if (Move == -1)
            {
                tom[0].x -= 5;
                if (target >= tom[0].x)
                {
                    Move = 0;
                    target = 0;

                }

            }
            for (int i = 0; i < points.Count; i++)
            { 
                if(tom[0].x + 32 > points[i].x1 - 20 && tom[0].x + 32 < points[i].x1 + 20 && tom[0].y + 29  > points[i].y1  - 5 && tom[0].y + 29 < points[i].y1 + 5 && !points[i].check)
                {
                    Move = 1;
                    target = tom[0].x + 100;
                    points[i].check=true;
                }
                if (tom[0].x + 32 > points[i].x2 - 20 && tom[0].x + 32 < points[i].x2 + 20 && tom[0].y + 29 > points[i].y1 - 5 && tom[0].y + 29 < points[i].y1 + 5 && !points[i].check)
                {
                    Move = -1;
                    target = tom[0].x - 100;
                    points[i].check = true;

                }
            }
        }
    }
}
