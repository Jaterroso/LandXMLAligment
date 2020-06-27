using System;
using System.Collections.Generic;
using System.Globalization;
using Xml2CSharp;

namespace Generator.LandXML
{
    public class VertAlignment
    {
        private Profile profile;

        public VertAlignment(Alignment aligment)
        {
            profile = aligment.Profile;
            NumberFormat();
        }

        private NumberFormatInfo _format = new NumberFormatInfo();
        private void NumberFormat()
        {
            _format.NumberDecimalSeparator = ".";
        }

        public void ZCoordInStation(double sta, ref double Z)
        {
            for (int i = 0; i < profile.ProfAlign.Count; i++)
            {
                ProfAlign profAlign = profile.ProfAlign[i];

                double PVISta1 = Convert.ToDouble(profAlign.PVI[0].Split(' ')[0], _format);
                double PVISta2 = Convert.ToDouble(profAlign.PVI[1].Split(' ')[0], _format);


                if (PVISta1 <= sta && sta <= PVISta2)
                {
                    List<double> points = new List<double>();

                    double PVIZ1 = Convert.ToDouble(profAlign.PVI[0].Split(' ')[1], _format);
                    double PVIZ2 = Convert.ToDouble(profAlign.PVI[1].Split(' ')[1], _format);

                    points.Add(PVISta1);
                    points.Add(PVIZ1);
                    for (int j = 0; j < profAlign.ParaCurve.Count; j++)
                    {
                        ParaCurve paraCurve = profAlign.ParaCurve[j];

                        double staPoint = Convert.ToDouble(paraCurve.Point.Split(' ')[0], _format);
                        double ZPoint = Convert.ToDouble(paraCurve.Point.Split(' ')[1], _format);
                        points.Add(staPoint);
                        points.Add(ZPoint);
                    }
                    points.Add(PVISta2);
                    points.Add(PVIZ2);   

                    for (int j = 0; j < points.Count / 2 - 1; j++)
                    {
                        int pIni = j;
                        int pFin = j + 1;

                        double staIni = points[2 * pIni];
                        double zIni = points[2 * pIni + 1];

                        double staFin = points[2 * pFin];
                        double zFin = points[2 * pFin + 1];

                        if (staIni <= sta && sta <= staFin)
                        {
                            double k = (sta - staIni) / (staFin - staIni);
                            Z = zIni + k * (zFin - zIni);                           
                        }
                    }


                    for (int j = 0; j < profAlign.ParaCurve.Count; j++)
                    {
                        ParaCurve paraCurve = profAlign.ParaCurve[j];

                        double staPoint = Convert.ToDouble(paraCurve.Point.Split(' ')[0], _format);

                        double L = Convert.ToDouble(paraCurve.Length, _format);
                        double T = L / 2;

                        double staIni = staPoint - T;
                        double staFin = staPoint + T;

                        if (staIni <= sta && sta <= staFin)
                        {
                            int ip1 = j;
                            int ip2 = j + 1;
                            int ip3 = j + 2;

                            double i1 = (points[2 * ip2 + 1] - points[2 * ip1 + 1]) / (points[2 * ip2] - points[2 * ip1]);
                            double i2 = (points[2 * ip3 + 1] - points[2 * ip2 + 1]) / (points[2 * ip3] - points[2 * ip2]);

                            double teta = Math.Abs(i2 - i1);

                            double Kv = L / teta;
                            double x = sta - staIni;
                            double y = (x * x) / (2 * Kv);

                            if (i1 >= 0) y *= -1;

                            Z += y;
                            return;
                        }
                    }
                }
            }
        }
    }
}
