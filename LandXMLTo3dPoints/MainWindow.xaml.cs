using Generator.LandXML;
using Lib.LandXML;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using Xml2CSharp;

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

            Alignments alignments = _data.Alignments;
            if(alignments != null) GetAlignment(alignments.Alignment[0]);


            Surfaces surfaces = _data.Surfaces;
            if (surfaces != null) GetSurfaces(surfaces);
        }

        private void GetSurfaces(Surfaces surfaces)
        {            
            for (int i = 0; i < surfaces.Surface.Count; i++)
            {   
                Surface surface = surfaces.Surface[i];                

                string pathFile = _filePath;
                string nameFile = "Surface_" + (i + 1).ToString();

                string path = pathFile + "/" + nameFile + ".dat";

                if (IsFileCreated(new FileInfo(path)) && IsFileLocked(new FileInfo(path)))
                {
                    MessageBox.Show("Cerrar archivo Aligment.dat");
                    return;
                }

                using (StreamWriter SurfaceDat = new StreamWriter(path))
                {
                    Definition definition = surface.Definition;

                    int nPoints = definition.Pnts.P.Count;

                    int[] idPoints = new int[nPoints];
                    SurfaceDat.WriteLine(nPoints.ToString());
                    for (int ip = 0; ip < nPoints; ip++)
                    {     
                        SurfaceDat.WriteLine(definition.Pnts.P[ip].Coord);
                        idPoints[ip] = Convert.ToInt32(definition.Pnts.P[ip].Id);
                    }

                    int nFaces = definition.Faces.F.Count;
                    SurfaceDat.WriteLine(nFaces.ToString());
                    for (int ifa = 0; ifa < nFaces; ifa++)
                    {
                        int nVert = definition.Faces.F[ifa].Vertexs.Split(' ').Length;

                        string faceVerts = "";

                        for (int ivert = 0; ivert < nVert; ivert++)
                        {
                            string idVert = definition.Faces.F[ifa].Vertexs.Split(' ')[ivert];

                            for (int ip = 0; ip < idPoints.Length; ip++)
                            {
                                if(idPoints[ip] == Convert.ToInt32(idVert))
                                {
                                    faceVerts += (ip + 1).ToString();
                                    break;
                                }
                            }
                            
                            if(ivert != nVert - 1) faceVerts += " ";
                        }

                        SurfaceDat.WriteLine(faceVerts);
                    }
                }
            }
        }

        private void GetAlignment(Alignment alignment)
        {
            Corridor corridor = new Corridor(alignment);
            NumberFormatInfo nfi = NumberFormat();

            double delta = 0.5; 

            double length = Convert.ToDouble(alignment.Length, nfi);
            int nPoints = (int)(length / delta) + 1;

            delta = length / (nPoints - 1);

            string pathFile = _filePath;
            string nameFile = "Alignment";

            string path = pathFile + "/" + nameFile + ".dat";

            if (IsFileCreated(new FileInfo(path)) && IsFileLocked(new FileInfo(path)))
            {
                MessageBox.Show("Cerrar archivo Aligment.dat");
                return;
            }

            using (StreamWriter Alignment = new StreamWriter(path))
            {
                double X = 0, Y = 0, Z = 0;
                for (int i = 0; i < nPoints; i++)
                {
                    double sta = i * delta;
                    
                    bool result = corridor.XYZCoord(sta, ref X, ref Y, ref Z);

                    if (result) Alignment.WriteLine(sta.ToString("N", nfi) + " " + X.ToString("N", nfi) + " " + Y.ToString("N", nfi) + " " + Z.ToString("N", nfi));                    
                }               
            }
        }

        private static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        private static bool IsFileCreated(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return true;
        }

        private static NumberFormatInfo NumberFormat()
        {
            return new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
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

        LandXML _data = new LandXML();

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
