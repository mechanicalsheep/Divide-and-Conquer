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
    class Merger
    {
        string dest;
        public Merger()
        {
            dest = @"D:\Homeworks\testing struct\to merge";
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
            /* 
             * COMMENTING CODE TO TEST SPLITTER
            string[] files = GetFiles();

                // Open the output document
                PdfDocument outputDocument = new PdfDocument();

                // Iterate files
                foreach (string file in files)
                {
                    // Open the document to import pages from it.
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                }

                // Save the document...
                const string filename = "ConcatenatedDocument1_tempfile.pdf";
                outputDocument.Save(filename);
                // ...and start a viewer.
                Process.Start(filename);*/
            }

        
    }
}
