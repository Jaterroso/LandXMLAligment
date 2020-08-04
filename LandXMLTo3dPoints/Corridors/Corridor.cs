using System;
using Xml2CSharp;

namespace Generator.LandXML
{
    public class Corridor
    {
        public HorAlignment HorAlignment { get; }
        public VertAlignment VertAlignment { get; }

        public Corridor(Alignment aligment)
        {
            HorAlignment = new HorAlignment(aligment);
            //VertAlignment = new VertAlignment(aligment); //ÇÇÇ
        }

        public bool XYZCoord(double sta, ref double X, ref double Y, ref double Z)
        {
            bool resHor =  HorAlignment.XYCoordInStation(sta, ref X, ref Y);
            //bool resVer =  VertAlignment.ZCoordInStation(sta, ref Z);
            bool resVer = true; //ÇÇÇÇÇ

            if (resHor && resVer) return true;
            return false;
        }         
    }
}
