using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class CAdvImgActor
    {
        public Bitmap img;
        public Rectangle rcDst;
        public Rectangle rcSrc;
    }
    public class cannon
    {
        public int x, y, width, height;
        public List<Bitmap> img;
        public int iFrame;
        public int dir;
        public int count;
    }
    public class bullet
    {
        public int x, y, width, height;
        public Bitmap img;
        public int died;
    }
    public class ball
    {
        public int x, y, width, height;
        public Color cl;
        public Bitmap img;
        public int dir;
        public int count;

    }
    public partial class Form1 : Form
    {
        Bitmap off;
        CAdvImgActor backGround = new CAdvImgActor();
        List<bullet> bullets = new List<bullet>();
        List<ball> balls = new List<ball>();


        cannon cannon = new cannon();
        bool KeysLeft = false, KeysRight = false, KeysSpace = false;
        int countToMakeBall = 0;
        Timer tt = new Timer();
        Random rr = new Random();

        public Form1()
        {
            this.Size = new Size(300, 600);
            this.Paint += Form1_Paint;
            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            tt.Start();
            tt.Tick += Tt_Tick;
            tt.Interval = 100;

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    KeysLeft = false;
                    break;
                case Keys.Right:
                    KeysRight = false;
                    break;
                case Keys.Space:
                    KeysSpace = false;
                    break;
            }
            DrawDubb(this.CreateGraphics());
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            accelerationCannon(); // to make acceleration to cannon


            moveBullets(); // to move bullets and have some conditions 

            KeysClicked(); // handle click 


            if (countToMakeBall % 20 == 0)
            {
                createBall(); // create ball with random size and random direction and random position
                countToMakeBall = 0;
            }

            countToMakeBall++;


            moveBalls();

            DrawDubb(this.CreateGraphics());
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    KeysLeft = true;
                    break;
                case Keys.Right:
                    KeysRight = true;
                    break;
                case Keys.Space:
                    KeysSpace = true;
                    break;
            }
            DrawDubb(this.CreateGraphics());

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            this.Location = new Point(550, 90);
            backGround.img = new Bitmap("backgroundColorGrass.png");
            backGround.rcSrc = new Rectangle(100, 200, 300, 600);
            backGround.rcDst = new Rectangle(0, 0, 300, 600);


            cannon.x = 100;
            cannon.y = 450;
            cannon.img = new List<Bitmap>();
            Bitmap images = new Bitmap("1Left.png");
            cannon.img.Add(images);

            images = new Bitmap("2Left.png");
            cannon.img.Add(images);
            images = new Bitmap("3Left.png");

            cannon.width = 100; cannon.height = 100;
            cannon.img.Add(images);
            cannon.iFrame = 0;
            cannon.count = 7;
            cannon.dir = 0;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            g.DrawImage(backGround.img, backGround.rcDst, backGround.rcSrc, GraphicsUnit.Pixel);

            g.DrawImage(cannon.img[cannon.iFrame], cannon.x, cannon.y, cannon.width, cannon.height);
            Pen p = new Pen(Color.Black);
            //g.DrawLine(p, cannon.x, cannon.y+ 15 , cannon.x+cannon.width, cannon.y+ 15);

            for (int i = 0; i < bullets.Count; i++)
            {

                g.DrawImage(bullets[i].img, bullets[i].x, bullets[i].y, bullets[i].width, bullets[i].height);
            }

            Brush b;
            for (int i = 0; i < balls.Count; i++)
            {
                b = new SolidBrush(balls[i].cl);
                g.FillEllipse(b, balls[i].x, balls[i].y, balls[i].width, balls[i].height);

                // g.DrawLine(p, balls[i].x, balls[i].y, balls[i].x + balls[i].width, balls[i].y);
                //g.DrawLine(p, balls[i].x, balls[i].y, balls[i].x, balls[i].y + balls[i].height);
                //g.DrawLine(p, balls[i].x, balls[i].y + balls[i].height, balls[i].x + balls[i].width, balls[i].y + balls[i].height);


                //g.DrawImage(balls[i].img, balls[i].x, balls[i].y, balls[i].width, balls[i].height);

                Font Fnt = new Font("Arial", balls[i].width / 2, FontStyle.Bold);
                g.DrawString($"{balls[i].count}", Fnt, Brushes.White, balls[i].x, balls[i].y + (balls[i].width / 5));

            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void createBullet()
        {
            bullet pnn = new bullet();
            pnn.x = cannon.x + 25;
            pnn.y = cannon.y;
            pnn.img = new Bitmap("bullet (2).png");
            pnn.width = 30;
            pnn.height = 20;
            pnn.died = 0;
            bullets.Add(pnn);

            pnn = new bullet();
            pnn.x = cannon.x + 35;
            pnn.y = cannon.y;
            pnn.img = new Bitmap("bullet (2).png");
            pnn.width = 30;
            pnn.height = 20;
            pnn.died = 0;
            bullets.Add(pnn);



            pnn = new bullet();
            pnn.x = cannon.x + 45;
            pnn.y = cannon.y;
            pnn.img = new Bitmap("bullet (2).png");
            pnn.width = 30;
            pnn.height = 20;
            pnn.died = 0;
            bullets.Add(pnn);

        }

        void accelerationCannon()
        {
            if (cannon.dir != 0)
            {
                if (cannon.dir == 1)
                {
                    cannon.x -= cannon.count;
                    cannon.count--;
                }
                else if (cannon.dir == 2)
                {
                    cannon.x += cannon.count;
                    cannon.count--;

                }
                if (cannon.count == 0)
                {
                    cannon.dir = 0;
                }

                if (cannon.iFrame == 0)
                {
                    cannon.iFrame = 1;
                }
                else
                {
                    cannon.iFrame = 0;
                }
            }
        }
        void moveBullets()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].y -= 30;
                if (bullets[i].y < 0)
                {
                    bullets.RemoveAt(i);

                }
                else
                {
                    for (int k = 0; k < balls.Count; k++)
                    {
                        this.Text = $"{balls.Count}";
                        //if ((balls[k].x < bullets[i].x + 15 && balls[k].x + balls[k].width > bullets[i].x + 15
                        //    && balls[k].y < bullets[i].y && balls[k].y + balls[k].height > bullets[i].y
                        //    ) || (bullets[i].x <= balls[k].x + balls[k].width && bullets[i].x >= balls[k].x &&
                        //    bullets[i].y <= balls[k].y + balls[k].height && bullets[i].y >= balls[k].y))

                        if (balls[k].x < bullets[i].x + 15 && balls[k].x + balls[k].width > bullets[i].x + 15
                            && balls[k].y < bullets[i].y && balls[k].y + balls[k].height > bullets[i].y
                            )
                        {
                            bullets.RemoveAt(i);
                            balls[k].count--;
                            balls[k].width -= 3;
                            balls[k].height -= 3;
                            if (balls[k].count < 1)
                            {
                                balls.RemoveAt(k);
                            }
                        }
                    }
                }

            }

        }
        void KeysClicked()
        {
            if (KeysLeft)
            {
                if (cannon.x > -10)
                {
                    cannon.x -= 5;
                    cannon.dir = 1;
                    cannon.count = 7;
                }
                if (cannon.iFrame == 0)
                {
                    cannon.iFrame = 1;

                }
                else
                {
                    cannon.iFrame = 0;
                }
            }
            if (KeysRight)
            {
                if (cannon.x < 200)
                {
                    cannon.x += 5;
                    cannon.dir = 2;
                    cannon.count = 7;

                }
                if (cannon.iFrame == 0)
                {
                    cannon.iFrame = 1;

                }
                else
                {
                    cannon.iFrame = 0;
                }
            }
            if (KeysSpace)
            {
                createBullet();
            }
        }

        void createBall()
        {
            int x, width, dir;

            x = rr.Next(0, 301);
            width = rr.Next(5, 15);
            if (x < 100)
            {
                dir = 0;
            }
            else if (x < 200)
            {
                dir = 1;
            }
            else
            {
                dir = 2;
            }
            ball pnn = new ball();
            pnn.count = width;
            pnn.x = x;
            pnn.y = -width;
            pnn.width = width * 5;
            pnn.height = width * 5;

            Bitmap img = new Bitmap("green.png");
            if (width > 8)
            {
                pnn.cl = Color.Red;
                img = new Bitmap("Red.png");
                img.MakeTransparent(img.GetPixel(0, 0));
            }
            else if (width > 5)
            {
                pnn.cl = Color.OrangeRed;
                img = new Bitmap("pink.png");
                img.MakeTransparent(img.GetPixel(0, 0));
            }
            else if (width > 3)
            {
                pnn.cl = Color.Orange;
                img = new Bitmap("yallow.png");
                img.MakeTransparent(img.GetPixel(0, 0));
            }
            else if (width > 1)
            {
                pnn.cl = Color.Yellow;
                img = new Bitmap("green.png");
                img.MakeTransparent(img.GetPixel(0, 0));
            }
            pnn.img = img;

            pnn.dir = dir;

            balls.Add(pnn);
        }
        void moveBalls()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].dir == 0)
                {
                    balls[i].x += 3;
                    balls[i].y += 10;

                }
                else if (balls[i].dir == 1)
                {
                    balls[i].y += 10;

                }
                else if (balls[i].dir == 2)
                {
                    balls[i].x -= 3;
                    balls[i].y += 10;

                }
                //check hit cannon 
                if (balls[i].x < (cannon.x + cannon.width) && (balls[i].x > cannon.x || balls[i].x + balls[i].width > cannon.x)
                    && balls[i].y < (cannon.y + cannon.height + 15) && (balls[i].y > cannon.y + 15 || balls[i].y + balls[i].height > cannon.y + 15))
                {
                    GameOver();
                }
            }

        }
        void GameOver()
        {
            tt.Stop();
        }
    }
}