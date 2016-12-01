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
        string[] appkeys;
        string dest;
       public MainForm()
        {
            
            var formtemp = this;
            InitializeComponent();
            //testing things then moving them either in new methods or different location.
             x = new List<String>(ConfigurationManager.AppSettings["Grade1"].Split(new char[] { ';' }));
            initvar();
            initapp();
            
           
        }
        //initialize variables
        public void initvar()
        {
            dest= @"D:\Homeworks\testing struct";
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
            appkeys= new string[size];
            for(int i=0; i<size; i++)
            {
                appkeys[i]= ConfigurationManager.AppSettings.GetKey(i);
                Console.WriteLine("appkey[" + i + "] is: " + appkeys[i]);
            }

        }

        public void testsplit()
        {
            
            DirectoryInfo accessfile = new DirectoryInfo(dest);
            foreach(FileInfo fi in accessfile.GetFiles())
            {
                splitting(fi);
            }
            
            
        }
        public void splitting(FileInfo currentFile)
        {
            string filename = currentFile.Name;
            string name = Path.GetFileNameWithoutExtension(filename);
            Console.WriteLine("File name is now " + filename);
            // Get a fresh copy of the sample PDF file
            

            //copy leads to the place where the file was copied.
            String copy = Path.Combine("D:\\Homeworks\\testing struct\\debug", filename);


            
            File.Copy(Path.Combine("D:\\Homeworks\\testing struct", filename),
                Path.Combine(copy), true);
            
            // Open the file
            //changed filename arg to initial
            PdfDocument inputDocument = PdfReader.Open(copy, PdfDocumentOpenMode.Import);

           
            for (int idx = 0; idx < inputDocument.PageCount; idx++)
            {
                try
                {
                    Console.WriteLine("strind dest = " + Path.Combine("D:\\Homeworks\\testing struct\\", name, string.Format("{0}{1}.pdf", getGradeNumber(name), x[idx])) + "      idx[" + idx + "]");
                    string dest = Path.Combine("D:\\Homeworks\\testing struct\\", name, string.Format("{0}{1}.pdf", getGradeNumber(name), x[idx]));
                   
                    // Create new document
                    PdfDocument outputDocument = new PdfDocument();
                    outputDocument.Version = inputDocument.Version;

                    outputDocument.Info.Title =
                   String.Format("Page {0} of {1}", idx + 1, inputDocument.Info.Title);

                    outputDocument.Info.Creator = inputDocument.Info.Creator;

                    // Add the page and save it
                    outputDocument.AddPage(inputDocument.Pages[idx]);

                    //////////////added minus 1 here instead of idx because idx =1
                    if (idx <= x.Count)
                    {
                        outputDocument.Save(Path.Combine("D:\\Homeworks\\testing struct\\", name, string.Format("{0}{1}.pdf", getGradeNumber(name), x[idx])));
                        outputDocument.Close();
                    }
                    else
                    {
                        outputDocument.Save(Path.Combine("D:\\Homeworks\\testing struct\\debug", String.Format("{0} - Unassigned {1}.pdf", name, idx)));//+ 1)));
                    }
                }
                catch(System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("**************WARNING, ARGUMENT OUT OF RANGE EXCEPTION THROWN**************");
                    Console.WriteLine(e.ToString());
                }
            }
            //inputDocument.Close();
        
            Console.WriteLine("Deleting copied file");
            File.Delete(Path.Combine("D:\\Homeworks\\testing struct\\debug", filename));
        }
        private void button_Convert_Click(object sender, EventArgs e)
        {
            //splitting();
            testsplit();
        }
    }
}
