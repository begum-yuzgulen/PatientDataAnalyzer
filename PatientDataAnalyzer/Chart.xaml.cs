using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PatientDataAnalyzer
{
    public partial class Chart : Window
    {
        public Chart(String chartType, List<String> labels, String dataset)
        {
            InitializeComponent();
            HttpClient httpClient = new HttpClient();
            var url_string = "https://quickchart.io/chart?c={type:'" + chartType + "',data:{labels:[" + String.Join(",", labels) + "], datasets:[" + dataset + "]}}";
            var response = httpClient.GetAsync(url_string).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Failed req {response.StatusCode}");
            }
            UserImage = response.Content.ReadAsByteArrayAsync().Result;
            DataContext = this;
        }
        public byte[] UserImage { get; set; }

        private void Download_Chart(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                File.WriteAllBytes(dialog.FileName + "\\chart.png", UserImage);
                MessageBox.Show("Downloaded chart at location: " + dialog.FileName + "\\chart.png");
            }
        }
    }
}
