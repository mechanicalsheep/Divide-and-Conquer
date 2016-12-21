using System;
using System.Configuration;
using System.Windows.Forms;
using System.IO;

namespace Divide_and_Conquer
{
    public partial class MainForm : Form
    {
        public string[] filer = new string[3];
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

        private void button2_Click(object sender, EventArgs e)
        {
            setstatus( "mew");
            Splitter sp = new Splitter(textBox1.Text, this);
            
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
                filer[0]=dlg.FileName;
                setstatus(dlg.FileName);
                button_g1.BackColor = System.Drawing.Color.Green;
                button_g1.ForeColor = System.Drawing.Color.White;
                
            }
           
        }

        private void button_Merge_Click(object sender, EventArgs e)
        {
            Merger m = new Merger(filer, this);
        }
    }
}
