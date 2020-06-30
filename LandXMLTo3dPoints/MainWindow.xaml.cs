using Generator.LandXML;
using Lib.LandXML;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace LandXMLTo3dPoints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenLandXMLFile();

            Xml2CSharp.Alignment alignment = _data.Alignments.Alignment[0];

            Corridor corridor = new Corridor(alignment);

            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            double delta = 1;

            double length = Convert.ToDouble(alignment.Length, format);
            int nPoints = (int)(length / delta) + 1;

            delta = length / nPoints;
            
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            nfi.NumberDecimalSeparator = ".";
            nfi.NumberGroupSeparator = "";

            string pathFile = _filePath;
            string nameFile = "Alignment";

            using (StreamWriter Alignment = new StreamWriter(pathFile + "\\" + nameFile + ".dat"))
            {
                double X = 0, Y = 0, Z = 0;
                for (int i = 0; i < nPoints; i++)
                {
                    double sta = i * delta;

                    bool result = corridor.XYZCoord(sta, ref X, ref Y, ref Z);

                    if(result) Alignment.WriteLine(sta.ToString("N", nfi) + " " + X.ToString("N", nfi) + " " + Y.ToString("N", nfi) + " " + Z.ToString("N", nfi) + " " + "2.0" + " " + "2.0");
                }
            }
        }

        string _filePath;

        private void OpenLandXMLFile()
        {
            // Filter files by extension 
            var dlg = new OpenFileDialog
            {
                Filter = string.Join("|", "LandXML Files|*.xml")
            };   

            dlg.FileOk += dlg_OpenLandXMLFile;
            dlg.ShowDialog(this);
        }

        Xml2CSharp.LandXML _data = new Xml2CSharp.LandXML();

        private void dlg_OpenLandXMLFile(object sender, CancelEventArgs e)
        {
            var dlg = sender as OpenFileDialog;
            if (dlg != null)
            {
                string fullPath = dlg.FileName;
                _filePath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                FileInfo file = new FileInfo(dlg.FileName);

                if (!file.Exists) // file does not exist; do nothing
                    return;

                try
                {
                    Loader load = new Loader();
                    _data = load.Load(file.FullName);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{file}-{exception.InnerException.Message}");
                }
            }
        }
    }
}
