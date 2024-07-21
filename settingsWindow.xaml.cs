using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace folderSynch
{
    /// <summary>
    /// Interakční logika pro settingsWindow.xaml
    /// </summary>
    public partial class settingsWindow : Window
    {
        public settingsWindow()
        {
            InitializeComponent();

            for (int i = 1; i != 16; i++)
            {
                time_to_Synch.Items.Add(i).ToString();

            }
            time_to_Synch.SelectedIndex = 14;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            saveSeting();
            folders.saveSettings();
            DialogResult = true;
            this.Close();   
        }


        void saveSeting()
        {
            folders.timeToSynch = int.Parse(time_to_Synch.SelectedValue.ToString()) * 60000;
            if (!string.IsNullOrEmpty(inputTextBox.Text))
            {
                folders.jmenoInstance = inputTextBox.Text;
            }
            if (bootChechBox.IsChecked.Value)
            {
                folders.bootOnStartup = true;
                folders.creteBoot();
            }
            else
            {
                folders.bootOnStartup = false;
            }
            


        }
    }
}
