using System;
using System.Collections.Generic;
using System.Text;

namespace PatientDataAnalyzer
{
    public class Patient
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double BodyHeight { get; set; }
        public double BodyWeight { get; set; }
        public double Hemoglobin { get; set; }
        public double WhiteCellCount { get; set; }
        public double RedCellCount { get; set; }
        public double Creatinin { get; set; }
        public double Glucose { get; set; }
        public double Calcium { get; set; }
        public double Neutrophils { get; set; }


        public string toString()
        {
            return Name + DateOfBirth + Gender + City + Country + BodyHeight + BodyWeight + Hemoglobin + WhiteCellCount + RedCellCount + Creatinin + Glucose + Calcium + Neutrophils;
        }
    }
}
