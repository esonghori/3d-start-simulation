using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace star2._2
{
    class Star : IComparable
    {
        public static int FREQ= 50;
        public static int MIN_R=2;
        public static int GERAVITY = 1;
        double mass;
	    double x;
	    double y;
	    double z;
	    double vx;
	    double vy;
	    double vz;
	    double ax;
	    double ay;
	    double az;
	    double r;
        public int CompareTo(object obj)
        {
            if (obj is Star)
            {
                Star otherTemperature = (Star)obj;
                return this.r.CompareTo(otherTemperature.r);
            }
            else
            {
                throw new ArgumentException("object is not a Star");
            }
        }

        public bool compare(Star Item)
        {
            return true;
        }

	    public void Add(Star Item)
        {
            x=(x*mass+(Item.x)*(Item.mass))/(mass+Item.mass);
	        y=(y*mass+(Item.y)*(Item.mass))/(mass+Item.mass);
	        z=(z*mass+(Item.z)*(Item.mass))/(mass+Item.mass);
	        vx=(vx*mass+(Item.vx)*(Item.mass))/(mass+Item.mass);
	        vy=(vy*mass+(Item.vy)*(Item.mass))/(mass+Item.mass);
	        vz=(vz*mass+(Item.vz)*(Item.mass))/(mass+Item.mass);
	        mass+=Item.mass;
	        Item.mass=0;
	        Item.x=100;
	        Item.y=100;
	        Item.z=100;
	        Item.vx=0;
	        Item.vy=0;
	        Item.vz=0;
	        r=Math.Pow(mass,1/3.0);
        }
    
	    public void Clock()
        {
            x+=vx/FREQ;
	        y+=vy/FREQ;
	        z+=vz/FREQ;
	        //////////
	        vx+=ax/FREQ;
	        vy+=ay/FREQ;
	        vz+=az/FREQ;	
	        //////////
	        ax=ay=az=0;
        }
	    public double X
        {
            get
            {
                return x;
            }
        }
	    public double Y
        {
            get
            {
                return y;
            }
        }
        public double Z
        {
            get
            {
                return z;
            }
        }
        public double Mass
        {
            get
            {
                return mass;
            }
        }
        public double R
        {
            get
            {
                return r;
            }
        }
	    public void impact(Star Item)
        {
            
            if(Item.available())
	        {
		        if(Item!=this)
		        {
			        double GM=GERAVITY*Item.mass;
			        double DeltaX=Item.x - x;
			        double DeltaY=Item.y - y;
			        double DeltaZ=Item.z - z;
			        double R3=Math.Pow(DeltaX*DeltaX+DeltaY*DeltaY+DeltaZ*DeltaZ,1.5);
			        if(Math.Pow(R3,1.0/3)<MIN_R)
			        {
				        if(mass>=Item.mass)
					       this.Add(Item);
			        }
			        else
			        {
				        ax+=(GM*DeltaX)/R3;
				        ay+=(GM*DeltaY)/R3;
				        az+=(GM*DeltaZ)/R3;
			        }
		        }
	        }
        }
	    public bool available()
        {
            if(mass==0)
		        return false;
	        return true;
        }
	    public void setPosition(double NewX,double NewY,double NewZ,double Vx,double Vy,double Vz,double NewMass)
        {
            mass=NewMass;
	        r=Math.Pow(Mass,1/3.0);
	        x=NewX;
	        y=NewY;
	        z=NewZ;
	        double Ro=Math.Pow(x*x+y*y+z*z,0.5);
            vx=Vx;
            vy=Vy;
            vz=Vz;

            ax=0;
            ay=0;
            az=0;
        }
	    public Star(double NewX,double NewY,double NewZ,double NewMass)
        {
            setPosition(NewX,NewY,NewZ,0,0,0,NewMass);
        }
	    public Star()
        {
            mass=0;
            x=0;
            y=0;
            z=0;
            vx=0;
            vy=0;
            vz=0;
            ax=0;
            ay=0;
            az=0;
        }

    }
}
