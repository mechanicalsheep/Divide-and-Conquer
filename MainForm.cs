using System;
using System.Configuration;
using System.Windows.Forms;
using System.IO;

namespace Divide_and_Conquer
{
    
    public partial class MainForm : Form
    {
        public pathset[] filer = new pathset[4];
        public void setstatus(string value)
        {
           this.statusbox.AppendText( value + Environment.NewLine);
            statusbox_Merger.AppendText(value + Environment.NewLine);
           
            
        }

        
        public MainForm()
        {

            InitializeComponent();
         
            setstatus( "Program Started...");

            //create a string to store all the paths to merge
          

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
       


private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl.TabPages[tabControl.SelectedIndex].Controls.Add(statusbox);
          
               
            
        }

        private void button_Convert_Click(object sender, EventArgs e)
        {
            Splitter s = new Splitter(textBox1.Text, this);
            s.split();
        }

       

        private void button_Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.ShowDialog();
         
                string filename = dlg.SelectedPath;
                textBox1.Text = filename;
            
        }

        public void open_file()
        {

        }
       

        private void button_g1_Click(object sender, EventArgs e)
        {
            
            FileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter="PDF Files (*.pdf) | *.pdf";

            dlg.ShowDialog();
            bool fileExists = dlg.CheckFileExists;
            if (fileExists)
            {
                FileInfo file = new FileInfo(dlg.FileName);
                //sets up a single pathSet object
                string arpath = textBox1.Text+ "\\Grade 1";
                //Console.WriteLine("arabic path is: " + arpath);
                string outpath = arpath+ "\\merged";

                filer[0]=new pathset(file,arpath, outpath) ;
                setstatus(dlg.FileName);
                button_g1.BackColor = System.Drawing.Color.Green;
                button_g1.ForeColor = System.Drawing.Color.White;
                
            }
            
           
        }

        private void button_Merge_Click(object sender, EventArgs e)
        {
            Merger m = new Merger(this, filer);
          //  testing t = new testing();
        }
    }



    public class pathset
    {
       public FileInfo get_EnglishFile()
        {
            return englishFile;
        }
        public void set_EnglishFile(FileInfo file)
        {
            englishFile = file;
        }

        public string get_arabicPath()
        {
            return arabicPath;
        }
        public void set_arabicPath(string arab)
        {
            arabicPath = arab;
        }
        public string get_outputPath()
        {
            return outputPath;
        }
        public void set_outputPath(string outp)
        {
            outputPath = outp;
        }

        FileInfo englishFile;
        string arabicPath;
        string outputPath;

        public pathset(FileInfo engFile, string arabPath, string outpath)
        {
            englishFile = engFile;
            arabicPath = arabPath;
            outputPath = outpath;
        }
    }
}
