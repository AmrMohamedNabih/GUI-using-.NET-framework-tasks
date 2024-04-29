using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15
{
        public class Data
    {
        public int X, Y;
        public Bitmap im;
    }

    public partial class Form1 : Form
    {
        private List<Data> Baskets = new List<Data>();
        private List<Data> Eggs = new List<Data>();
        private List<Data> L1 = new List<Data>();
        private List<Data> L2 = new List<Data>();
        private List<Data> L3 = new List<Data>();

        private Data chicken = new Data();
        private bool isDrag = false;
        private int xOld, yOld;
        private int selected = -1;

        private Bitmap off;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            //this.Text = $"{Baskets[0].X} amr ";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBaskets();
            CreateChicken();
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

        private void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            for (int i = 0; i < Baskets.Count; i++)
            {
                Data ptrav = Baskets[i];
                g.DrawImage(ptrav.im, ptrav.X, ptrav.Y, 80, 50);
            }
            g.DrawImage(chicken.im, chicken.X, chicken.Y, 50, 70);

            for (int i = 0; i < Eggs.Count; i++)
            {
                Data ptrav = Eggs[i];
                g.DrawImage(ptrav.im, ptrav.X, ptrav.Y, 10, 15);
            }
        }

        private void CreateChicken()
        {
            chicken.X = (this.ClientSize.Width) * 1 / 2;
            chicken.Y = (this.ClientSize.Height) * 1 / 8;
            chicken.im = new Bitmap("1.bmp");
            chicken.im.MakeTransparent(chicken.im.GetPixel(0, 0));
        }

        private void CreateBaskets()
        {
            for (int i = 0; i < 3; i++)
            {
                Data pnn = new Data();
                pnn.X = 350 * (i + 1);
                pnn.Y = (this.ClientSize.Height) * 3 / 4;
                pnn.im = new Bitmap("2.bmp");
                pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
                Baskets.Add(pnn);
            }
        }

        private void CreateEgg()
        {
            Data egg = new Data();
            egg.X = chicken.X + 20;
            egg.im = new Bitmap("3.bmp");
            egg.im.MakeTransparent(egg.im.GetPixel(0, 0));

            if (egg.X >= 350 && egg.X <= 430)
            {
                egg.Y = Baskets[0].Y - 5;
                L1.Add(egg);
            }
            else if (egg.X >= 700 && egg.X <= 780)
            {
                egg.Y = Baskets[1].Y - 5;
                L2.Add(egg);
            }
            else if (egg.X >= 1050 && egg.X <= 1130)
            {
                egg.Y = Baskets[2].Y - 5;
                L3.Add(egg);
            }
            else
            {
                egg.Y = this.ClientSize.Height - 5;
            }

            Eggs.Add(egg);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    chicken.X += 5;
                    break;

                case Keys.Left:
                    chicken.X -= 5;
                    break;

                case Keys.Enter:
                    CreateEgg();
                    break;
            }
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < Baskets.Count; i++)
                {
                    if (e.X > Baskets[i].X && e.X < Baskets[i].X + 80
                        && e.Y > Baskets[i].Y && e.Y < Baskets[i].Y + 50)
                    {
                        selected = i;
                        xOld = e.X;
                        yOld = e.Y;
                        isDrag = true;
                    }
                }
            }

            DrawDubb(this.CreateGraphics());
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag == true)
            {
                if (selected != -1)
                {
                    int dx = e.X - xOld;
                    int dy = e.Y - yOld;

                    Baskets[selected].X += dx;
                    Baskets[selected].Y += dy;

                    if (selected == 0)
                    {
                        if (L1.Count > 0)
                        {
                            for (int k = 0; k < L1.Count; k++)
                            {
                                L1[k].X += dx;
                                L1[k].Y += dy;
                            }
                        }
                    }
                    else if (selected == 1)
                    {
                        if (L2.Count > 0)
                        {
                            for (int k = 0; k < L2.Count; k++)
                            {
                                L2[k].X += dx;
                                L2[k].Y += dy;
                            }
                        }
                    }
                    else if (selected == 2)
                    {
                        if (L3.Count > 0)
                        {
                            for (int k = 0; k < L3.Count; k++)
                            {
                                L3[k].X += dx;
                                L3[k].Y += dy;
                            }
                        }
                    }

                    xOld = e.X;
                    yOld = e.Y;
                }
            }

            DrawDubb(this.CreateGraphics());
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            selected = -1;
            DrawDubb(this.CreateGraphics());
        }
    }
}
