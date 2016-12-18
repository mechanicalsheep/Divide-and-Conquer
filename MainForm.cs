using System;
using System.Configuration;
using System.Windows.Forms;

namespace Divide_and_Conquer
{
    public partial class MainForm : Form
    {
        public void setstatus(string value)
        {
           this.statusbox.Text += value + Environment.NewLine; 
        }
        public MainForm()
        {

            InitializeComponent();
            setstatus( "Program Started...");
            
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
 
        
       
        private void button_Convert_Click(object sender, EventArgs e)
        {
            Splitter s = new Splitter(textBox1.Text, this);
            s.split();
        }

        private void button_Merge_Click(object sender, EventArgs e)
        {
            //TODO: calls m constructor for testing, after test is complete, call apropriate class
            Merger m = new Merger();

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
    }
}
