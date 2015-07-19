using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace star2._2
{
    public partial class main_form : Form
    {
        private Random rand;
        private int i, j;
        private double x, y, z, ro ,teta, phi;
        private bool quit, change, right, left, up, down, zoomu, zoomb;
        private int temp=0;
        /////////////////////////////
        private int M32Size = 100;
        private Star[] M32;
        private Transformation T;
        private Graphics screen;
        public main_form()
        {
            InitializeComponent();
            ////
            timer.Interval = 1;
            ////
            screen = picbox.CreateGraphics();
            screen.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            screen.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            screen.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            rand = new Random();
            ro = 500;
            teta = 3.1415 / 4;
            phi = 3.1415 / 4;
            x = ro * Math.Sin(phi) * Math.Cos(teta);
            y = ro * Math.Sin(phi) * Math.Sin(teta);
            z = ro * Math.Cos(phi);
            quit =  change = false;
            right = left = false;
            up =  down = false;
            zoomu =  zoomb = false;
            /////////////////////////////
            M32Size = 20;
            M32 = new Star[M32Size];

            M32[0] = new Star();
            M32[0].setPosition(0, 0, 0, 0, 0, 0, 5000);
            for (i = 1; i < M32Size; i++)
            {
                double khi = 2*Math.PI * i / (M32Size-1);

                int xx = rand.Next() % 300 - 150;
                int yy = rand.Next() % 300 - 150;
                int zz = 0;// rand.Next() % 300 - 150;
                double Ro = Math.Pow(xx * xx + yy * yy + zz * zz, 0.5);
                double V = Math.Pow(Star.GERAVITY * 5000 / Ro, 0.5);
                double Vec = 1.1*Math.Pow(xx * xx + yy * yy, 0.5);
                double vx = -yy * V / Vec;
                double vy = xx * V / Vec;
                M32[i] = new Star();
                M32[i].setPosition(xx , yy, zz, vx, vy, 0, 200);
            }
            ////////////////////////////
            T = new Transformation(x, y, z);
        }
        
        private void start_btn_Click(object sender, EventArgs e)
        {
            if(timer.Enabled)
            {
                start_btn.Text = "Start";
                timer.Enabled = false;
                timer.Stop();
            }
            else
            {
                start_btn.Text = "Stop";
                timer.Enabled = true;
                timer.Start();
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
		    ///////////
		    for(i=0;i<M32Size;i++)
		    {
			    for(j=0;j<M32Size;j++)
			    {
				    M32[i].impact(M32[j]);
			    }
		    }
		    //////////////
		    for(i=0;i<M32Size;i++)
		    {
			    M32[i].Clock();
		    }
            /////////////
            if (temp==0)
            {
                screen.Clear(Color.Black);
                temp = 5;
            }
            else
                temp--;
            //T.ShowVector(screen);
		    for(i=0;i<M32Size;i++)
		    {
                SortedDictionary<Star, int> I = new SortedDictionary<Star, int>();
			    T.Show(M32[i],screen);
		    }
 		    /////////////////
            if (right)
            {
                teta += 3.14 / 1000;
                change = true;
            }
            else if (left)
            {
                teta -= 3.14 / 1000;
                change = true;
            }
            if (up && phi > 0)
            {
                phi -= 3.14 / 1000;
                change = true;
            }
            else if (down && phi < 3.1415)
            {
                phi += 3.14 / 1000;
                change = true;
            }
            if (zoomb)
            {
                ro = 100 * ro / 99;
                change = true;
            }
            else if (zoomu)
            {
                ro = 99 * ro / 100;
                change = true;
            }
		    if(change)
		    {
                x = ro * Math.Sin(phi) * Math.Cos(teta);
                y = ro * Math.Sin(phi) * Math.Sin(teta);
                z = ro * Math.Cos(phi);
			    T.setPsitionOfCamera(x,y,z);
			    change=false;
		    }
		    T.setPsitionOfDist(0,0,0);
        }

        private void main_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.D)
            {
                right = true;
            }
            else if (e.KeyCode == Keys.A)
            {
                left = true;
            }
            else if (e.KeyData == Keys.W)
            {
                up = true;
            }
            else if (e.KeyCode == Keys.S)
            {
                down = true;
            }
            else if (e.KeyCode == Keys.X)
            {
                zoomb = true;
            }
            else if (e.KeyCode == Keys.Z)
            {
                zoomu = true;
            }
        }

        private void main_form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                right = false;
            }
            else if (e.KeyCode == Keys.A)
            {
                left = false;
            }
            else if (e.KeyCode == Keys.W)
            {
                up = false;
            }
            else if (e.KeyCode == Keys.S)
            {
                down = false;
            }
            else if (e.KeyCode == Keys.X)
            {
                zoomb = false;
            }
            else if (e.KeyCode == Keys.Z)
            {
                zoomu = false;
            }
        }
    }
}
