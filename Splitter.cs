using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace Divide_and_Conquer
{
    class Splitter : MainForm
    {
        List<String> x;
        string[] appkeys;
        string dest;

        public Splitter()
        {
            var formtemp = this;
            initSplit();
        }

        //returns number of the grade (For example "Grade 1" will return 1, used for naming split pages
        public string getGradeNumber(string name)
        {
            string[] temp = name.Split(new char[] { ' ' });
            string num = temp[1];
            return num;
        }

        //initialize splitting variables
        public void initSplit()
        {
            //for now, dest is set to testing struct in D:\homeworks
            dest = @"D:\Homeworks\testing struct";
            int size = ConfigurationManager.AppSettings.Count;
            appkeys = new string[size];
            for (int i = 0; i < size; i++)
            {
                appkeys[i] = ConfigurationManager.AppSettings.GetKey(i);
            }
        }

        //returns the key name in a certain appkeys[index]
        public string getKeyName(int index)
        {
            return appkeys[index];
        }

        //initialize the documents and some vairables related to the splitting of files.
        public void split()
        {

            DirectoryInfo accessfile = new DirectoryInfo(dest);
            int index = 0;
            foreach (FileInfo fi in accessfile.GetFiles())
            {
                x = new List<String>(ConfigurationManager.AppSettings[getKeyName(index)].Split(new char[] { ';' }));
                //Call the splitting page process to each file in the root folder
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
                catch (System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("**************WARNING, ARGUMENT OUT OF RANGE EXCEPTION THROWN**************");
                    Console.WriteLine(e.ToString());
                }
            }
            //inputDocument.Close();

            Console.WriteLine("Deleting copied file");
            File.Delete(Path.Combine("D:\\Homeworks\\testing struct\\debug", filename));
        }
    }
}
