using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    class MYPoint
    {
        private double x;
        private double y;
        private double z;

        public MYPoint()
        {
            x = y = z = 0;
        }
        public MYPoint(double tx, double ty, double tz)
        {
            x = tx; y = ty; z = tz;
        }
        ~MYPoint() { }

        public double getX()
        {
            return x;
        }
        public double getY()
        {
            return y;
        }
        public double getZ()
        {
            return z;
        }

        //public static bool operator == (MYPoint p1, MYPoint p2){
        //    if (p1.x == p2.x && p1.y == p2.y && p1.z == p2.z){
        //        return true;
        //    }
        //    return false;
        //}

        //public static bool operator != (MYPoint p1, MYPoint p2){
        //    return !(p1 == p2);
        //}

        //public operator MYPoint(MYPoint tp)
        //{
        //    x = tp.x; y = tp.y; z = tp.z;
        //    return this;
        //}
        //public static  operator = (MYPoint p){

        //}
    }

    //------------------------------------------------------------------------------

    class checkPoint
    {
        protected MYPoint p;
        protected bool press;
        protected List<MYPoint> ps;
        protected bool oneNote;

        public checkPoint()
        {
            press = false;
            ps = new List<MYPoint>();
            oneNote = true;
        }

        public checkPoint(MYPoint tp)
        {
            p = tp; press = false;
            ps = new List<MYPoint>();
            oneNote = true;
        }

        ~checkPoint() { }

        public virtual int isContain()
        {
            return 0;
        }

        public virtual void isClick() { }

        public virtual bool getPress()
        {
            return press;
        }

        public virtual List<MYPoint> getps()
        {
            return ps;
        }

        public virtual int checkNote(MYPoint pt)
        {
            p = pt;
            ps.Add(pt);
            isClick();
            int num = isContain();
            if (getPress() && oneNote && num != 0)
            {
                oneNote = false;
                return num;
            }

            checkPoint tcp = new checkPoint();
            if (ps.Count() > 1)
            {
                tcp = new checkPoint(ps[ps.Count() - 2]);
                tcp.isClick();
                if (!this.getPress() && tcp.getPress())
                {
                    oneNote = true;
                    ps.Clear();
                }
            }
            return 0;
        }
    }


    class checkPiano : checkPoint
    {
        public checkPiano()
        {
            press = false;
            ps = new List<MYPoint>();
            oneNote = true;
        }

        public checkPiano(MYPoint tp)
        {
            p = tp; press = false;
            ps = new List<MYPoint>();
            oneNote = true;
        }

        ~checkPiano() { }

        public override int isContain()
        {
            if (p.getX() >= 0.11 || p.getX() <= -0.10 || p.getY() <= -0.12 || p.getY() >= 0.12)
                return 0;

            double tempX = p.getX() * 100 + 10;
            double tempY = p.getY() * 100 + 12;
            int pianoNum = 0;

            if (tempX < 3)
            {
                if (tempY < 8)
                    pianoNum = 7;
                else if (tempY > 16)
                    pianoNum = 21;
                else
                    pianoNum = 14;
            }
            else if (tempX >= 3 && tempX < 6)
            {
                if (tempY < 8)
                    pianoNum = 6;
                else if (tempY > 16)
                    pianoNum = 20;
                else
                    pianoNum = 13;
            }
            else if (tempX >= 6 && tempX < 9)
            {
                if (tempY < 8)
                    pianoNum = 5;
                else if (tempY > 16)
                    pianoNum = 19;
                else
                    pianoNum = 12;
            }
            else if (tempX >= 9 && tempX < 12)
            {
                if (tempY < 8)
                    pianoNum = 4;
                else if (tempY > 16)
                    pianoNum = 18;
                else
                    pianoNum = 11;
            }
            else if (tempX >= 12 && tempX < 15)
            {
                if (tempY < 8)
                    pianoNum = 3;
                else if (tempY > 16)
                    pianoNum = 17;
                else
                    pianoNum = 10;
            }
            else if (tempX >= 15 && tempX < 18)
            {
                if (tempY < 8)
                    pianoNum = 2;
                else if (tempY > 16)
                    pianoNum = 16;
                else
                    pianoNum = 9;
            }
            else
            {
                if (tempY < 8)
                    pianoNum = 1;
                else if (tempY > 16)
                    pianoNum = 15;
                else
                    pianoNum = 8;
            }

            return pianoNum;
        }

        public override void isClick()
        {
            if (p.getZ() < 0.22)
                press = true;
            else
                press = false;

        }

        public override bool getPress()
        {
            return press;
        }

        public override List<MYPoint> getps()
        {
            return ps;
        }

        public override int checkNote(MYPoint pt)//bool oneNote)
        {
            p = pt;
            ps.Add(pt);
            isClick();
            int num = isContain();
            if (getPress() && oneNote && num != 0)
            {
                oneNote = false;
                return num;
            }

            checkPiano tcp = new checkPiano();
            if (ps.Count() > 1)
            {
                tcp = new checkPiano(ps[ps.Count() - 2]);
                tcp.isClick();
                if (!this.getPress() && tcp.getPress())
                {
                    oneNote = true;
                    ps.Clear();
                }
            }
            return 0;
        }
    }

    //------------------------------------------------------------------------------

    class checkDrum : checkPoint
    {
        const int rightCymbalx = 5;
        const int leftCymbalx = 35;
        const int Cymbalz = 19;
        const int midTomdrumx = 15;
        const int leftTomdrumx = 25;
        const int Tomdrumz = 29;
        const int leftSnaredrumx = 34;
        const int Snaredrumz = 40;
        const int rightTomdrumx = 6;
        const int areasmall = 25;
        const int areabig = 36;

        public checkDrum() { }

        public checkDrum(MYPoint tp)
        {
            p = tp; press = false;
        }

        ~checkDrum() { }

        public override int isContain()
        {
            if (p.getX() >= 0.20 || p.getX() <= -0.20 || p.getZ() <= 0.14 || p.getZ() >= 0.46)
                return 0;

            double tempX = p.getX() * 100 + 20;
            double tempZ = p.getZ() * 100;
            int drumNum = 0;

            if ((Math.Pow(tempX - rightCymbalx, 2) + Math.Pow(tempZ - Cymbalz, 2)) < areasmall)
                drumNum = 100;
            else if ((Math.Pow(tempX - leftCymbalx, 2) + Math.Pow(tempZ - Cymbalz, 2)) < areasmall)
                drumNum = 101;
            else if ((Math.Pow(tempX - midTomdrumx, 2) + Math.Pow(tempZ - Tomdrumz, 2)) < areasmall)
                drumNum = 102;
            else if ((Math.Pow(tempX - leftTomdrumx, 2) + Math.Pow(tempZ - Tomdrumz, 2)) < areasmall)
                drumNum = 103;
            else if ((Math.Pow(tempX - rightTomdrumx, 2) + Math.Pow(tempZ - Snaredrumz, 2)) < areabig)
                drumNum = 104;
            else if ((Math.Pow(tempX - leftSnaredrumx, 2) + Math.Pow(tempZ - Snaredrumz, 2)) < areabig)
                drumNum = 105;

            return drumNum;
        }

        public override void isClick()
        {
            if (p.getY() < 0)
                press = true;
            else
                press = false;
        }

        public override bool getPress()
        {
            return press;
        }

        public override List<MYPoint> getps()
        {
            return ps;
        }

        public override int checkNote(MYPoint pt)
        {
            p = pt;
            ps.Add(pt);
            isClick();
            int num = isContain();
            if (getPress() && oneNote && num != 0)
            {
                oneNote = false;
                return num;
            }

            checkDrum tcp = new checkDrum();
            if (ps.Count() > 1)
            {
                tcp = new checkDrum(ps[ps.Count() - 2]);
                tcp.isClick();
                if (!this.getPress() && tcp.getPress())
                {
                    oneNote = true;
                    ps.Clear();
                }
            }
            return 0;
        }
       
    }
}