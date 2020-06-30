using System;
using System.Collections.Generic;
using System.Globalization;
using Xml2CSharp;

namespace Generator.LandXML
{
    public class HorAlignment
    {        
        private List<HorizontalEntity> sectionsList;

        public HorAlignment(Alignment aligment)
        {
            NumberFormat();
            sectionsList = new List<HorizontalEntity>();

            // Parse the alignment section entities to the List<HorizontalEntity> sectionsList
            ParseLineSections(aligment);
            ParseCurveSections(aligment);
            ParseSpiralSections(aligment);
        }

        private void ParseSpiralSections(Alignment aligment)
        {
            for (int i = 0; i < aligment.CoordGeom.Spiral.Count; i++)
            {
                Spiral spiral = aligment.CoordGeom.Spiral[i];

                double staStart = Convert.ToDouble(spiral.StaStart);
                double length = Convert.ToDouble(spiral.Length, _format);

                double xIni = Convert.ToDouble(spiral.Start.Point.Split(' ')[0], _format);
                double yIni = Convert.ToDouble(spiral.Start.Point.Split(' ')[1], _format);
                double xFin = Convert.ToDouble(spiral.End.Point.Split(' ')[0], _format);
                double yFin = Convert.ToDouble(spiral.End.Point.Split(' ')[1], _format);

                sectionsList.Add(new HorizontalSpiral());
            }
        }

        private void ParseCurveSections(Alignment aligment)
        {
            for (int i = 0; i < aligment.CoordGeom.Curve.Count; i++)
            {
                Curve curve = aligment.CoordGeom.Curve[i];

                double staStart = Convert.ToDouble(curve.StaStart, _format);
                double length = Convert.ToDouble(curve.Length, _format);

                double dirStart = Convert.ToDouble(curve.DirStart, _format);
                double dirEnd = Convert.ToDouble(curve.DirEnd, _format);
                double radius = Convert.ToDouble(curve.Radius, _format);
                double xCenter = Convert.ToDouble(curve.Center.Point.Split(' ')[0], _format);
                double yCenter = Convert.ToDouble(curve.Center.Point.Split(' ')[1], _format);

                string rot = curve.Rot;

                sectionsList.Add(new HorizontalCurve(staStart, length, dirStart, dirEnd, radius, rot, xCenter, yCenter));
            }
        }

        private void ParseLineSections(Alignment aligment)
        {
            for (int i = 0; i < aligment.CoordGeom.Line.Count; i++)
            {
                Line line = aligment.CoordGeom.Line[i];

                double staStart = Convert.ToDouble(line.StaStart);
                double length = Convert.ToDouble(line.Length, _format);

                double xIni = Convert.ToDouble(line.Start.Point.Split(' ')[0], _format);
                double yIni = Convert.ToDouble(line.Start.Point.Split(' ')[1], _format);
                double xFin = Convert.ToDouble(line.End.Point.Split(' ')[0], _format);
                double yFin = Convert.ToDouble(line.End.Point.Split(' ')[1], _format);

                double dir = Convert.ToDouble(line.Dir, _format);

                sectionsList.Add(new HorizontalLine(staStart, length, xIni, yIni, xFin, yFin, dir));
            }
        }

        private NumberFormatInfo _format = new NumberFormatInfo();
        private void NumberFormat()
        {
            _format.NumberDecimalSeparator = ".";
        }
       

        public bool XYCoordInStation(double sta, ref double X, ref double Y)
        {
            for (int i = 0; i < sectionsList.Count; i++)
            {
                HorizontalEntity section = sectionsList[i];

                double staIni = section.StaStart;
                double staFin = staIni + section.Length;

                if (staIni <= sta && sta <= staFin)
                {
                    section.XYCoordInStation(sta, ref X, ref Y);
                    return true;
                }
            }

            return false;
        }    
    }

    abstract class HorizontalEntity
    {       
        public double StaStart { get; set; }
        public double Length { get; set; }

        public double XIni { get; set; }
        public double YIni { get; set; }
        public double XFin { get; set; }
        public double YFin { get; set; }

        public abstract void XYCoordInStation(double sta, ref double X, ref double Y);
    }

    class HorizontalLine : HorizontalEntity
    {
        public double Dir { get; set; }

        public HorizontalLine(double staStart, double length, double xIni, double yIni, double xFin, double yFin, double dir)
        {
            StaStart = staStart;
            Length = length;
            XIni = xIni;
            YIni = yIni;
            XFin = xFin;
            YFin = yFin;
            Dir = dir;
        }              

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            double k = (sta - StaStart) / Length;           

            X = XIni + k * (XFin - XIni);
            Y = YIni + k * (YFin - YIni);
        }
    }

    class HorizontalCurve : HorizontalEntity
    {
        public HorizontalCurve(double staStart, double length, double dirStart, double dirEnd, double radius, string rot, double xCenter, double yCenter)
        {
            StaStart = staStart;
            Length = length;           

            DirStart = dirStart;
            DirEnd = dirEnd;
            Radius = radius;
            Rot = rot;
            XCenter = xCenter;
            YCenter = yCenter;
        }

        public double DirStart { get; set; }
        public double DirEnd { get; set; }
        public double Radius { get; set; }        
        public string Rot { get; set; }

        public double XCenter { get; set; }
        public double YCenter { get; set; }

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            double k = (sta - StaStart) / Length; 

            double angStart = DirStart * Math.PI / 200;
            double angEnd = DirEnd * Math.PI / 200; 
           
            double angSta = angStart;

            if (Rot == "cw") angSta += (angEnd - angStart) * k;
            else angSta -= (angEnd - angStart) * k;

            X = XCenter + Radius * Math.Sin(angSta);
            Y = YCenter - Radius * Math.Cos(angSta);
        }
    }

    class HorizontalSpiral : HorizontalEntity
    {
        public double DirStart { get; set; }
        public double DirEnd { get; set; }
        public double RadiusStart { get; set; }
        public double RadiusEnd { get; set; }
        public double Constant { get; set; }
        public double Rot { get; set; }

        public double XPI { get; set; }
        public double PI { get; set; }

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            double k = (sta - StaStart) / Length;

            ////////////////////////////////////
            X = -1;
            Y = -1;
            ////////////////////////////////////
        }
    }
}
