using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p2
{
    public partial class Form1 : Form
    {
        private int ct = 9;
        private int flag = 1;
        public Form1()
        {
            this.MouseDown += Form1_MouseDown;
        }
        private void Form1_MouseDown(object sender , MouseEventArgs e)
        {
            if(ct == 9)
            {
                flag = 1;
            }else if(ct == 0)
            {
                flag = 0;
            }
            if(flag == 0)
            {
                ct++;
                this.Opacity += 0.1;
            }
            else if(flag == 1)
            {
                ct--;
                this.Opacity -= 0.1;
            }
        }
    }
}
