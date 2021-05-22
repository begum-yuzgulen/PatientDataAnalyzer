using ExcelMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;

namespace PatientDataAnalyzer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            ChosenFile = "No file chosen...";
            DataContext = this;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 

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
    }
}
