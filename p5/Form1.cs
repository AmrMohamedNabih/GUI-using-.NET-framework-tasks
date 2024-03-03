using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;
        int ct = 1;
        bool Drag = false;
        List<Form1> L = new List<Form1>();
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
            x = e.X;
            y = e.Y;
        }
        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                int dy = e.Y - y;
                for (int i=0;i<L.Count;i++)
                {
                    L[i].Location = new Point(L[i].Location.X, L[i].Location.Y + dy);
                }
                    x = e.X;
                    y = e.Y;
            }
        }
        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
            if (e.Button == MouseButtons.Right)
            {
                if(ct == 1)
                { 
                    for (int i = 0; i < 4; i++)
                    {
                        Form1 pnn = new Form1();
                        pnn.Show();
                        pnn.Size = new Size(200, 100);
                        pnn.BackColor = Color.Orange;
                        pnn.Location = new Point(this.Location.X - this.ClientSize.Width, this.Location.Y - 50 + i * 110);
                        L.Add(pnn);
                    }
                }
                if (ct == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Form1 pnn = new Form1();
                        pnn.Show();
                        pnn.Size = new Size(200, 100);
                        pnn.BackColor = Color.Red;
                        pnn.Location = new Point(this.Location.X + this.ClientSize.Width + 20, this.Location.Y - 50 + i * 110);
                        L.Add(pnn);
                    }
                }
                ct++;

               

            }
        }
    }
}
