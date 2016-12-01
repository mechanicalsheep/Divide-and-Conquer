using System;
using System.Configuration;
using System.Windows.Forms;

namespace Divide_and_Conquer
{
    public partial class MainForm : Form
    {
        
       public MainForm()
        {
            InitializeComponent();
            
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
            Splitter s = new Divide_and_Conquer.Splitter();
            s.split();
        }
    }
}
