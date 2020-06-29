using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using Xml2CSharp;

namespace Generator.LandXML
{
    public class VertAlignment
    {
        private List<VerticalEntity> sectionsList;

        public VertAlignment(Alignment aligment)
        {
            NumberFormat();
            sectionsList = new List<VerticalEntity>();

            ProfAlign profAlign = aligment.Profile.ProfAlign[0];

            // Find all the polygonal points

            List<double> points = new List<double>();

            points.Add(Convert.ToDouble(profAlign.PVI[0].Split(' ')[0], _format));
            points.Add(Convert.ToDouble(profAlign.PVI[0].Split(' ')[1], _format));
            for (int i = 0; i < profAlign.ParaCurve.Count; i++)
            {
                ParaCurve paraCurve = profAlign.ParaCurve[i];

                points.Add(Convert.ToDouble(paraCurve.Point.Split(' ')[0], _format));
                points.Add(Convert.ToDouble(paraCurve.Point.Split(' ')[1], _format));
            }
            points.Add(Convert.ToDouble(profAlign.PVI[1].Split(' ')[0], _format));
            points.Add(Convert.ToDouble(profAlign.PVI[1].Split(' ')[1], _format));

            // Generates curves

            List<double> linePoints = new List<double>();
            int nVert = profAlign.ParaCurve.Count;

            for (int i = 1; i < nVert + 1; i++)
            {
                ParaCurve paraCurve = profAlign.ParaCurve[i - 1];

                double L = Convert.ToDouble(paraCurve.Length, _format);

                int i1 = i - 1;
                int i2 = i;
                int i3 = i + 1;

                double sta1 = points[2 * i1];
                double z1 = points[2 * i1 + 1];

                double sta2 = points[2 * i2];
                double z2 = points[2 * i2 + 1];

                double sta3 = points[2 * i3];
                double z3 = points[2 * i3 + 1];

                double staP1 = sta2 - L / 2;
                double staP2 = sta2 + L / 2;               

                double zP1 = z1 + (staP1 - sta1) / (sta2 - sta1) * (z2 - z1);
                double zP2 = z2 + (staP2 - sta2) / (sta3 - sta2) * (z3 - z2);
                
                sectionsList.Add(new VerticalCurve(staP1, zP1, staP2, zP2, sta2, z2, L));


                if (i == 1)
                {
                    linePoints.Add(sta1);
                    linePoints.Add(z1);
                }

                linePoints.Add(staP1);
                linePoints.Add(zP1);
                linePoints.Add(staP2);
                linePoints.Add(zP2);

                if (i == nVert)
                {
                    linePoints.Add(sta3);
                    linePoints.Add(z3);
                }
            }

            // Generate lines

            for (int i = 0; i < linePoints.Count / 4; i++)
            {
                double staIni = linePoints[4 * i];
                double zIni = linePoints[4 * i + 1];

                double staFin = linePoints[4 * i + 2];
                double zFin = linePoints[4 * i + 3];

                sectionsList.Add(new VerticalLine(staIni, zIni, staFin, zFin));
            }
        }
       

        public void ZCoordInStation(double sta, ref double Z)
        {
            for (int i = 0; i < sectionsList.Count; i++)
            {
                VerticalEntity section = sectionsList[i];

                double staIni = section.staIni;
                double staFin = section.staFin;

                if (staIni <= sta && sta <= staFin)
                {
                    section.ZCoordInStation(sta, ref Z);
                    return;
                }
            }
        }

        private NumberFormatInfo _format = new NumberFormatInfo();
        private void NumberFormat()
        {
            _format.NumberDecimalSeparator = ".";
        }
    }

    abstract class VerticalEntity
    {
        public double staIni { get; set; }
        public double staFin { get; set; }

        public double zIni { get; set; }
        public double zFin { get; set; }

        public abstract void ZCoordInStation(double sta, ref double Z);
    }

    class VerticalLine : VerticalEntity
    {
        public VerticalLine(double staIni, double zIni, double staFin, double zFin)
        {
            this.staIni = staIni;
            this.zIni = zIni;
            this.staFin = staFin;
            this.zFin = zFin;
        }

        public override void ZCoordInStation(double sta, ref double Z)
        {
            double k = (sta - staIni) / (staFin - staIni);

            Z = zIni + k * (zFin - zIni);
        }
    }

    class VerticalCurve : VerticalEntity
    {
        public double staVert { get; set; }
        public double zVert { get; set; }

        public double length { get; set; }

        public VerticalCurve(double staIni, double zIni, double staFin, double zFin, double staVert, double zVert, double length)
        {
            this.staIni = staIni;
            this.zIni = zIni;
            this.staFin = staFin;
            this.zFin = zFin;

            this.staVert = staVert;
            this.zVert = zVert;
            this.length = length;
        }      


        public override void ZCoordInStation(double sta, ref double Z)
        {
            double k;
            if (sta <= staVert)
            {
                k = (sta - staIni) / (staVert - staIni);
                Z = zIni + k * (zVert - zIni);
            }
            else
            {
                k = (sta - staVert) / (staFin - staVert);
                Z = zVert + k * (zFin - zVert);
            }

            double i1 = (zVert - zIni) / (staVert - staIni);
            double i2 = (zFin - zVert) / (staFin - staVert);

            double Kv = length / Math.Abs(i2 - i1);

            double l;
            if (sta < staVert) l = sta - staIni;
            else l = staFin - sta;

            double z = (l * l) / (2 * Kv);

            if (i1 > i2) z *= -1;

            Z += z;
        }
    }
}
