using Microsoft.Win32;
using System.IO;
using System.Windows;




namespace folderSynch
{
    static class folders
    {
        public static string sourseFolder = null;
        public static string destinacionFolder = null;
        public static int pocetZmen = 0;
        public static string jsemCesta { get { return Directory.GetCurrentDirectory(); } set { jsemCesta = value; } }

        public static void saveSettings()
        {
            System.Windows.Forms.MessageBox.Show(jsemCesta, "Path to clioboard save ");
            Clipboard.SetText(jsemCesta);

            using (StreamWriter st1 = new StreamWriter(jsemCesta + "\\"+ "setting.txt"))
            { 
                

               
               st1.WriteLine($"Folders out:* {sourseFolder} ");
               st1.WriteLine($"Folders in:* {destinacionFolder}");

              
            
            }

            File.WriteAllText($"{jsemCesta}\\settings.txt", "s");


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
}
