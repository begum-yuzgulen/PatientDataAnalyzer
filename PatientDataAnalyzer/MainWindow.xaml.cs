using ExcelMapper;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;

namespace PatientDataAnalyzer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Connect_To_Database();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            
            ChosenFile = "No file chosen...";
            DataContext = this;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                SelectedFile.Text = filename;
                try
                {
                    using var stream = File.OpenRead(dlg.FileName);
                    using var importer = new ExcelImporter(stream);

                    ExcelSheet sheet = importer.ReadSheet();
                    Patients = sheet.ReadRows<Patient>().ToArray();
                    Patient1.Text = "Found and inserted " + Patients.Length + " patients.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                DataContext = this;
            }
        }
        public string ChosenFile { get; set; }
        public Patient[] Patients { get; set; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChartForm mw = new ChartForm(Patients);
            mw.Show();
            this.Close();
        }

        public class PatientData
        {
            public Dictionary<String, List<Patient>> Patients { get; set; }
        }
        private async void Connect_To_Database()
        {
            var firebase = new FirebaseClient("https://patientdataanalyzer-default-rtdb.europe-west1.firebasedatabase.app/");
            var dinos = await firebase.Child("patient/0").OnceAsync<Patient>();

            MessageBox.Show(dinos.ToString());
        }

    }
}
