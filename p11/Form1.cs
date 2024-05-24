using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace p17
{
    public class Star
    {
        public int x;
        public int y;
        public int inc;
        public int di;

        public Bitmap img;
    }
    public class laser
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;

    }
    public partial class Form1 : Form
    {
        Bitmap off;
        int xRocket, yRocket;
        Bitmap Rocket = new Bitmap("1.bmp");
        bool acc = false;
        int dir = 0;
        Timer tt = new Timer();
        int createTime = 0;
        int count = 9;

        List<Star> Stars = new List<Star>();
        List<laser> lasers = new List<laser>();


        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.Load += Form1_Load;
            tt.Interval = 100;
            tt.Start();
            tt.Tick += Tt_Tick;
            this.KeyDown += Form1_KeyDown;

        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    xRocket += 8;
                    count = 9;
                    acc = true;
                    dir = 1;
                    break;
                case Keys.Left:
                    xRocket -= 8;
                    count = 9;
                    acc = true;
                    dir = -1;
                    break;
                case Keys.Space:
                    laser pnn = new laser();
                    pnn.x1 = xRocket + 85;
                    pnn.y1 = yRocket- 10;
                    pnn.x2 = xRocket + 85;
                    pnn.y2 = 0;
                    lasers.Add(pnn);
                    break;
            }
        }
        private void Tt_Tick(object sender, EventArgs e)
        {
            if (createTime % 25 == 0)
                createStars();
            for (int i = 0; i < Stars.Count; i++)
            {
                Stars[i].y += 6;
                if (Stars[i].di == 1)
                {
                    Stars[i].x += 3;
                    Stars[i].inc++;
                    if (Stars[i].inc == 5)
                    {
                        Stars[i].di = -1;
                    }
                }
                else
                {
                    Stars[i].x -= 3;
                    Stars[i].inc--;
                    if (Stars[i].inc == -5)
                    {
                        Stars[i].di = 1;
                    }
                }
            }
            createTime++;

            for (int i = 0; i < lasers.Count; i++)
            {
                lasers[i].y1 -= 100;
                lasers[i].y2 -= 100;
               
                for (int k = 0; k < Stars.Count; k++)
                {
                    if (lasers[i].x1 > Stars[k].x && lasers[i].x1 < Stars[k].x + 50
                       && lasers[i].y2 <= Stars[k].y)
                    {
                        Stars.RemoveAt(k);
                        //
                    }
                }

            }
            for (int i = 0; i < lasers.Count; i++)
            {
                if (lasers[i].y1 < 0)
                {
                    lasers.RemoveAt(i);
                }
            }
            if(acc)
            {
                if(count > 0)
                {
                    if (dir == 1)
                    {
                        xRocket+=count;
                    }
                    else
                    {
                        xRocket-=count;
                    }
                    count--;
                }

 
            }
            DrawDubb(this.CreateGraphics());
        }
        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            Rocket.MakeTransparent(Rocket.GetPixel(0, 0));
            xRocket = this.ClientSize.Width / 2 - 100;
            yRocket = this.ClientSize.Height - 200;

        }
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        private void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);

            g.DrawImage(Rocket, xRocket, yRocket, 170, 170);

            for (int i = 0; i < Stars.Count; i++)
            {
                g.DrawImage(Stars[i].img, Stars[i].x, Stars[i].y, 50, 50);
            }
            for (int i = 0; i < lasers.Count; i++)
            {
                Pen p = new Pen(Color.Yellow, 3);

                g.DrawLine(p, lasers[i].x1, lasers[i].y1, lasers[i].x2, lasers[i].y2);

            }
        }

        void createStars()
        {
            Random rr = new Random();

            Star pnn = new Star();
            pnn.inc = 0;
            pnn.di = 1;
            pnn.x = rr.Next(100, this.ClientSize.Width - 100);
            pnn.y = 0;
            pnn.img = new Bitmap("2.bmp");
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
            Stars.Add(pnn);

        }
    }
}