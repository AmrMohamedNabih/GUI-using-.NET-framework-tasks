using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _14
{
 
        public class Mickey
        {
            public int X, Y;
            public List<Bitmap> img;
            public int iFrame;
        }
        public class Egg
        {
            public int X, Y;
            public List<Bitmap> im;
            public int iFrame;
            public int diraction;
        }
        public class Coin
        {
            public int X, Y;
            public Bitmap im;
            public int lineX, lineY;
            public int lineH, lineW;
        }

        public partial class Form1 : Form
        {
            Bitmap off;
            Mickey mickey = new Mickey();
            List<Egg> Eggs = new List<Egg>();
            List<Coin> Coins = new List<Coin>();
            public Form1()
            {
                this.WindowState = FormWindowState.Maximized;
                this.Paint += Form1_Paint;
                this.Load += Form1_Load;
                this.KeyDown += Form1_KeyDown;
                this.MouseDown += Form1_MouseDown;
            }
            void Form1_Load(object sender, EventArgs e)
            {
                off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                mickey.img = new List<Bitmap>();
                CreateMickey();
                CreateEggs();
            }
            void Form1_Paint(object sender, PaintEventArgs e)
            {
                DrawDubb(e.Graphics);
            }
            void CreateEggs()
            {
                Random random = new Random();


                for (int i = 0; i < 4; i++)
                {
                    Egg pnn = new Egg();
                    switch (i)
                    {
                        case 0:
                            pnn.X = random.Next(0, this.ClientSize.Width / 2 - 150);
                            pnn.Y = this.ClientSize.Height / 2 - 66;
                            pnn.iFrame = 2;
                            pnn.diraction = 0;
                            break;
                        case 1:
                            pnn.X = random.Next(this.ClientSize.Width / 2 + 200, this.ClientSize.Width - 35);
                            pnn.Y = this.ClientSize.Height / 2 - 66;
                            pnn.iFrame = 2;
                            pnn.diraction = 1;
                            break;
                        case 2:
                            pnn.X = random.Next(0, this.ClientSize.Width / 2 - 150);
                            pnn.Y = this.ClientSize.Height / 2 - 16;
                            pnn.iFrame = 2;
                            pnn.diraction = 0;
                            break;
                        case 3:
                            pnn.X = random.Next(this.ClientSize.Width / 2 + 200, this.ClientSize.Width - 35);
                            pnn.Y = this.ClientSize.Height / 2 - 16;
                            pnn.iFrame = 2;
                            pnn.diraction = 1;
                            break;
                    }
                    pnn.im = new List<Bitmap>();
                    for (int k = 0; k < 3; k++)
                    {
                        Bitmap img = new Bitmap("e" + (k + 1) + ".bmp");
                        img.MakeTransparent(img.GetPixel(0, 0));
                        pnn.im.Add(img);
                    }
                    Eggs.Add(pnn);
                }
            }
            private void CreateMickey()
            {
                mickey.X = ((this.ClientSize.Width) * 1 / 2) - 50;
                mickey.Y = ((this.ClientSize.Height) * 1 / 2) - 55;
                mickey.iFrame = 0;
                for (int k = 0; k < 4; k++)
                {
                    Bitmap img = new Bitmap((k + 1) + ".bmp");
                    img.MakeTransparent(img.GetPixel(0, 0));
                    mickey.img.Add(img);
                }
            }
            void CreateCoins()
            {
                Random rr = new Random();


                for (int i = 0; i < 4; i++)
                {
                    Coin pnn = new Coin();
                    Bitmap img = new Bitmap("c" + (i + 1) + ".bmp");
                    img.MakeTransparent(img.GetPixel(0, 0));
                    pnn.im = img;
                    switch (i)
                    {
                        case 0:
                            pnn.X = rr.Next(0, this.ClientSize.Width / 2 - 150);
                            pnn.Y = this.ClientSize.Height / 2 - 270;
                            pnn.lineY = this.ClientSize.Height / 2 - 200;
                            break;
                        case 1:
                            pnn.X = rr.Next(this.ClientSize.Width / 2 + 200, this.ClientSize.Width - 35); ;
                            pnn.Y = this.ClientSize.Height / 2 - 270;
                            pnn.lineY = this.ClientSize.Height / 2 - 200;
                            break;
                        case 2:
                            pnn.X = rr.Next(0, this.ClientSize.Width / 2 - 150);
                            pnn.Y = this.ClientSize.Height / 2 + 170;
                            pnn.lineY = this.ClientSize.Height / 2 + 20;
                            break;
                        case 3:
                            pnn.X = rr.Next(this.ClientSize.Width / 2 + 200, this.ClientSize.Width - 35);
                            pnn.Y = this.ClientSize.Height / 2 + 170;
                            pnn.lineY = this.ClientSize.Height / 2 + 20;
                            break;
                    }

                    pnn.lineX = pnn.X + 70 / 2;

                    pnn.lineH = 150;
                    pnn.lineW = 2;
                    Coins.Add(pnn);

                }

            }

            private void DrawDubb(Graphics g)
            {
                Graphics g2 = Graphics.FromImage(off);
                DrawScene(g2);
                g.DrawImage(off, 0, 0);
            }
            private void DrawScene(Graphics g)
            {
                g.Clear(Color.Goldenrod);
                Pen p = new Pen(Color.DarkRed);
                Brush b = new SolidBrush(Color.DarkRed);
               // g.DrawRectangle(p, 0, (this.ClientSize.Height * 1 / 2) - 50, (this.ClientSize.Width * 1 / 2) - 50, 20);
                g.FillRectangle(b, 0, (this.ClientSize.Height * 1 / 2) - 50, (this.ClientSize.Width * 1 / 2) - 50, 20);
                g.DrawRectangle(p, (this.ClientSize.Width * 1 / 2) + 50, (this.ClientSize.Height * 1 / 2) - 50, this.ClientSize.Width, 20);
                g.FillRectangle(b, (this.ClientSize.Width * 1 / 2) + 50, (this.ClientSize.Height * 1 / 2) - 50, this.ClientSize.Width, 20);
                ////////////
                g.DrawRectangle(p, 0, (this.ClientSize.Height * 1 / 2), (this.ClientSize.Width * 1 / 2) - 50, 20);
                g.FillRectangle(b, 0, (this.ClientSize.Height * 1 / 2), (this.ClientSize.Width * 1 / 2) - 50, 20);
                g.DrawRectangle(p, (this.ClientSize.Width * 1 / 2) + 50, (this.ClientSize.Height * 1 / 2), this.ClientSize.Width, 20);
                g.FillRectangle(b, (this.ClientSize.Width * 1 / 2) + 50, (this.ClientSize.Height * 1 / 2), this.ClientSize.Width, 20);

                g.DrawImage(mickey.img[mickey.iFrame], mickey.X, mickey.Y, 90, 90);


                for (int i = 0; i < Eggs.Count; i++)
                {
                    Egg ptrav = Eggs[i];
                    g.DrawImage(ptrav.im[ptrav.iFrame % 3], ptrav.X, ptrav.Y);
                }
                for (int i = 0; i < Coins.Count; i++)
                {
                    Coin ptrav = Coins[i];
                    g.DrawImage(ptrav.im, ptrav.X, ptrav.Y);
                    g.DrawRectangle(new Pen(Color.Green), Coins[i].lineX, Coins[i].lineY, Coins[i].lineW, Coins[i].lineH);
                    g.FillRectangle(new SolidBrush(Color.Green), Coins[i].lineX, Coins[i].lineY, Coins[i].lineW, Coins[i].lineH);
                }
            }

            ///////
            ///

            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        mickey.iFrame = 1;
                        break;
                    case Keys.Down:
                        mickey.iFrame = 2;
                        break;
                    case Keys.Left:
                        mickey.iFrame = 3;
                        break;
                    case Keys.Right:
                        mickey.iFrame = 0;
                        break;
                    case Keys.Space:
                        for (int i = 0; i < Eggs.Count; i++)
                        {
                            if (Eggs[i].diraction == 0)
                                Eggs[i].X += 5;
                            else
                                Eggs[i].X -= 5;
                            Eggs[i].iFrame++;
                            if (Eggs[i].X > (this.ClientSize.Width * 1 / 2) - 50
                                && Eggs[i].X < (this.ClientSize.Width * 1 / 2) + 50)
                            {
                                MessageBox.Show("one egg righs monkey");
                                Eggs.RemoveRange(0, Eggs.Count);
                                CreateCoins();
                            }
                        }

                        break;
                }
                DrawDubb(this.CreateGraphics());
            }
            private void Form1_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < Coins.Count; i++)
                    {

                        if (e.X > Coins[i].X && e.X < (Coins[i].X + 90) && e.Y > Coins[i].Y && e.Y < (Coins[i].Y + 150))
                        {
                            Egg egg = new Egg();
                            egg.X = Coins[i].X;
                            egg.im = new List<Bitmap>();
                            for (int k = 0; k < 3; k++)
                            {
                                Bitmap img = new Bitmap("e" + (k + 1) + ".bmp");
                                img.MakeTransparent(img.GetPixel(0, 0));
                                egg.im.Add(img);
                            }
                            if (i == 0 || i == 1)
                            {
                                egg.Y = this.ClientSize.Height / 2 - 66;

                            }
                            else if (i == 2 || i == 3)
                            {
                                egg.Y = this.ClientSize.Height / 2 - 16;

                            }

                            Eggs.Add(egg);
                            Random rr = new Random();
                            if (i % 2 == 0)
                            {
                                Coins[i].X = rr.Next(0, this.ClientSize.Width / 2 - 150);
                                Coins[i].lineX = Coins[i].X + 70 / 2;

                            }
                            else
                            {
                                Coins[i].X = rr.Next(this.ClientSize.Width / 2 + 200, this.ClientSize.Width - 35);
                                Coins[i].lineX = Coins[i].X + 70 / 2;

                            }

                            break;
                        }
                    }
                }

                DrawDubb(this.CreateGraphics());

            }
        }
    }


