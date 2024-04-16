using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public class CActor
    {
        public int x, y, w, h;
        public Color cl;
    }
    public partial class Form1 : Form
    {
        List<CActor> LWnds = new List<CActor>();
        List<CActor> layers = new List<CActor>();
        List<int> L1 = new List<int>();
        List<int> L2 = new List<int>();
        List<int> L3 = new List<int>();

        int L1Y, L2Y, L3Y;
        bool Drag = false;
        int num_layer = -1;
        int num_wnd = 1;
        int xOld = -1;
        int yOld = -1;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
        }
        private void Form1_Load(object sender , EventArgs e)
        {
            L1Y = this.ClientSize.Height - 410- 40;
            L2Y = L3Y = this.ClientSize.Height - 40;
            int xMargin = 190;
            for (int i = 0; i < 3; i++)
            {
                CActor pnn = new CActor();
                pnn.x = xMargin;
                pnn.y = this.ClientSize.Height - 510;
                pnn.w = 30;
                pnn.h = 500;
                pnn.cl = Color.Gray;
                LWnds.Add(pnn);

                xMargin +=300;

            }
            for (int i = 0; i < 10; i++)
            {
                CActor pnn = new CActor();
                pnn.x = 190 - (i * 10);
                pnn.y = this.ClientSize.Height - 410 + (i * 40);
                pnn.w = 30 + (i * 20);
                pnn.h = 40;
                pnn.cl = Color.Yellow;
                
                layers.Add(pnn);
            }
            L1.Add(10000);
            L2.Add(10000);
            L3.Add(10000);
            for (int i = layers.Count-1; i > -1;i--)
            {
                L1.Add(layers[i].w);
            }
        }
        private void Form1_Paint(object sender , PaintEventArgs e)
        {
            DrawScene(e.Graphics);
        }
   
        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);
            Brush pn;
            Pen border;
            for(int i =0; i< LWnds.Count; i++)
            {
                CActor ptrav = LWnds[i];
                pn = new SolidBrush(ptrav.cl);
                border = new Pen(Color.Black,5);
                g.FillRectangle (pn, ptrav.x, ptrav.y, ptrav.w, ptrav.h);
                g.DrawRectangle(border, ptrav.x, ptrav.y, ptrav.w, ptrav.h);
            }
            for(int i =0; i< 10; i++)
            {
                CActor ptrav = layers[i];
                pn = new SolidBrush(ptrav.cl);
                border = new Pen(Color.Black, 5);
                g.FillRectangle(pn, ptrav.x, ptrav.y, ptrav.w, ptrav.h);
                g.DrawRectangle(border, ptrav.x, ptrav.y, ptrav.w, ptrav.h);

            }
        }
        void Form1_MouseDown(object sender , MouseEventArgs e)
        {
            bool check = false;
            num_layer = whichLayer(e.X ,e.Y);
            if(num_layer != -1)
            {
            
                num_wnd = whichWind(e.X, e.Y);
                switch (num_wnd)
                {
                    case 1:
                        if (layers[num_layer].y == L1Y + 40)
                        {
                            check = true;
                        }
                        break;
                    case 2:
                        if (layers[num_layer].y <= L2Y + 40)
                        {
                            check = true;
                        }
                        break;
                    case 3:
                        if (layers[num_layer].y == L3Y + 40)
                        {
                            check = true;
                        }
                        break;
                }
                if (check)
                {
                    xOld = e.X;
                    yOld = e.Y;
                    Drag = true;
                }
                else
                    num_layer = -1;
               

            }
        }
        int whichLayer(int x , int y)
        {

            for (int i = 0; i < layers.Count; i++)
            {
                CActor ptrav = layers[i];
                if (x >= ptrav.x && x <= (ptrav.x + ptrav.w)
                    && y >= ptrav.y && y <= (ptrav.y + ptrav.h)
                    )
                {
                    return i;
                }
            }

            return -1;
        }
        int whichWind(int x, int y)
        {

            if (x < 490)
                return 1;
            else if (x < 790)
                return 2;
            else
                return 3;
        }
        void Form1_MouseUp (object sender , MouseEventArgs e)
        {
            Drag = false;
           // this.Text = $"{layers[num_layer].y} , {L1Y}";
            if (num_layer != -1)
            {
                if (e.X > 190 && e.X < 220 && num_wnd != 1 && layers[num_layer].w < L1[L1.Count-1])
                {
                    layers[num_layer].x = 190 - (num_layer * 10);
                    layers[num_layer].y = L1Y;
                    L1Y -= 40;
                    L1.Add(layers[num_layer].w);
                    DeleteLeyer(num_wnd);
                }
                else if(e.X > 490 && e.X < 520 && num_wnd != 2 && layers[num_layer].w < L2[L2.Count - 1])
                {
                    layers[num_layer].x = 490 - (num_layer * 10);
                    layers[num_layer].y = L2Y;
                    L2Y -= 40;
                    L2.Add(layers[num_layer].w);
                    DeleteLeyer(num_wnd);
                }
                else if (e.X > 790 && e.X < 820 && num_wnd != 3 && layers[num_layer].w < L3[L3.Count - 1])
                {
                    layers[num_layer].x = 790 - (num_layer * 10);
                    layers[num_layer].y = L3Y;
                    L3Y -= 40;
                    L3.Add(layers[num_layer].w);
                    DeleteLeyer(num_wnd);
                }
                else
                {
                    int x = 0, y = 0;
                    switch(num_wnd)
                    {
                        case 1:
                            x = 190;
                            y = L1Y + 40;
                            
                            break;
                        case 2:
                            x = 490;
                            y = L2Y + 40;
                            
                            break;
                        case 3:
                            x = 790;
                            y = L3Y + 40;
                            
                            break;
                    }
                    layers[num_layer].x = x - (num_layer * 10);
                    layers[num_layer].y = y;
                }
                
                DrawScene(this.CreateGraphics());
                num_wnd = 0;
                num_layer = -1;
            }
        }
        void DeleteLeyer(int win)
        {
            switch (win)
            {
                case 1:
                    L1Y += 40;
                    L1.RemoveAt(L1.Count - 1);
                    break;
                case 2:
                    L2Y += 40;
                    L2.RemoveAt(L2.Count - 1);
                    break;
                case 3:
                    L3Y += 40;
                    L3.RemoveAt(L3.Count - 1);
                    break;
            }
        }
        void Form1_MouseMove(object sender , MouseEventArgs e)
        {
            if (Drag && num_layer != -1)
            {
                int dx = e.X - xOld;
                int dy = e.Y - yOld;

                layers[num_layer].x += dx;
                layers[num_layer].y += dy;

                xOld = e.X;
                yOld = e.Y;

                DrawScene(this.CreateGraphics());
            }
        }
    }
}
