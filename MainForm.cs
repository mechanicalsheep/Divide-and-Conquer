using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;

namespace Divide_and_Conquer
{
    public partial class MainForm : Form
    {
        bool hiding;
        public MainForm()
        {
            hiding= false;
            var formtemp = this;
            InitializeComponent();
            //testing things then moving them either in new methods or different location.
            var x = new List<String>(ConfigurationManager.AppSettings["Grade1"].Split(new char[] { ';' }));
            for (int i = 0; i < x.Count; i++)
            {
                Console.WriteLine("Config Key of A is: " + x[i]);

            }
            Console.WriteLine(this.ToString());
        }

        private static string GetKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static void SetKey(string key, string value)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {
                    }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditForm ef = new EditForm();
            ef.Show();
            
           
           
        }
        public void splitting()
        {
            // Get a fresh copy of the sample PDF file
            string filename = "grade 1.pdf";
            String initial = Path.Combine("D:\\Homeworks\\testing struct", filename);
            //Console.WriteLine("initial file string= " + initial);

            string dest = Path.Combine("D:\\Homeworks\\testing struct\\debug", filename);
            //Console.WriteLine("destination string is: " + dest);

            File.Copy(Path.Combine("D:\\Homeworks\\testing struct", filename),
                Path.Combine("D:\\Homeworks\\testing struct\\debug", filename), true);
            
            // Open the file
            //changed filename arg to initial
            PdfDocument inputDocument = PdfReader.Open(dest, PdfDocumentOpenMode.Import);

            string name = Path.GetFileNameWithoutExtension(filename);
            for (int idx = 0; idx < inputDocument.PageCount; idx++)
            {
                // Create new document
                PdfDocument outputDocument = new PdfDocument();
                outputDocument.Version = inputDocument.Version;
                outputDocument.Info.Title =
                  String.Format("Page {0} of {1}", idx + 1, inputDocument.Info.Title);
                outputDocument.Info.Creator = inputDocument.Info.Creator;
               // PdfReader.Open(outputDocument)
                // Add the page and save it
                outputDocument.AddPage(inputDocument.Pages[idx]);
                outputDocument.Save(Path.Combine("D:\\Homeworks\\testing struct\\debug", String.Format("{0} - Page {1}.pdf", name, idx + 1)));
                //outputDocument.save
            }
        }
        private void button_Convert_Click(object sender, EventArgs e)
        {
            splitting();
        }
    }
}
