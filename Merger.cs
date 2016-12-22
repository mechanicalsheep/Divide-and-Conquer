using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Configuration;

namespace Divide_and_Conquer
{
    class Merger : MainForm
    {
        FileInfo arabic_Page, english_Page;
        string dest;
        string arabic_Path;
        pathset[] paths;
        string[] appkeys;

        public Merger(MainForm f, pathset[] path)
        {
            // arabic_Page = new FileInfo(@"D:\Homeworks\merge test\arabic page\u1a.pdf");
            //english_Page = new FileInfo(@"D:\Homeworks\merge test\english page\english.pdf");
            //dest = @"D:\Homeworks\merge test\";

            paths = path;
            
            deconstruct_pathset(paths[0]);

            Merge();
        }

        public void deconstruct_pathset(pathset element)
        {
            arabic_Path = element.get_arabicPath();
            
            english_Page = element.get_EnglishFile();
            dest = element.get_outputPath();
        }
      
        public void Merge()
        {


            DirectoryInfo dir = new DirectoryInfo(arabic_Path);
            foreach (FileInfo file in dir.GetFiles())
            {
                merging(file);
            }
        }
        public string getGradeNumber(string name)
        {
            string[] temp = name.Split(new char[] { ' ' });
            string num = temp[1];
            return num;
        }
        public void initMerge()
        {


            int size = ConfigurationManager.AppSettings.Count;

            /*
             * TEST CODE FOR APP.CONFIG
             *           
           */
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


        public void merging(FileInfo currentFile)
        {
            

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            arabic_Page = new FileInfo(currentFile.FullName);
            // Open the document to import pages from it.
            PdfDocument inputDocument = PdfReader.Open(arabic_Page.FullName, PdfDocumentOpenMode.Import);

            // Iterate pages
            int count = inputDocument.PageCount;
            for (int idx = 0; idx < count; idx++)
            {
                // Get the page from the external document...
                PdfPage page = inputDocument.Pages[idx];
                // ...and add it to the output document.
                outputDocument.AddPage(page);

            }

            PdfDocument englishDocument = PdfReader.Open(english_Page.FullName, PdfDocumentOpenMode.Import);
            count = englishDocument.PageCount;
            for (int idx = 0; idx < count; idx++)
            {
                // Get the page from the external document...
                PdfPage page = englishDocument.Pages[idx];
                // ...and add it to the output document.
                outputDocument.AddPage(page);

            }


            // Save the document...
            const string filename = "ConcatenatedDocument1_tempfile.pdf";
            outputDocument.Save(Path.Combine(dest, arabic_Page.Name)); 
            // ...and start a viewer.
            // Process.Start(filename);
        }
    }


    }



