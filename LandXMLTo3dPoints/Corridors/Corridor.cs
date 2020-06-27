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
            VertAlignment = new VertAlignment(aligment);
        }

        public void XYZCoord(double sta, ref double X, ref double Y, ref double Z)
        {
            HorAlignment.XYCoordInStation(sta, ref X, ref Y);
            VertAlignment.ZCoordInStation(sta, ref Z);
        }         
    }
}
