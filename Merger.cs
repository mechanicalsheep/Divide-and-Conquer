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
        pathset paths;
        

        public Merger(FileInfo file, string arpath, string outpath)
        {
            // arabic_Page = new FileInfo(@"D:\Homeworks\merge test\arabic page\u1a.pdf");
            //english_Page = new FileInfo(@"D:\Homeworks\merge test\english page\english.pdf");
            //dest = @"D:\Homeworks\merge test\";

            //changed this for single input instead of array of pathsets
            //  paths = path;

            english_Page = file;
            arabic_Path = arpath;
            dest = outpath;

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

            //////////////////BAD CODING 


            //Console.WriteLine("Paths[" + i + "] is: " + paths);
           // deconstruct_pathset(paths);
            DirectoryInfo dir = new DirectoryInfo(arabic_Path);
            foreach (FileInfo file in dir.GetFiles())
            {
                merging(file);
            }
        
    

            /////////////////BAD CODING


           /* for (int i = 0; i < paths.Length; i++)
            {
                //Console.WriteLine("Paths[" + i + "] is: " + paths);
                deconstruct_pathset(paths[i]);
                DirectoryInfo dir = new DirectoryInfo(arabic_Path);
                foreach (FileInfo file in dir.GetFiles())
                {
                    merging(file);
                }
            }*/
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
            // const string filename = "ConcatenatedDocument1_tempfile.pdf";
            string[] temp = arabic_Page.Name.Split(new char[] { ' ' });
            string name = temp[1];
            outputDocument.Save(Path.Combine(dest, name)); 
            // ...and start a viewer.
            // Process.Start(filename);
        }
    }


    }



