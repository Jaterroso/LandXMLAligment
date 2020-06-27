using System;
using System.Globalization;
using Xml2CSharp;

namespace Generator.LandXML
{
    public class HorAlignment
    {
        private CoordGeom coordGeom; 

        public HorAlignment(Alignment aligment)
        {
            coordGeom = aligment.CoordGeom;
            NumberFormat();
        }

        private NumberFormatInfo _format = new NumberFormatInfo();
        private void NumberFormat()
        {
            _format.NumberDecimalSeparator = ".";
        }

        public enum CoordGeomType
        {
            line,
            curve,
            spiral,
            undefined
        }

        public CoordGeomType XYCoordInStation(double sta, ref double X, ref double Y)
        {
            CoordGeomType type;
            int iSec = 0;

            if (SectionLine(sta, ref iSec)) type = CoordGeomType.line;
            else if (SectioCurve(sta, ref iSec)) type = CoordGeomType.curve;
            else if (SectionSpiral(sta, ref iSec)) type = CoordGeomType.spiral;
            else return CoordGeomType.undefined;

            switch (type)
            {
                case CoordGeomType.line:
                    XYCoordinatesLineInStation(sta, coordGeom.Line[iSec], ref X, ref Y);
                    break;
                case CoordGeomType.curve:
                    XYCoordinatesCurveInStation(sta, coordGeom.Curve[iSec], ref X, ref Y);
                    break;
                case CoordGeomType.spiral:
                    XYCoordinatesSpiralInStation(sta, coordGeom.Spiral[iSec], ref X, ref Y);
                    break;
                default:
                    break;
            }

            return type;
        }

        private bool SectionSpiral(double sta, ref int iSec)
        {
            for (int i = 0; i < coordGeom.Spiral.Count; i++)
            {
                Spiral spiral = coordGeom.Spiral[i];

                double staStart = Convert.ToDouble(spiral.StaStart);
                double staEnd = staStart + Convert.ToDouble(spiral.Length);

                if (sta >= staStart && sta <= staEnd) { iSec = i; return true; }
            }
            return false;
        }

        private bool SectioCurve(double sta, ref int iSec)
        {
            for (int i = 0; i < coordGeom.Curve.Count; i++)
            {
                Curve curve = coordGeom.Curve[i];

                double staStart = Convert.ToDouble(curve.StaStart);
                double staEnd = staStart + Convert.ToDouble(curve.Length);

                if (sta >= staStart && sta <= staEnd) { iSec = i; return true; }
            }
            return false;
        }

        private bool SectionLine(double sta, ref int iSec)
        {

            for (int i = 0; i < coordGeom.Line.Count; i++)
            {
                Line line = coordGeom.Line[i];

                double staStart = Convert.ToDouble(line.StaStart);
                double staEnd = staStart + Convert.ToDouble(line.Length, _format);

                if (sta >= staStart && sta <= staEnd) { iSec = i; return true; }
            }
            return false;
        }

        void XYCoordinatesLineInStation(double sta, Line line, ref double X, ref double Y)
        {
            double k = (sta - Convert.ToDouble(line.StaStart, _format)) / Convert.ToDouble(line.Length, _format);

            double xIni = Convert.ToDouble(line.Start.Point.Split(' ')[0], _format);
            double yIni = Convert.ToDouble(line.Start.Point.Split(' ')[1], _format);
            double xFin = Convert.ToDouble(line.End.Point.Split(' ')[0], _format);
            double yFin = Convert.ToDouble(line.End.Point.Split(' ')[1], _format);

            X = xIni + k * (xFin - xIni);
            Y = yIni + k * (yFin - yIni);
        }



        void XYCoordinatesCurveInStation(double sta, Curve curve, ref double X, ref double Y)
        {
            double length = Convert.ToDouble(curve.Length, _format);
            double distSta = sta - Convert.ToDouble(curve.StaStart, _format);

            double k = distSta / length;

            double dirStart = Convert.ToDouble(curve.DirStart, _format);
            double dirEnd = Convert.ToDouble(curve.DirEnd, _format);

            double xIni = Convert.ToDouble(curve.Start.Point.Split(' ')[0], _format);
            double yIni = Convert.ToDouble(curve.Start.Point.Split(' ')[1], _format);

            double xFin = Convert.ToDouble(curve.End.Point.Split(' ')[0], _format);
            double yFin = Convert.ToDouble(curve.End.Point.Split(' ')[1], _format);

            double radius = Convert.ToDouble(curve.Radius, _format);

            double xCen = Convert.ToDouble(curve.Center.Point.Split(' ')[0], _format);
            double yCen = Convert.ToDouble(curve.Center.Point.Split(' ')[1], _format);
        }




        void XYCoordinatesSpiralInStation(double sta, Spiral spiral, ref double X, ref double Y)
        {
            double k = (sta - Convert.ToDouble(spiral.StaStart, _format)) / Convert.ToDouble(spiral.Length, _format);

        }
    }
}
