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

            initSplit();
            
           
        }
        //initialize variables
       
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


        public void initSplit()
        {
            dest = @"D:\Homeworks\testing struct";
            int size = ConfigurationManager.AppSettings.Count;
            appkeys = new string[size];
            for (int i = 0; i < size; i++)
            {
                appkeys[i] = ConfigurationManager.AppSettings.GetKey(i);
                //Console.WriteLine("appkey[" + i + "] is: " + appkeys[i]);
            }
        }

        public string getKeyName(int index)
        {
            return appkeys[index];
        }

        //initialize the documents and some vairables related to the splitting of files.
        public void split()
        {
            
            DirectoryInfo accessfile = new DirectoryInfo(dest);
            int index=0;
            foreach (FileInfo fi in accessfile.GetFiles())
            {
                x = new List<String>(ConfigurationManager.AppSettings[getKeyName(index)].Split(new char[] { ';' }));
                splitting(fi);
                index++;
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
            File.Copy(Path.Combine("D:\\Homeworks\\testing struct", filename), Path.Combine(copy), true);
            
            // Open the file
            PdfDocument inputDocument = PdfReader.Open(copy, PdfDocumentOpenMode.Import);

           //go throughfile and split each page into respective grade using keys from app.config
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
            split();
        }
    }
}
