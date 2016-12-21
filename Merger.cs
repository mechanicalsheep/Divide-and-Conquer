using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;

namespace Divide_and_Conquer
{
    class Merger : MainForm
    {
        string dest;
        string[] filePaths;
        public Merger(MainForm f, string sourceFolder, string[] englishPaths)
        {
            dest = sourceFolder;
            MainForm form = f;
            filePaths = englishPaths;


        }

        public void fileGetter()
        {
            DirectoryInfo accessfile = new DirectoryInfo(dest);
        }


        public void Merge()
        {
            /// <summary>
            /// Imports all pages from a list of documents.
            /// </summary>
            ///
            // Get some file names

            //NOT NEEDED, TEST ON filePaths[0] for now // string[] files = GetFiles();

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files

            DirectoryInfo folder = new DirectoryInfo(dest);
            foreach (FileInfo file in folder.GetFiles())
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file.FullName, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                   
                }
                //close current inputdocument
                inputDocument.Close();
                PdfDocument englishDocument = PdfReader.Open(filePaths[0], PdfDocumentOpenMode.Import);
                count = englishDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = englishDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);

                }


                // Save the document...
                const string filename = "D:\\ConcatenatedDocument1_tempfile.pdf";
                outputDocument.Save(filename);
                // ...and start a viewer.
               // Process.Start(filename);
            }


        }
    }
}
