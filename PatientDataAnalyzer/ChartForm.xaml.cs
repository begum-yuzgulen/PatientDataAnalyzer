using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PatientDataAnalyzer
{

    public partial  class ChartForm : Window
    {
        public ChartForm(Patient[] patients)
        {
            InitializeComponent();
            dgPatients.DataContext = patients;
        }

        public byte[] UserImage { get; set; }

        private string Format_Value(double value)
        {
            return "'" + value.ToString() + "'";
        }

        private string Format_Dataset(IDictionary<string, List<string>> values)
        {
            var results = new List<string> { };

            foreach (KeyValuePair<string, List<string>> entry in values)
            {
                var data = "{label:'" + entry.Key + "', data:[" + String.Join(",", entry.Value) + "]}";
                results.Add(data);

            }

            return String.Join(",", results);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            var labels = new List<string> { };
            if (WhiteCells.IsChecked == true) labels.Add("'" + WhiteCells.Content + "'");
            if (RedCells.IsChecked == true) labels.Add("'" + RedCells.Content + "'");
            if (Hemoglobin.IsChecked == true) labels.Add("'" + Hemoglobin.Content + "'");
            if (Creatinin.IsChecked == true) labels.Add("'" + Creatinin.Content + "'");
            if (Glucose.IsChecked == true) labels.Add("'" + Glucose.Content + "'");
            if (Calcium.IsChecked == true) labels.Add("'" + Calcium.Content + "'");
            if (Neutrophils.IsChecked == true) labels.Add("'" + Neutrophils.Content + "'");

            IDictionary<string, List<string>> PatientValues = new Dictionary<string, List<string>> { };
            foreach (Patient p in Patients)
            {
                var p_values = new List<string> { };
                if(WhiteCells.IsChecked == true) p_values.Add(Format_Value(p.WhiteCellCount));
                if (RedCells.IsChecked == true) p_values.Add(Format_Value(p.RedCellCount));
                if (Hemoglobin.IsChecked == true) p_values.Add(Format_Value(p.Hemoglobin));
                if (Creatinin.IsChecked == true) p_values.Add(Format_Value(p.Creatinin));
                if (Glucose.IsChecked == true) p_values.Add(Format_Value(p.Glucose));
                if (Calcium.IsChecked == true) p_values.Add(Format_Value(p.Calcium));
                if (Neutrophils.IsChecked == true) p_values.Add(Format_Value(p.Neutrophils));
                if (p_values != null) PatientValues.Add(new KeyValuePair<string, List<string>>(p.Name, p_values));
            }

            var dataset = Format_Dataset(PatientValues);
            
            PatientNames.Content = dataset;
            var url_string = "https://quickchart.io/chart?c={type:'bar',data:{labels:[" + String.Join(",", labels) + "], datasets:[" + dataset + "]}}";
            var response = httpClient.GetAsync(url_string).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Failed req {response.StatusCode}");
            }
            UserImage = response.Content.ReadAsByteArrayAsync().Result;
            DataContext = this;
        }
        public List<Patient> Patients { get; set; }
        public String patient_names { get; set; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void dgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            patient_names = "";
            Patients = new List<Patient> { };
            for (int i = 0; i < dgPatients.SelectedItems.Count; i++)
            {
                Patients.Add((Patient)dgPatients.SelectedItems[i]);
                String name = ((Patient)dgPatients.SelectedItems[i]).Name;
                patient_names += name;
            }
            PatientNames.Content = patient_names;
        }

    }
}
