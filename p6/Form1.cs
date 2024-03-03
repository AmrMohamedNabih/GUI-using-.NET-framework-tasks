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
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;
        bool Drag = false;
        public Form1()
        {
            this.BackColor = Color.Black;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
        }
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            this.BackColor = Color.Black;
            x = e.X;
            y = e.Y;
        }
        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(Drag)
            {

                int dx = e.X - x;
                int dy = e.Y - y;
                if (dy < 0 )
                {
                    dy *= -1;
                }
                if(dx < 0)
                {
                    dx*= -1;
                }
                this.BackColor = Color.FromArgb(0, dy%255, dx%255);

            }
        }
        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }
    }
}
