using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.MouseMove += Form1_MouseMove;
        }
        private void Form1_MouseMove(object sender , MouseEventArgs e)
        {
            if(e.Location.X <= this.Width/4)
            {
                this.BackColor = Color.Red;
            }
            else if (e.Location.X <= this.Width/3)
            {
                this.BackColor = Color.Green;
            }else  if(e.Location.X <=this.Width/2)
            {
                this.BackColor = Color.Blue;
            }
            else
            {
                this.BackColor= Color.Yellow;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
