using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;
        int ct = 1;
        List<Form1> L = new List<Form1>();
        public int dx= 0;
        public int dy= 0;

        public Form1()
        {
            this.BackColor = Color.Black;
            this.MouseDown += Form1_MouseDown;
        }
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ct == 1)
                {
                    Form1 pnn = new Form1();
                    pnn.Show();
                    pnn.BackColor = Color.Red;
                    pnn.Location = new Point(this.Location.X - this.ClientSize.Width - 10, this.Location.Y - this.ClientSize.Width- 10);
                    pnn.dx = 1;
                    pnn.dy = 0;
                    L.Add(pnn);
                }
                if (ct == 2)
                {
                    Form1 pnn = new Form1();
                    pnn.Show();
                    pnn.BackColor = Color.Yellow;
                    pnn.Location = new Point(this.Location.X - this.ClientSize.Width - 10, this.Location.Y + this.ClientSize.Width + 10);
                    pnn.dx = 0;
                    pnn.dy = -1;
                    L.Add(pnn);
                }
                if (ct == 3)
                {
                    Form1 pnn = new Form1();
                    pnn.Show();
                    pnn.BackColor = Color.Orange;
                    pnn.Location = new Point(this.Location.X + this.ClientSize.Width + 10, this.Location.Y - this.ClientSize.Width - 10);
                    pnn.dx = 0;
                    pnn.dy = 1;
                    L.Add(pnn);
                }
                if (ct == 4)
                {
                    Form1 pnn = new Form1();
                    pnn.Show();
                    pnn.BackColor = Color.Blue;
                    pnn.Location = new Point(this.Location.X + this.ClientSize.Width + 10, this.Location.Y + this.ClientSize.Width + 10);
                    pnn.dx = -1;
                    pnn.dy = 0;
                    L.Add(pnn);
                }

                ct++;
            }
            if(e.Button == MouseButtons.Right)
            {
                
                if (true)
                {
                    for(int i =0; ; i++)
                    {
                        //this.Text = "la";
                        
                        for(int k =0; k < 4; k++)
                        {
                            if (L[k].dx == 1)
                            {
                                L[k].Location = new Point(L[k].Location.X + 1, L[k].Location.Y);
                                this.Text = $"no{this.Location.X *2}";
                                if(L[k].Location.X == this.Location.X + L[k].ClientSize.Width)
                                {
                                    L[k].dx = 0;
                                    L[k].dy = 1;
                                }
                            }
                            else if (L[k].dx == -1)
                            {
                                L[k].Location = new Point(L[k].Location.X - 1, L[k].Location.Y);
                                this.Text = $"no{this.Location.X * 2}";
                                if (L[k].Location.X == this.Location.X- L[k].ClientSize.Width)
                                {
                                    L[k].dx = 0;
                                    L[k].dy = -1;
                                }
                            }
                            else if (L[k].dy == 1)
                            {
                                L[k].Location = new Point(L[k].Location.X, L[k].Location.Y + 1);
                                this.Text = $"no{this.Location.X * 2}";
                                if (L[k].Location.Y == this.Location.Y + L[k].ClientSize.Height)
                                {
                                    L[k].dx = -1;
                                    L[k].dy = 0;
                                }
                            }
                            else if (L[k].dy == -1)
                            {
                                L[k].Location = new Point(L[k].Location.X, L[k].Location.Y - 1);
                                this.Text = $"no{this.Location.X * 2}";
                                if (L[k].Location.Y == this.Location.Y - L[k].ClientSize.Height)
                                {
                                    L[k].dx = 1;
                                    L[k].dy = 0;
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
