using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Forms = System.Windows.Forms;
using Path = System.IO.Path;

namespace folderSynch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;

        private readonly Forms.NotifyIcon _nf;
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            folders.loadSettings();
            _nf = new Forms.NotifyIcon();
            _nf.Icon = new System.Drawing.Icon("images/icon.ico");
            _nf.Text = "Folder Synch APP";
            _nf.ContextMenuStrip = new Forms.ContextMenuStrip();
            _nf.ContextMenuStrip.Items.Add("Stop", null, NotifyIcon_Click);
            
            

        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
            _nf.Visible = false;


        }

        public delegate void MethodInvoker();

        
        protected override void OnClosing(CancelEventArgs e)
        {
            _nf.Dispose();
            base.OnClosing(e);
        }




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


        private void synchButton_Click(object sender, RoutedEventArgs e)
        {


            sych();


        }

        async void sych()
        {

            if (folders.destinacionFolder == null || folders.sourseFolder == null)
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
                    await Task.Run(() => synch.copyFiles(prubeh));


                }
                catch (System.Exception err)
                {

                    Forms.MessageBox.Show(err.Message, "Nastala chybu u synch");
                    disEnabElement(true);
                }

            });





        }
        public void disEnabElement(bool operand)
        {
            this.synchButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () =>
            {
                synchButton.IsEnabled = operand;
            });
            this.synchButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () =>
            {
                buttonDes.IsEnabled = operand;
            });
            this.synchButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () =>
            {
                buttonSec.IsEnabled = operand;
            });
            this.synchButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () =>
            {
                synchOnBackButton.IsEnabled = operand;
            });




        }



        public  async void  reload()
        {

            if (folders.destinacionFolder == null) return;
            this.desctiFilesView.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                      () => {
                          desctiFilesView.Items.Clear();
                      });
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
            this.desCount.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, () => { desCount.Content = $"Počet s.: {pocet}"; });
                

        }
        private void reloadButton_Click(object sender, RoutedEventArgs e)
        {
            reload();
            
        }

        private void saveSesButton_Click(object sender, RoutedEventArgs e)
        {
            folders.saveSettings();
        }


        void createIcon()
        {


         
            
            
            //_nf.Click += NotifyIcon_Click;
            _nf.Visible = true;

            //this.Visibility = Visibility.Visible;
        }

        void cancleIcon()
        {
            _nf.Visible =false;
        }


        private void synchOnBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            createIcon();
        }
    }


}
