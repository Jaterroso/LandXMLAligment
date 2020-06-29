/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
    [XmlRoot(ElementName = "Alignment")]
    public class Alignment
    {
        [XmlElement(ElementName = "CoordGeom")]
        public CoordGeom CoordGeom { get; set; }
        [XmlElement(ElementName = "CrossSects")]
        public CrossSects CrossSects { get; set; }
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Profile")]
        public Profile Profile { get; set; }
        [XmlAttribute(AttributeName = "staStart")]
        public string StaStart { get; set; }
        [XmlElement(ElementName = "Superelevation")]
        public Superelevation Superelevation { get; set; }
    }

    [XmlRoot(ElementName = "Alignments")]
    public class Alignments
    {
        [XmlElement(ElementName = "Alignment")]
        public List<Alignment> Alignment { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Application")]
    public class Application
    {
        [XmlElement(ElementName = "Author")]
        public Author Author { get; set; }
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "manufacturer")]
        public string Manufacturer { get; set; }
        [XmlAttribute(AttributeName = "manufacturerURL")]
        public string ManufacturerURL { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "timeStamp")]
        public string TimeStamp { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

    [XmlRoot(ElementName = "Author")]
    public class Author
    {
        [XmlAttribute(AttributeName = "company")]
        public string Company { get; set; }
        [XmlAttribute(AttributeName = "companyURL")]
        public string CompanyURL { get; set; }
        [XmlAttribute(AttributeName = "createdBy")]
        public string CreatedBy { get; set; }
        [XmlAttribute(AttributeName = "createdByEmail")]
        public string CreatedByEmail { get; set; }
        [XmlAttribute(AttributeName = "timeStamp")]
        public string TimeStamp { get; set; }
    }

    [XmlRoot(ElementName = "Backsight")]
    public class Backsight
    {
        [XmlElement(ElementName = "BacksightPoint")]
        public BacksightPoint BacksightPoint { get; set; }
        [XmlAttribute(AttributeName = "circle")]
        public string Circle { get; set; }
        [XmlAttribute(AttributeName = "setupID")]
        public string SetupID { get; set; }
    }

    [XmlRoot(ElementName = "BacksightPoint")]
    public class BacksightPoint
    {
        [XmlAttribute(AttributeName = "pntRef")]
        public string PntRef { get; set; }
    }

    [XmlRoot(ElementName = "Center")]
    public class Center
    {
        [XmlText] public string Point { get; set; }
        [XmlAttribute(AttributeName = "pntRef")]
        public string PntRef { get; set; }
    }
    [XmlRoot(ElementName = "Boundaries")]
    public class Boundaries
    {
        [XmlElement(ElementName = "Boundary")]
        public List<Boundary> Boundary { get; set; }
    }

    [XmlRoot(ElementName = "Boundary")]
    public class Boundary
    {
        [XmlAttribute(AttributeName = "bndType")]
        public string BndType { get; set; }
        [XmlAttribute(AttributeName = "m")]
        public string M { get; set; }
        [XmlElement(ElementName = "PntList3D")]
        public string PntList3D { get; set; }
    }

    [XmlRoot(ElementName = "CgPoint")]
    public class CgPoint
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "m")]
        public string M { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "pntRef")]
        public string PntRef { get; set; }
        [XmlAttribute(AttributeName = "pntSurv")]
        public string PntSurv { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "CgPoints")]
    public class CgPoints
    {
        [XmlElement(ElementName = "CgPoint")]
        public List<CgPoint> CgPoint { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "CrossSect")]
    public class CrossSect
    {
        [XmlElement(ElementName = "CrossSectSurf")]
        public List<CrossSectSurf> CrossSectSurf { get; set; }
        [XmlElement(ElementName = "DesignCrossSectSurf")]
        public List<DesignCrossSectSurf> DesignCrossSectSurf { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "sta")]
        public string Sta { get; set; }
    }

    [XmlRoot(ElementName = "CrossSectPnt")]
    public class CrossSectPnt
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlText]
        public string Text { get; set; }
    }


    [XmlRoot(ElementName = "CoordGeom")]
    public class CoordGeom
    {
        [XmlElement(ElementName = "Spiral")]
        public List<Spiral> Spiral { get; set; }
        [XmlElement(ElementName = "Curve")]
        public List<Curve> Curve { get; set; }
        [XmlElement(ElementName = "Line")]
        public List<Line> Line { get; set; }
    }

    [XmlRoot(ElementName = "CoordinateSystem")]
    public class CoordinateSystem
    {
        [XmlAttribute(AttributeName = "ogcWktCode")]
        public string OgcWktCode { get; set; }
    }

    [XmlRoot(ElementName = "CrossSects")]
    public class CrossSects
    {
        [XmlElement(ElementName = "CrossSect")]
        public List<CrossSect> CrossSect { get; set; }
    }

    [XmlRoot(ElementName = "CrossSectSurf")]
    public class CrossSectSurf
    {
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "PntList2D")]
        public string PntList2D { get; set; }
    }

    [XmlRoot(ElementName = "Curve")]
    public class Curve
    {
        [XmlElement(ElementName = "Center")]
        public Center Center { get; set; }
        [XmlAttribute(AttributeName = "chord")]
        public string Chord { get; set; }
        [XmlAttribute(AttributeName = "crvType")]
        public string CrvType { get; set; }
        [XmlAttribute(AttributeName = "delta")]
        public string Delta { get; set; }
        [XmlAttribute(AttributeName = "dirEnd")]
        public string DirEnd { get; set; }
        [XmlAttribute(AttributeName = "dirStart")]
        public string DirStart { get; set; }
        [XmlElement(ElementName = "End")]
        public End End { get; set; }
        [XmlAttribute(AttributeName = "external")]
        public string External { get; set; }
        [XmlAttribute(AttributeName = "staStart")]
        public string StaStart { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "midOrd")]
        public string MidOrd { get; set; }
        [XmlElement(ElementName = "PI")]
        public string PI { get; set; }
        [XmlAttribute(AttributeName = "radius")]
        public string Radius { get; set; }
        [XmlAttribute(AttributeName = "rot")]
        public string Rot { get; set; }
        [XmlElement(ElementName = "Start")]
        public Start Start { get; set; }
        [XmlAttribute(AttributeName = "tangent")]
        public string Tangent { get; set; }
    }

    [XmlRoot(ElementName = "Spiral")]
    public class Spiral
    {
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "radiusEnd")]
        public string RadiusEnd { get; set; }
        [XmlAttribute(AttributeName = "radiusStart")]
        public string RadiusStart { get; set; }
        [XmlAttribute(AttributeName = "rot")]
        public string Rot { get; set; }
        [XmlAttribute(AttributeName = "spiType")]
        public string SpiType { get; set; }
        [XmlAttribute(AttributeName = "chord")]
        public string Chord { get; set; }
        [XmlAttribute(AttributeName = "constant")]
        public string Constant { get; set; }
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "dirEnd")]
        public string DirEnd { get; set; }
        [XmlAttribute(AttributeName = "dirStart")]
        public string DirStart { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "theta")]
        public string Theta { get; set; }
        [XmlAttribute(AttributeName = "totalY")]
        public string TotalY { get; set; }
        [XmlAttribute(AttributeName = "totalX")]
        public string TotalX { get; set; }
        [XmlAttribute(AttributeName = "staStart")]
        public string StaStart { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "tanLong")]
        public string TanLong { get; set; }
        [XmlAttribute(AttributeName = "tanShort")]
        public string TanShort { get; set; }
        [XmlAttribute(AttributeName = "oID")]
        public string OID { get; set; }
        [XmlElement(ElementName = "Start")]
        public Start Start { get; set; }
        [XmlElement(ElementName = "PI")]
        public string PI { get; set; }
        [XmlElement(ElementName = "End")]
        public Start End { get; set; }
    }

    [XmlRoot(ElementName = "Definition")]
    public class Definition
    {
        [XmlAttribute(AttributeName = "area2DSurf")]
        public string Area2DSurf { get; set; }
        [XmlAttribute(AttributeName = "area3DSurf")]
        public string Area3DSurf { get; set; }
        [XmlAttribute(AttributeName = "elevMax")]
        public string ElevMax { get; set; }
        [XmlAttribute(AttributeName = "elevMin")]
        public string ElevMin { get; set; }
        [XmlElement(ElementName = "Faces")]
        public Faces Faces { get; set; }
        [XmlElement(ElementName = "Pnts")]
        public Pnts Pnts { get; set; }
        [XmlAttribute(AttributeName = "surfType")]
        public string SurfType { get; set; }
    }
    [XmlRoot(ElementName = "End")]
    public class End
    {
        [XmlText] public string Point { get; set; }
        [XmlAttribute(AttributeName = "pntRef")]
        public string PntRef { get; set; }
    }

    [XmlRoot(ElementName = "DesignCrossSectSurf")]
    public class DesignCrossSectSurf
    {
        [XmlAttribute(AttributeName = "area")]
        public string Area { get; set; }
        [XmlAttribute(AttributeName = "closedArea")]
        public string ClosedArea { get; set; }
        [XmlElement(ElementName = "CrossSectPnt")]
        public List<CrossSectPnt> CrossSectPnt { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "side")]
        public string Side { get; set; }
    }

    [XmlRoot(ElementName = "Equipment")]
    public class Equipment
    {
        [XmlElement(ElementName = "InstrumentDetails")]
        public InstrumentDetails InstrumentDetails { get; set; }
    }

    [XmlRoot(ElementName = "F")]
    public class F
    {
        [XmlAttribute(AttributeName = "i")]
        public string I { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Faces")]
    public class Faces
    {
        [XmlElement(ElementName = "F")]
        public List<F> F { get; set; }
    }

    [XmlRoot(ElementName = "Imperial")]
    public class Imperial
    {
        [XmlAttribute(AttributeName = "angularUnit")]
        public string AngularUnit { get; set; }
        [XmlAttribute(AttributeName = "areaUnit")]
        public string AreaUnit { get; set; }
        [XmlAttribute(AttributeName = "diameterUnit")]
        public string DiameterUnit { get; set; }
        [XmlAttribute(AttributeName = "directionUnit")]
        public string DirectionUnit { get; set; }
        [XmlAttribute(AttributeName = "linearUnit")]
        public string LinearUnit { get; set; }
        [XmlAttribute(AttributeName = "pressureUnit")]
        public string PressureUnit { get; set; }
        [XmlAttribute(AttributeName = "temperatureUnit")]
        public string TemperatureUnit { get; set; }
        [XmlAttribute(AttributeName = "volumeUnit")]
        public string VolumeUnit { get; set; }
    }

    [XmlRoot(ElementName = "InstrumentDetails")]
    public class InstrumentDetails
    {
        [XmlElement(ElementName = "Corrections")]
        public string Corrections { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "InstrumentPoint")]
    public class InstrumentPoint
    {
        [XmlAttribute(AttributeName = "pntRef")]
        public string PntRef { get; set; }
    }

    [XmlRoot(ElementName = "InstrumentSetup")]
    public class InstrumentSetup
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "instrumentHeight")]
        public string InstrumentHeight { get; set; }
        [XmlElement(ElementName = "InstrumentPoint")]
        public InstrumentPoint InstrumentPoint { get; set; }
        [XmlAttribute(AttributeName = "stationName")]
        public string StationName { get; set; }
    }

    [XmlRoot(ElementName = "LandXML")]
    public class LandXML
    {
        [XmlElement(ElementName = "Alignments")]
        public Alignments Alignments { get; set; }
        [XmlElement(ElementName = "Application")]
        public Application Application { get; set; }
        [XmlElement(ElementName = "CgPoints")]
        public List<CgPoints> CgPoints { get; set; }
        [XmlElement(ElementName = "CoordinateSystem")]
        public CoordinateSystem CoordinateSystem { get; set; }
        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
        [XmlAttribute(AttributeName = "language")]
        public string Language { get; set; }
        [XmlElement(ElementName = "MaterialTable")]
        public MaterialTable MaterialTable { get; set; }
        [XmlElement(ElementName = "Parcels")]
        public Parcels Parcels { get; set; }
        [XmlElement(ElementName = "PlanFeatures")]
        public PlanFeatures PlanFeatures { get; set; }
        [XmlElement(ElementName = "Project")]
        public Project Project { get; set; }
        [XmlAttribute(AttributeName = "readOnly")]
        public string ReadOnly { get; set; }
        [XmlElement(ElementName = "Roadways")]
        public Roadways Roadways { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation")]
        public string SchemaLocation { get; set; }
        [XmlElement(ElementName = "Surfaces")]
        public Surfaces Surfaces { get; set; }
        [XmlElement(ElementName = "SymbolTable")]
        public SymbolTable SymbolTable { get; set; }
        [XmlElement(ElementName = "TextureImageTable")]
        public TextureImageTable TextureImageTable { get; set; }
        [XmlElement(ElementName = "Survey")]
        public Survey Survey { get; set; }
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
        [XmlElement(ElementName = "Units")]
        public Units Units { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "xsi")]
        public string Xsi { get; set; }
    }

    [XmlRoot(ElementName = "Line")]
    public class Line
    {
        [XmlAttribute(AttributeName = "dir")]
        public string Dir { get; set; }
        [XmlElement(ElementName = "End")]
        public End End { get; set; }
        [XmlElement(ElementName = "Start")]
        public Start Start { get; set; }
        [XmlAttribute(AttributeName = "staStart")]
        public string StaStart { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
    }

    [XmlRoot(ElementName = "Material")]
    public class Material
    {
        [XmlAttribute(AttributeName = "color")]
        public string Color { get; set; }
        [XmlAttribute(AttributeName = "index")]
        public string Index { get; set; }
        [XmlAttribute(AttributeName = "symbolRef")]
        public string SymbolRef { get; set; }
        [XmlAttribute(AttributeName = "symbolRotation")]
        public string SymbolRotation { get; set; }
        [XmlAttribute(AttributeName = "symbolXScale")]
        public string SymbolXScale { get; set; }
        [XmlAttribute(AttributeName = "symbolYScale")]
        public string SymbolYScale { get; set; }
        [XmlAttribute(AttributeName = "symbolZScale")]
        public string SymbolZScale { get; set; }
        [XmlAttribute(AttributeName = "textureImageRef")]
        public string TextureImageRef { get; set; }
        [XmlAttribute(AttributeName = "textureImageScale")]
        public string TextureImageScale { get; set; }
    }

    [XmlRoot(ElementName = "MaterialTable")]
    public class MaterialTable
    {
        [XmlElement(ElementName = "Material")]
        public List<Material> Material { get; set; }
    }

    [XmlRoot(ElementName = "Metric")]
    public class Metric
    {
        [XmlAttribute(AttributeName = "areaUnit")]
        public string AreaUnit { get; set; }
        [XmlAttribute(AttributeName = "linearUnit")]
        public string LinearUnit { get; set; }
        [XmlAttribute(AttributeName = "volumeUnit")]
        public string VolumeUnit { get; set; }
    }
    [XmlRoot(ElementName = "ObservationGroup")]
    public class ObservationGroup
    {
        [XmlElement(ElementName = "Backsight")]
        public Backsight Backsight { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "RawObservation")]
        public List<RawObservation> RawObservation { get; set; }
    }


    [XmlRoot(ElementName = "P")]
    public class P
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ParaCurve")]
    public class ParaCurve
    {
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlText]
        public string Point { get; set; }
    }

    [XmlRoot(ElementName = "PlanFeature")]
    public class PlanFeature
    {
        [XmlElement(ElementName = "CoordGeom")]
        public CoordGeom CoordGeom { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Parcel")]
    public class Parcel
    {
        [XmlAttribute(AttributeName = "area")]
        public string Area { get; set; }
        [XmlElement(ElementName = "Center")]
        public string Center { get; set; }
        [XmlElement(ElementName = "CoordGeom")]
        public CoordGeom CoordGeom { get; set; }
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Parcels")]
    public class Parcels
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Parcel")]
        public List<Parcel> Parcel { get; set; }
    }

    [XmlRoot(ElementName = "PlanFeatures")]
    public class PlanFeatures
    {
        [XmlElement(ElementName = "PlanFeature")]
        public List<PlanFeature> PlanFeature { get; set; }
    }

    [XmlRoot(ElementName = "Pnts")]
    public class Pnts
    {
        [XmlElement(ElementName = "P")]
        public List<P> P { get; set; }
    }

    [XmlRoot(ElementName = "ProfAlign")]
    public class ProfAlign
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "ParaCurve")]
        public List<ParaCurve> ParaCurve { get; set; }
        [XmlElement(ElementName = "PVI")]
        public List<string> PVI { get; set; }
    }

    [XmlRoot(ElementName = "Profile")]
    public class Profile
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "ProfAlign")]
        public List<ProfAlign> ProfAlign { get; set; }
        [XmlElement(ElementName = "ProfSurf")]
        public ProfSurf ProfSurf { get; set; }
    }

    [XmlRoot(ElementName = "ProfSurf")]
    public class ProfSurf
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "PntList2D")]
        public string PntList2D { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
    }

    [XmlRoot(ElementName = "Project")]
    public class Project
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "RawObservation")]
    public class RawObservation
    {
        [XmlAttribute(AttributeName = "directFace")]
        public string DirectFace { get; set; }
        [XmlAttribute(AttributeName = "horizAngle")]
        public string HorizAngle { get; set; }
        [XmlAttribute(AttributeName = "horizDistance")]
        public string HorizDistance { get; set; }
        [XmlAttribute(AttributeName = "slopeDistance")]
        public string SlopeDistance { get; set; }
        [XmlAttribute(AttributeName = "targetHeight")]
        public string TargetHeight { get; set; }
        [XmlElement(ElementName = "TargetPoint")]
        public TargetPoint TargetPoint { get; set; }
        [XmlAttribute(AttributeName = "vertDistance")]
        public string VertDistance { get; set; }
        [XmlAttribute(AttributeName = "zenithAngle")]
        public string ZenithAngle { get; set; }
    }
    [XmlRoot(ElementName = "Roadway")]
    public class Roadway
    {
        [XmlAttribute(AttributeName = "alignmentRefs")]
        public string AlignmentRefs { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Speeds")]
        public string Speeds { get; set; }
        [XmlAttribute(AttributeName = "staEnd")]
        public string StaEnd { get; set; }
        [XmlAttribute(AttributeName = "staStart")]
        public string StaStart { get; set; }
        [XmlAttribute(AttributeName = "surfaceRefs")]
        public string SurfaceRefs { get; set; }
    }

    [XmlRoot(ElementName = "SourceData")]
    public class SourceData
    {
        [XmlElement(ElementName = "Boundaries")]
        public Boundaries Boundaries { get; set; }
    }

    [XmlRoot(ElementName = "Start")]
    public class Start
    {
        [XmlText] public string Point { get; set; }
        [XmlAttribute(AttributeName = "pntRef")]
        public string PntRef { get; set; }
    }

    [XmlRoot(ElementName = "Survey")]
    public class Survey
    {
        [XmlElement(ElementName = "CgPoints")]
        public CgPoints CgPoints { get; set; }
        [XmlElement(ElementName = "Equipment")]
        public Equipment Equipment { get; set; }
        [XmlElement(ElementName = "InstrumentSetup")]
        public List<InstrumentSetup> InstrumentSetup { get; set; }
        [XmlElement(ElementName = "ObservationGroup")]
        public List<ObservationGroup> ObservationGroup { get; set; }
        [XmlElement(ElementName = "SurveyHeader")]
        public SurveyHeader SurveyHeader { get; set; }
    }

    [XmlRoot(ElementName = "Surface")]
    public class Surface
    {
        [XmlElement(ElementName = "Definition")]
        public Definition Definition { get; set; }
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "SourceData")]
        public SourceData SourceData { get; set; }
    }

    [XmlRoot(ElementName = "Surfaces")]
    public class Surfaces
    {
        [XmlElement(ElementName = "Surface")]
        public List<Surface> Surface { get; set; }
        [XmlElement(ElementName = "SurfVolumes")]
        public SurfVolumes SurfVolumes { get; set; }
    }

    [XmlRoot(ElementName = "SurfVolume", Namespace = "http://www.landxml.org/schema/LandXML-1.1")]
    public class SurfVolume
    {
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "surfBase")]
        public string SurfBase { get; set; }
        [XmlAttribute(AttributeName = "surfCompare")]
        public string SurfCompare { get; set; }
        [XmlAttribute(AttributeName = "volCut")]
        public string VolCut { get; set; }
        [XmlAttribute(AttributeName = "volFill")]
        public string VolFill { get; set; }
        [XmlAttribute(AttributeName = "volTotal")]
        public string VolTotal { get; set; }
    }

    [XmlRoot(ElementName = "SurfVolumes")]
    public class SurfVolumes
    {
        [XmlAttribute(AttributeName = "surfVolCalcMethod")]
        public string SurfVolCalcMethod { get; set; }
        [XmlElement(ElementName = "SurfVolume")]
        public SurfVolume SurfVolume { get; set; }
    }
    [XmlRoot(ElementName = "Symbol")]
    public class Symbol
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "SymbolHexString")]
        public string SymbolHexString { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }



    [XmlRoot(ElementName = "SymbolTable")]
    public class SymbolTable
    {
        [XmlElement(ElementName = "Symbol")]
        public Symbol Symbol { get; set; }
    }

    [XmlRoot(ElementName = "TextureImage")]
    public class TextureImage
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "TextureHexString")]
        public string TextureHexString { get; set; }
    }

    [XmlRoot(ElementName = "TextureImageTable")]
    public class TextureImageTable
    {
        [XmlElement(ElementName = "TextureImage")]
        public List<TextureImage> TextureImage { get; set; }
    }
    [XmlRoot(ElementName = "Roadways")]
    public class Roadways
    {
        [XmlElement(ElementName = "Roadway")]
        public Roadway Roadway { get; set; }
    }

    [XmlRoot(ElementName = "Superelevation")]
    public class Superelevation
    {
        [XmlElement(ElementName = "BeginRunoffSta")]
        public string BeginRunoffSta { get; set; }
        [XmlElement(ElementName = "BeginRunoutSta")]
        public List<string> BeginRunoutSta { get; set; }
        [XmlElement(ElementName = "EndofRunoutSta")]
        public List<string> EndofRunoutSta { get; set; }
        [XmlElement(ElementName = "FullSuperelev")]
        public string FullSuperelev { get; set; }
        [XmlElement(ElementName = "FullSuperSta")]
        public string FullSuperSta { get; set; }
        [XmlElement(ElementName = "RunoffSta")]
        public string RunoffSta { get; set; }
        [XmlElement(ElementName = "StartofRunoutSta")]
        public string StartofRunoutSta { get; set; }
    }






    [XmlRoot(ElementName = "SurveyHeader")]
    public class SurveyHeader
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "TargetPoint")]
    public class TargetPoint
    {
        [XmlAttribute(AttributeName = "desc")]
        public string Desc { get; set; }
        [XmlAttribute(AttributeName = "pntRef")]
        public string PntRef { get; set; }
        [XmlText]
        public string Text { get; set; }
    }


    [XmlRoot(ElementName = "Units")]
    public class Units
    {
        [XmlElement(ElementName = "Metric")]
        public Metric Metric { get; set; }

        [XmlElement(ElementName = "Imperial")]
        public Imperial Imperial { get; set; }
    }

}
