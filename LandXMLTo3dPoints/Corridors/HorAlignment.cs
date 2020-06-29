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

                double staStart = Convert.ToDouble(curve.StaStart);
                double length = Convert.ToDouble(curve.Length, _format);

                double xIni = Convert.ToDouble(curve.Start.Point.Split(' ')[0], _format);
                double yIni = Convert.ToDouble(curve.Start.Point.Split(' ')[1], _format);
                double xFin = Convert.ToDouble(curve.End.Point.Split(' ')[0], _format);
                double yFin = Convert.ToDouble(curve.End.Point.Split(' ')[1], _format);

                sectionsList.Add(new HorizontalCurve());
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
       

        public void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            for (int i = 0; i < sectionsList.Count; i++)
            {
                HorizontalEntity section = sectionsList[i];

                double staIni = section.staStart;
                double staFin = staIni + section.length;

                if (staIni <= sta && sta <= staFin)
                {
                    section.XYCoordInStation(sta, ref X, ref Y);
                    return;
                }
            }
        }    
    }

    abstract class HorizontalEntity
    {       
        public double staStart { get; set; }
        public double length { get; set; }

        public double xIni { get; set; }
        public double yIni { get; set; }
        public double xFin { get; set; }
        public double yFin { get; set; }

        public abstract void XYCoordInStation(double sta, ref double X, ref double Y);
    }

    class HorizontalLine : HorizontalEntity
    {
        public double dir { get; set; }

        public HorizontalLine(double staStart, double length, double xIni, double yIni, double xFin, double yFin, double dir)
        {
            this.staStart = staStart;
            this.length = length;
            this.xIni = xIni;
            this.yIni = yIni;
            this.xFin = xFin;
            this.yFin = yFin;
            this.dir = dir;
        }              

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            double k = (sta - staStart) / length;           

            X = xIni + k * (xFin - xIni);
            Y = yIni + k * (yFin - yIni);
        }
    }

    class HorizontalCurve : HorizontalEntity
    {
        public double dirStart { get; set; }
        public double dirEnd { get; set; }
        public double radius { get; set; }        
        public string rot { get; set; }

        public double xCenter { get; set; }
        public double yCenter { get; set; }

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            double k = (sta - staStart) / length;

            ////////////////////////////////////
            X = -1;
            Y = -1;
            ////////////////////////////////////
        }
    }

    class HorizontalSpiral : HorizontalEntity
    {
        public double dirStart { get; set; }
        public double dirEnd { get; set; }
        public double radiusStart { get; set; }
        public double radiusEnd { get; set; }
        public double constant { get; set; }
        public double rot { get; set; }

        public double xPI { get; set; }
        public double PI { get; set; }

        public override void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            double k = (sta - staStart) / length;

            ////////////////////////////////////
            X = -1;
            Y = -1;
            ////////////////////////////////////
        }
    }
}
