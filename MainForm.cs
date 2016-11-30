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
        List<String> x;
        
        public MainForm()
        {
            //hiding= false;
            var formtemp = this;
            InitializeComponent();
            //testing things then moving them either in new methods or different location.
             x = new List<String>(ConfigurationManager.AppSettings["Grade1"].Split(new char[] { ';' }));

            getGradeNumber("grade 11");
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
       
       

        private void button1_Click(object sender, EventArgs e)
        {
            EditForm ef = new EditForm();
            ef.Show();
            
           
           
        }

        public string getGradeNumber(string name)
        {
            string[] temp = name.Split(new char[] {' '});
            string num = temp[1];
            return num;
        }
        public void initapp()
        {
            int size = ConfigurationManager.AppSettings.Count;
            string[] appkeys= new string[size];
            for(int i=0; i<size; i++)
            {
                appkeys[i]= GetKey(ConfigurationManager.AppSettings.GetKey(i));
                Console.WriteLine("appkey[" + i + "] is: " + appkeys[i]);
            }

        }

        public void splitting()
        {
            initapp();
            // Get a fresh copy of the sample PDF file
            string filename = "grade 1.pdf";
            String initial = Path.Combine("D:\\Homeworks\\testing struct", filename);
           

            string dest = Path.Combine("D:\\Homeworks\\testing struct\\debug", filename);
          

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
           
                // Add the page and save it
                outputDocument.AddPage(inputDocument.Pages[idx]);
                
                if (idx <= x.Count)
                {
                    outputDocument.Save(Path.Combine("D:\\Homeworks\\testing struct\\",name, String.Format("{0}{1}.pdf", getGradeNumber(name), x[idx])));
                }
                else
                {
                    outputDocument.Save(Path.Combine("D:\\Homeworks\\testing struct\\debug", String.Format("{0} - Unassigned {1}.pdf", name, idx+1)));
                }
            }
            Console.WriteLine("Deleting copied file");
            File.Delete(Path.Combine("D:\\Homeworks\\testing struct\\debug", filename));
        }
        private void button_Convert_Click(object sender, EventArgs e)
        {
            splitting();
        }
    }
}
