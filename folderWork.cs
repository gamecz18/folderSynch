using Microsoft.Win32;
using System.IO;
using System.Windows;




namespace folderSynch
{
    static class folders
    {
        public static string sourseFolder = null;
        public static string destinacionFolder = null;
        public static int timeToSynch = 15000;
        public static int pocetZmen = 0;
        public static string jsemCesta { get { return Directory.GetCurrentDirectory(); } set { jsemCesta = value; } }

        public static void saveSettings()
        {
            System.Windows.Forms.MessageBox.Show(jsemCesta, "Path to clioboard saved ");
            Clipboard.SetText(jsemCesta);

            using (StreamWriter st1 = new StreamWriter(jsemCesta + "\\" + "setting.txt"))
            {



                st1.WriteLine($"Folders out:* {sourseFolder} ");
                st1.WriteLine($"Folders in:* {destinacionFolder}");
                st1.WriteLine($"Time to Synch:* {timeToSynch}");



            }




        }
        public static void loadSettings()
        {
            if (File.Exists(jsemCesta + "\\" + "setting.txt"))
            {
                using (StreamReader st1 = new StreamReader(jsemCesta + "\\" + "setting.txt"))
                {
                    string line;
                    string[] parts = new string[3];
                    int pomoc = 0;
                    while ((line = st1.ReadLine()) != null)
                    {
                        parts[pomoc] = line.Split(new string[] { ":*" }, System.StringSplitOptions.None)[1];
                        pomoc++;
                    }
                    folders.sourseFolder = parts[0].Trim();
                    folders.destinacionFolder = parts[1].Trim();
                    folders.timeToSynch = int.Parse(parts[2].Trim());

                }
                MainWindow.Instance.sourcePath.Content = $"Cesta: : {folders.sourseFolder}";
                MainWindow.Instance.desPath.Content = $"Cesta: : {folders.destinacionFolder}";
            }
           
        }

    }

}
static class folderWork
{



    static public bool selecFolder(ref string s)
    {

        var op = new OpenFolderDialog();

        if (op.ShowDialog().Value == true)
        {
            s = op.FolderName;
            return false;
        }
        return true;

    }
}

