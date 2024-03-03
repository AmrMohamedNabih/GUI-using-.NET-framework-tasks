using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p3
{
    
    public partial class Form1 : Form
    {
        public class node
        {
            public int Y;
            public int X;
        }
        private int flagLevel = 0;
        private int level_y = 0;
        private List<node> up = new List<node>();
        private List<node> down = new List<node>();
        public Form1()
        {
            this.MouseDown += Form1_MouseDown;
        }
        private void Form1_MouseDown(object sender , MouseEventArgs e)
        {
            
            if(flagLevel == 0)
            {
                if(e.Button == MouseButtons.Left)
                {
                    flagLevel = 1;
                    level_y = e.Y;
                    this.Text = $"{level_y}";
                }
            }
            else
            {
                
                if (e.Button == MouseButtons.Left)
                {
                    if(flagLevel == 1)
                    {
                        if(e.Y <= level_y)
                        {
                            node pnn = new node();
                            pnn.X = e.X;
                            pnn.Y = e.Y;
                            up.Add(pnn);
                            flagLevel = 2;
                        }
                        else
                        {
                            MessageBox.Show("Error!!!");
                        }
                    }else if(flagLevel == 2)
                    {
                        if (e.Y >= level_y)
                        {
                            node pnn = new node();
                            pnn.X = e.X;
                            pnn.Y = e.Y;
                            down.Add(pnn);
                            flagLevel = 1;
                        }
                        else
                        {
                            MessageBox.Show("Error!!!");
                        }
                    }

                }
                else
                {
                    for(int i =-1;i < up.Count ;i++)
                    {
                        if( i==-1)
                        {
                            MessageBox.Show("up");
                        }
                        else
                        {
                            MessageBox.Show($"{up[i].X} , {up[i].Y}");
                        }
                    }
                    for (int i = -1; i < down.Count; i++)
                    {
                        if (i == -1)
                        {
                            MessageBox.Show("down");
                        }
                        else
                        {
                            MessageBox.Show($"{down[i].X} , {down[i].Y}");
                        }
                    }
                }
            }
        }
    }
}
