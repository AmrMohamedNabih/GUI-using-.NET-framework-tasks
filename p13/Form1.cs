using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P19
{
    public class cube
    {
        public int x, y;
        public int dir;
        public bool have;
        public Bitmap img;
    }
    public class bird
    {
        public int x, y;
        public List<Bitmap> img;
        public bool have;
        public int iframe;
    }
    public partial class Form1 : Form
    {
        Bitmap off ,imgH;
        int x , y;
        Timer tt = new Timer(); 
        bool isUp = false ;
        int counter = 0;
        List<cube> cubes = new List<cube>();
        List<bird> birds = new List<bird>();
        List<bird> havebirds = new List<bird>();
        bool keYR = false;
        bool keYl = false;
        bool keYu = false;
        bool keYd = false;



        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;

            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            tt.Start();
            tt.Tick += Tt_Tick;
            tt.Interval = 100;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                keYu = false;
            }else if (e.KeyCode == Keys.Space)
            {
                keYd = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                keYR = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                keYl = false;
            }
        }

        void Form1_Load (object sender ,EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);

            x = this.ClientSize.Width / 2;
            y = 2;
            imgH = new Bitmap("1.bmp");
            imgH.MakeTransparent(imgH.GetPixel(0,0));

            createCubes();
            createBirds();

        }
        void Form1_Paint(object sender , PaintEventArgs e)
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

            g.DrawImage(imgH, x,y,89 , 74);
            
            for(int i =0; i < cubes.Count; i++)
            {
                g.DrawImage(cubes[i].img, cubes[i].x, cubes[i].y, 78, 54);
            }
            for (int i = 0; i < birds.Count; i++)
            {
                if(cubes[i+1].have)
                {

                    g.DrawImage(birds[i].img[birds[i].iframe], birds[i].x, birds[i].y, 49, 49);
                }
            }
            for (int i = 0; i < havebirds.Count; i++)
            {
                g.DrawImage(havebirds[i].img[havebirds[i].iframe], havebirds[i].x, havebirds[i].y, 49, 49);
            }
        }
        void Tt_Tick(object sender , EventArgs e)
        {
            if(!isUp)
            {
                y+=5;
                for (int i = 0; i < havebirds.Count; i++)
                {
                    havebirds[i].y += 5;
                }
                for (int i = 0; i < birds.Count; i++)
                {
                    if (y > birds[i].y - 100 && x < birds[i].x + 25 && x > birds[i].x - 40 )
                    {
                        birds[i].iframe = 1;
                    }
                    else
                    {
                        if(!birds[i].have)
                            birds[i].iframe = 0;
                    }
                }
            }

            if(isUp)
            {
                counter++;
            }
            if(counter == 2)
            {
                isUp = false;
                counter = 0;
            }

            MoveCubes();
            MoveHeroo();
            DrawDubb(this.CreateGraphics());
        }
        void Form1_KeyDown (object sender , KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                keYu = true;
            }
            else if (e.KeyCode == Keys.Space)
            {
                keYd = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                keYR = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                keYl = true;
            }
            
            DrawDubb(this.CreateGraphics());

        }
        void createCubes()
        {
            cube pnn = new cube();
            pnn.have = true;
            pnn.x = 0;
            pnn.y = this.ClientSize.Height - 75;
            pnn.dir = 0;
            pnn.img = new Bitmap("2.bmp");
            cubes.Add(pnn);

          
            for (int i =0; i <4; i++)
            {
                pnn = new cube();
                if (i< 2)
                {   
                    pnn.dir = 1;
                }
                else
                {
                    pnn.dir = -1;
                }
                pnn.have = true;
                pnn.x = 200 * (i + 1);
                pnn.y = this.ClientSize.Height - 75;
                pnn.img = new Bitmap("2.bmp");
                cubes.Add(pnn);
            }
            pnn = new cube();
            pnn.have = true;
            pnn.x = this.ClientSize.Width - 300;
            pnn.y = this.ClientSize.Height - 75;
            pnn.dir = 0;
            pnn.img = new Bitmap("2.bmp");
            cubes.Add(pnn);
        }
        void MoveCubes()
        {
            for(int i = 1; i <cubes.Count-1; i++)
            {
                if (cubes[i].dir == 1)
                {
                    if(cubes[i].x + 78 == cubes[i+1].x)
                    {
                        cubes[i].dir = -1;
                    }
                    cubes[i].x++;
                    this.Text = $"{i} , {cubes[i].have}";
                    if (cubes[i].have)
                    {
                        birds[i - 1].x++;

                    }
                }
                else
                {
                    cubes[i].x--;
                     if(cubes[i].have)
                    {
                        birds[i - 1].x--;

                    }

                    if (cubes[i].x == cubes[i - 1].x + 78)
                    {
                        cubes[i].dir = 1;
                    }
                }
            }
        }
        void createBirds()
        {
            bird pnn;
            for (int i =0; i< 4; i ++)
            {
                pnn = new bird();
                pnn.img = new List<Bitmap>();
                Bitmap imgg = new Bitmap("3.bmp");
                imgg.MakeTransparent(imgg.GetPixel(0,0));
                pnn.img.Add(imgg);
                imgg = new Bitmap("4.bmp");
                imgg.MakeTransparent(imgg.GetPixel(0, 0));

                pnn.img.Add(imgg);
                pnn.iframe = 0;
                pnn.y = this.ClientSize.Height - (75 + 49);
                pnn.x = cubes[i + 1].x + (19);
                pnn.have = false;

                birds.Add(pnn); 
            }
        }
        void MoveHeroo()
        {
            if(keYR)
            {
                x += 3;
                for (int i = 0; i < havebirds.Count; i++)
                {
                    havebirds[i].x += 3;
                }
            }
            if(keYu)
            {
                y -= 5;
                for (int i = 0; i < havebirds.Count; i++)
                {
                    havebirds[i].y -= 5;
                }
                isUp = true;
            }
            if(keYd)
            {
                for (int i = 0; i < birds.Count; i++)
                {
                    if (y > birds[i].y - 100 && x < birds[i].x + 25 && x > birds[i].x - 40)
                    {
                        bird pnn = new bird();

                        pnn.iframe = 1;
                        pnn.have = true;
                        pnn.y = y + 74;
                        pnn.x = x + 78 - (havebirds.Count * 38);
                        pnn.img = new List<Bitmap>();
                        Bitmap imgg = new Bitmap("3.bmp");
                        imgg.MakeTransparent(imgg.GetPixel(0, 0));
                        pnn.img.Add(imgg);
                        imgg = new Bitmap("4.bmp");
                        imgg.MakeTransparent(imgg.GetPixel(0, 0));

                        pnn.img.Add(imgg);
                        havebirds.Add(pnn);
                        cubes[i + 1].have = false;
                    }
                }
            }if(keYl)
            {
                x -= 3;
                for (int i = 0; i < havebirds.Count; i++)
                {
                    havebirds[i].x -= 3;
                }
            }
                
        }
    }
}
