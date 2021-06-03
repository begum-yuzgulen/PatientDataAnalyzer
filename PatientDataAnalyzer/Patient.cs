using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientDataAnalyzer
{
    public class Patient
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("bodyHeight")]
        public double BodyHeight { get; set; }
        [JsonProperty("bodyWeight")]
        public double BodyWeight { get; set; }
        [JsonProperty("hemoglobin")]
        public double Hemoglobin { get; set; }
        [JsonProperty("whiteCellCount")]
        public double WhiteCellCount { get; set; }
        [JsonProperty("redCellCount")]
        public double RedCellCount { get; set; }
        [JsonProperty("creatinin")]
        public double Creatinin { get; set; }
        [JsonProperty("glucose")]
        public double Glucose { get; set; }
        [JsonProperty("calcium")]
        public double Calcium { get; set; }
        [JsonProperty("neutrophils")]
        public double Neutrophils { get; set; }


        public string toString()
        {
            return Name + DateOfBirth + Gender + City + Country + BodyHeight + BodyWeight + Hemoglobin + WhiteCellCount + RedCellCount + Creatinin + Glucose + Calcium + Neutrophils;
        }
    }
}
