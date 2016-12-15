using System;
using System.Collections.Generic;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Configuration;

namespace Divide_and_Conquer
{
    class Splitter : MainForm
    {
        List<String> x;
        string[] appkeys;
        string dest;
        MainForm superForm;
        //constructor accepts string to where the folder will be in order to split.
        public Splitter(string sourcefolder, MainForm x)
        {
            superForm = x;
            dest = sourcefolder;
            var formtemp = this;
            
            superForm.setstatus( "splitter called");
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
            /*
             * Commented for now, dest variable relies on browse folder choice or default textbox1.text
            dest = @"D:\Homeworks\testing struct";
            */
            int size = ConfigurationManager.AppSettings.Count;
           
            /*
             * TEST CODE FOR APP.CONFIG
            //var sizeofmeow = ConfigurationManager.GetSection("kitty") as NameValueConfigurationCollection;
            //Console.WriteLine("KITTTYYYYYYY= "+sizeofmeow["kitty"].ToString());
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
        public string getDest()
        {
            return dest;
        }
        public void splitting(FileInfo currentFile)
        {
            string filename = currentFile.Name;
            string name = Path.GetFileNameWithoutExtension(filename);

         superForm.setstatus("File name is now: " + filename);
            // Get a fresh copy of the sample PDF file

            /*
             * change the hardcoded location to dest.
            //copy leads to the place where the file was copied.
            String copy = Path.Combine("D:\\Homeworks\\testing struct\\debug", filename);
            File.Copy(Path.Combine("D:\\Homeworks\\testing struct", filename), Path.Combine(copy), true);
            */

            string copy = Path.Combine(dest+ "\\debug", filename);
            File.Copy(Path.Combine(dest, filename), Path.Combine(copy), true);
            // Open the file
            PdfDocument inputDocument = PdfReader.Open(copy, PdfDocumentOpenMode.Import);

            //go throughfile and split each page into respective grade using keys from app.config
            for (int idx = 0; idx < inputDocument.PageCount; idx++)
            {
                try
                {
                    Console.WriteLine(" " + string.Format("{0}{1}.pdf", getGradeNumber(name), x[idx]));
                   //testing purposes, shows full path and file name as well as index of Key idx[(int)]
                    // superForm.setstatus("string dest = " + Path.Combine(getDest(), name, string.Format("{0}{1}.pdf", getGradeNumber(name), x[idx])) + "      idx[" + idx + "]");
                    string dest = Path.Combine(getDest(), name, string.Format("{0}{1}.pdf", getGradeNumber(name), x[idx]));
                    superForm.setstatus("Splitting " + string.Format("{0}{1}.pdf",getGradeNumber(name),x[idx]));
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
                        if (Convert.ToInt32(getGradeNumber(name)) < 5)
                        {
                            outputDocument.Save(Path.Combine(getDest(), name, string.Format("{0}{1}{2}.pdf", "unjoined ", getGradeNumber(name), x[idx])));
                            outputDocument.Close();
                        }
                        else
                        {
                            outputDocument.Save(Path.Combine(getDest(), name, string.Format("{0}{1}.pdf", getGradeNumber(name), x[idx])));
                            outputDocument.Close();
                        }
                    }
                    else
                    {
                        outputDocument.Save(Path.Combine(dest+"\\debug", String.Format("{0} - Unassigned {1}.pdf", name, idx)));//+ 1)));
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
            File.Delete(Path.Combine(getDest()+"\\debug", filename));
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Splitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(526, 375);
            this.Name = "Splitter";
            this.ResumeLayout(false);

        }
    }
}
