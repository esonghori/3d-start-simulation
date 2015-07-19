using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace star2._2
{
    class Transformation
    {
        double xOfDist,yOfDist,zOfDist;
	    double xOfCamera,yOfCamera,zOfCamera;
	    double GetNewX(double aX,double aY,double aZ)
        {
	        double x=aX-xOfDist;
	        double y=aY-yOfDist;
	        double z=aZ-zOfDist;
	        double a,b,c,X;
	        double RO=Math.Pow(xOfCamera*xOfCamera+yOfCamera*yOfCamera+zOfCamera*zOfCamera,0.5);
	        double R=Math.Pow((xOfCamera-x)*(xOfCamera-x)+(yOfCamera-y)*(yOfCamera-y)+(zOfCamera-z)*(zOfCamera-z),0.5);
	        a=xOfCamera+(x*(yOfCamera*yOfCamera+zOfCamera*zOfCamera)/(RO*RO))-(xOfCamera*(yOfCamera*y+zOfCamera*z)/(RO*RO));
	        b=yOfCamera+(y*(xOfCamera*xOfCamera+zOfCamera*zOfCamera)/(RO*RO))-(yOfCamera*(xOfCamera*x+zOfCamera*z)/(RO*RO));
	        c=zOfCamera+(z*(xOfCamera*xOfCamera+yOfCamera*yOfCamera)/(RO*RO))-(zOfCamera*(xOfCamera*x+yOfCamera*y)/(RO*RO));
	        X=(b*xOfCamera-a*yOfCamera)/Math.Pow(xOfCamera*xOfCamera+yOfCamera*yOfCamera,0.5);
	        return 1000*X/Math.Pow(R*R-X*X,0.5);
        }
	    double GetNewY(double aX,double aY,double aZ)
        {
            double x=aX-xOfDist;
	        double y=aY-yOfDist;
	        double z=aZ-zOfDist;
	        double a,b,c,Y;
	        double RO=Math.Pow(xOfCamera*xOfCamera+yOfCamera*yOfCamera+zOfCamera*zOfCamera,0.5);
	        double R=Math.Pow((xOfCamera-x)*(xOfCamera-x)+(yOfCamera-y)*(yOfCamera-y)+(zOfCamera-z)*(zOfCamera-z),0.5);
	        a=xOfCamera+(x*(yOfCamera*yOfCamera+zOfCamera*zOfCamera)/(RO*RO))-(xOfCamera*(yOfCamera*y+zOfCamera*z)/(RO*RO));
	        b=yOfCamera+(y*(xOfCamera*xOfCamera+zOfCamera*zOfCamera)/(RO*RO))-(yOfCamera*(xOfCamera*x+zOfCamera*z)/(RO*RO));
	        c=zOfCamera+(z*(xOfCamera*xOfCamera+yOfCamera*yOfCamera)/(RO*RO))-(zOfCamera*(xOfCamera*x+yOfCamera*y)/(RO*RO));
	        Y=(a*(xOfCamera*zOfCamera)+b*(yOfCamera*zOfCamera)-c*(yOfCamera*yOfCamera+xOfCamera*xOfCamera))/Math.Pow((xOfCamera*zOfCamera)*(xOfCamera*zOfCamera)+(yOfCamera*zOfCamera)*(yOfCamera*zOfCamera)+(yOfCamera*yOfCamera+xOfCamera*xOfCamera)*(yOfCamera*yOfCamera+xOfCamera*xOfCamera),0.5);
	        return 1000*Y/Math.Pow(R*R-Y*Y,0.5);
        }

        public void Show(Star Star1,Graphics screen)
        {
            Brush brush;
            if (Star1.available())
            {
                int w, h,xx,yy;
                double x = Star1.X - xOfDist;
                double y = Star1.Y - yOfDist;
                double z = Star1.Z - zOfDist;
                if (xOfCamera * x + yOfCamera * y + zOfCamera * z < xOfCamera * xOfCamera + yOfCamera * yOfCamera + zOfCamera * zOfCamera)
                {
                    double R = Math.Pow((x - xOfCamera) * (x - xOfCamera) + (y - yOfCamera) * (y - yOfCamera) + (z - zOfCamera) * (z - zOfCamera), 0.5);
                    double M = 255 - (255 * R) / (R + 400);
                    brush = new SolidBrush(Color.FromArgb((int)M, (int)M, (int)M));
                    double X, Y;
                    X = this.GetNewX(x, y, z);
                    Y = this.GetNewY(x, y, z);
                    h = w = (int)((Math.Pow(Star1.R, 2) / Math.Pow(R * R - Math.Pow(Star1.R, 3.0 / 2), 0.5)) * 200);///2/3
                    xx = (int)(screen.VisibleClipBounds.Width/ 2 + X - w / 2);
                    yy = (int)(screen.VisibleClipBounds.Height / 2 + Y - h / 2);
                    screen.FillEllipse(brush, xx, yy, w, h);
                }
            }
        }
        public void ShowVector(Graphics screen)
        {

            int xx, xy, yx, yy, zx, zy,x0,y0;
            double RO = Math.Pow(xOfCamera * xOfCamera + yOfCamera * yOfCamera + zOfCamera * zOfCamera, 0.5);
            Pen pen = new Pen(Color.Red, 2);


            x0 = (int)(screen.VisibleClipBounds.Width / 2 + this.GetNewX(0, 0, 0));
            y0 = (int)(screen.VisibleClipBounds.Height / 2 + this.GetNewY(0, 0, 0));

            xx = (int)(screen.VisibleClipBounds.Width / 2 + this.GetNewX(RO, 0, 0));
            xy = (int)(screen.VisibleClipBounds.Height / 2 + this.GetNewY(RO, 0, 0));
            yx = (int)(screen.VisibleClipBounds.Width / 2 + this.GetNewX(0, RO, 0));
            yy = (int)(screen.VisibleClipBounds.Height / 2 + this.GetNewY(0, RO, 0));
            zx = (int)(screen.VisibleClipBounds.Width / 2 + this.GetNewX(0, 0, RO));
            zy = (int)(screen.VisibleClipBounds.Height / 2 + this.GetNewY(0, 0, RO));
            screen.DrawLine(pen, x0, y0, xx, xy);
            screen.DrawLine(pen, x0, y0, yx, yy);
            screen.DrawLine(pen, x0, y0, zx, zy);
        }
        public void setPsitionOfCamera(double xOfCamera, double yOfCamera, double zOfCamera)
        {
            this.xOfCamera = xOfCamera;
            this.yOfCamera = yOfCamera;
            this.zOfCamera = zOfCamera;
        }
        public void setPsitionOfDist(double xOfDist, double yOfDist, double zOfDist)
        {
            this.xOfDist = xOfDist - this.xOfDist;
            this.yOfDist = yOfDist - this.yOfDist;
            this.zOfDist = zOfDist - this.zOfDist;
        }
        public Transformation(double xOfCamera, double yOfCamera, double zOfCamera)
        {
            this.xOfCamera = xOfCamera;
            this.yOfCamera = yOfCamera;
            this.zOfCamera = zOfCamera;
            xOfDist = yOfDist = zOfDist = 0;
        }
    }
}
