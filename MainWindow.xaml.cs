using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Path = System.IO.Path;

namespace folderSynch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance = new MainWindow();  
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        public delegate void MethodInvoker();

        private async void buttonSec_Click(object sender, RoutedEventArgs e)
        {
            if (folderWork.selecFolder(ref folders.sourseFolder))
            {
                return;
            }
            int pocet = 0;
            sourceFilesView.Items.Clear();
            if (folders.sourseFolder == null) return;
            await Task.Run(() =>
            {

                foreach (var item in Directory.GetFiles(folders.sourseFolder))
                {
                    pocet++;

                    this.sourceFilesView.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        () => { sourceFilesView.Items.Add(Path.GetFileName(item)); });
                }
            });
            sourceCount.Content = $"Počet s.: {pocet}";
            sourcePath.Content = $"Cesta: : {folders.sourseFolder}";
        }

        private async void buttonDes_Click(object sender, RoutedEventArgs e)
        {
            
            if (folderWork.selecFolder(ref folders.destinacionFolder))
            {

                return;
            }
            int pocet = 0;
            desctiFilesView.Items.Clear();
            if (folders.destinacionFolder == null) return;
            await Task.Run(() =>
            {

                foreach (var item in Directory.GetFiles(folders.destinacionFolder))
                {
                    pocet++;

                    this.desctiFilesView.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        () => { desctiFilesView.Items.Add(Path.GetFileName(item)); });

                }
            });
            desCount.Content = $"Počet s.: {pocet}";
            desPath.Content = $"Cesta: : {folders.destinacionFolder}";

        }


        private  void synchButton_Click(object sender, RoutedEventArgs e)
        {
          
       
            sych();
            
           
        }

        async void sych() {

            if (folders.destinacionFolder == null ||folders.sourseFolder == null)
            {
                System.Windows.Forms.MessageBox.Show("Jedna šložka nebo více složek není vybráno", "Warning", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            
            
            disEnabElement(false);
            await Task.Run(async () =>
            {
                
                try
                {
                    folders.pocetZmen = 0;
                    synch.checkFiles(folders.sourseFolder, true, ref synch.sourceInfo);
                    synch.checkFiles(folders.destinacionFolder, false, ref synch.desInfo);
                    await Task.Run(() =>synch.copyFiles(prubeh));
                    
                    
                }
                catch (System.Exception err)
                {

                    MessageBox.Show(err.Message, "Nastala chybu u synch");
                }
                
            });
          
            



        }
        public void disEnabElement(bool operand)
        {
            this.synchButton.Dispatcher.Invoke( System.Windows.Threading.DispatcherPriority.Normal, () => {
                synchButton.IsEnabled = operand;
            });
            this.synchButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () => {
                buttonDes.IsEnabled = operand;
            });
            this.synchButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () => {
                buttonSec.IsEnabled = operand;
            });
            this.synchButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () => {
               synchOnBackButton.IsEnabled = operand;
            });
            



        }



        private async void  reloadButton_Click(object sender, RoutedEventArgs e)
        {

            if (folders.destinacionFolder == null) return;
            desctiFilesView.Items.Clear();
            int pocet = 0;
            await Task.Run(() =>
            {

                foreach (var item in Directory.GetFiles(folders.destinacionFolder))
                {
                    pocet++;

                    this.desctiFilesView.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        () => { desctiFilesView.Items.Add(Path.GetFileName(item)); });

                }
            });
            desCount.Content = $"Počet s.: {pocet}";
        }

        private void saveSesButton_Click(object sender, RoutedEventArgs e)
        {
            folders.saveSettings();
        }
    }


}
