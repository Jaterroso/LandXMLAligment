using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
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

                double staStart = Convert.ToDouble(spiral.StaStart, _format);
                double length = Convert.ToDouble(spiral.Length, _format);

                double dirStart = Convert.ToDouble(spiral.DirStart, _format);
                double dirEnd = Convert.ToDouble(spiral.DirEnd, _format);

                double radiusStart = 0, radiusEnd = 0;
                if (spiral.RadiusStart != "INF") radiusStart = Convert.ToDouble(spiral.RadiusStart, _format);
                if (spiral.RadiusEnd != "INF") radiusEnd = Convert.ToDouble(spiral.RadiusEnd, _format);
                
                double constant = Convert.ToDouble(spiral.Constant, _format);
                double xIni = Convert.ToDouble(spiral.Start.Point.Split(' ')[1], _format);
                double yIni = Convert.ToDouble(spiral.Start.Point.Split(' ')[0], _format);
                double xFin = Convert.ToDouble(spiral.End.Point.Split(' ')[1], _format);
                double yFin = Convert.ToDouble(spiral.End.Point.Split(' ')[0], _format);
                double xPI = Convert.ToDouble(spiral.PI.Split(' ')[1], _format);
                double ypI = Convert.ToDouble(spiral.PI.Split(' ')[0], _format);

                string desc = spiral.Desc;
                string rot = spiral.Rot;

                sectionsList.Add(new HorizontalSpiral(staStart, length, dirStart, dirEnd, radiusStart, radiusEnd, constant, desc, rot, xIni, yIni, xFin, yFin, xPI, ypI));               
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
                double xCenter = Convert.ToDouble(curve.Center.Point.Split(' ')[1], _format);
                double yCenter = Convert.ToDouble(curve.Center.Point.Split(' ')[0], _format);

                string rot = curve.Rot;

                sectionsList.Add(new HorizontalCurve(staStart, length, dirStart, dirEnd, radius, rot, xCenter, yCenter));
            }
        }

        private void ParseLineSections(Alignment aligment)
        {
            for (int i = 0; i < aligment.CoordGeom.Line.Count; i++)
            {
                Line line = aligment.CoordGeom.Line[i];

                double staStart = Convert.ToDouble(line.StaStart, _format);
                double length = Convert.ToDouble(line.Length, _format);

                double xIni = Convert.ToDouble(line.Start.Point.Split(' ')[1], _format);
                double yIni = Convert.ToDouble(line.Start.Point.Split(' ')[0], _format);
                double xFin = Convert.ToDouble(line.End.Point.Split(' ')[1], _format);
                double yFin = Convert.ToDouble(line.End.Point.Split(' ')[0], _format);

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
        public double DirStart { get; set; }
        public double DirEnd { get; set; }
        public double Radius { get; set; }
        public string Rot { get; set; }

        public double XCenter { get; set; }
        public double YCenter { get; set; }

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

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            double k = (sta - StaStart) / Length;
           
            double difAng = Math.Abs(DirEnd - DirStart);
            if (difAng > 200) difAng = 400 - difAng;           

            double dirRadStart = 200 - DirStart;
            int lambda = -1;
            if (Rot == "ccw") 
            {
                if (difAng <= 200) lambda *= -1;
                dirRadStart += 200;
            }             

            double angSta = dirRadStart + lambda * difAng * k;           

            X = XCenter + Radius * Math.Cos(angSta * Math.PI / 200);
            Y = YCenter + Radius * Math.Sin(angSta * Math.PI / 200);
        }
    }

    class HorizontalSpiral : HorizontalEntity
    {
        public double DirStart { get; set; }
        public double DirEnd { get; set; }
        public double RadiusStart { get; set; }
        public double RadiusEnd { get; set; }
        public double Constant { get; set; }
        public string Desc { get; set; }
        public string Rot { get; set; }

        public double XPI { get; set; }
        public double YPI { get; set; }

        public double XInf { get; set; }
        public double YInf { get; set; }
        

        public HorizontalSpiral(double staStart, double length, double dirStart, double dirEnd, double radiusStart, double radiusEnd, double constant, string desc, string rot, double xIni, double yIni, double xFin, double yFin, double xPI, double ypI)
        {
            StaStart = staStart;
            Length = length;           

            DirStart = dirStart;
            DirEnd = dirEnd;
            RadiusStart = radiusStart;
            RadiusEnd = radiusEnd;
            Constant = constant;
            Desc = desc;
            Rot = rot;
            XIni = xIni;
            YIni = yIni;
            XFin = xFin;
            YFin = yFin;
            XPI = xPI;
            YPI = ypI;
        }

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {           
            double x, y, A = Constant, lIni = 0, omega, xPos, yPos;                       
            int lambda = 1;           

            if (Desc == "TangentToCurve" || (Desc == "CurveToCurve" && RadiusEnd < RadiusStart))
            {
                if (Desc == "CurveToCurve") lIni = A * A / RadiusStart;

                x = XIni;
                y = YIni;
                omega = 100 - DirStart;               
                if (Rot == "cw") lambda = -1;                
            }
            else 
            {
                if (Desc == "CurveToCurve") lIni = A * A / RadiusEnd;                

                x = XFin;
                y = YFin;
                omega = 300 - DirEnd;               
                if (Rot == "ccw") lambda = -1;                
            }

            omega -= lambda * (lIni * lIni) / (2 * A * A);

            double lSta = sta - StaStart;

            double dS = 0.01;
            int nPunts = (int)(lSta / dS) + 1;
            dS = lSta / nPunts;

            for (int i = 0; i < nPunts; i++)
            {
                double s = dS * i;

                xPos = x + dS * Math.Cos((lambda * ((s * s) / (2 * A * A)) + omega) * Math.PI / 200);
                yPos = y + dS * Math.Sin((lambda * ((s * s) / (2 * A * A)) + omega) * Math.PI / 200);

                x = xPos;
                y = yPos;
            }

            X = x;
            Y = y;
        }  
    }
}
