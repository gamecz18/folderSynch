﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;




namespace folderSynch
{
    static class folders
    {
        public static string sourseFolder = null;
        public static string destinacionFolder = null;
        public static int timeToSynch = 900000;
        public static string jmenoInstance {  get; set; }
        public static bool bootOnStartup = false;
        public static int pocetZmen = 0;
        public static string jsemCesta { get { return Directory.GetCurrentDirectory(); } set { jsemCesta = value; } }



        public static void creteBoot()
        {
            string cesta = Directory.GetCurrentDirectory();
            // vytvozi se objekt 
            IWshRuntimeLibrary.WshShell w1 = new IWshRuntimeLibrary.WshShell();
            //dalsi objekt // ktery se urci cesta kam se ma zastupce dat
            IWshRuntimeLibrary.IWshShortcut s1 = (IWshRuntimeLibrary.IWshShortcut)w1.CreateShortcut($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Start Menu\Programs\Startup\{jmenoInstance}.lnk");
            //zaspuce otevre
            s1.TargetPath = $@"{cesta}\folderSynch.exe";
            s1.WorkingDirectory = $@"{cesta}";
            //ulozi
            s1.Save();
            Clipboard.SetText($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Start Menu\Programs\Startup");
            System.Windows.Forms.MessageBox.Show(jsemCesta, "Boot path to clioboard saved ");
           

        }
        public static void saveSettings()
        {
            Clipboard.SetText(jsemCesta);
            System.Windows.Forms.MessageBox.Show(jsemCesta, "Path to clioboard saved ");
           

            using (StreamWriter st1 = new StreamWriter(jsemCesta + "\\" + "setting.txt"))
            {



                st1.WriteLine($"Folders out:* {sourseFolder} ");
                st1.WriteLine($"Folders in:* {destinacionFolder}");
                st1.WriteLine($"Time to Synch:* {timeToSynch}");
                st1.WriteLine($"Instance:* {jmenoInstance}");
                st1.WriteLine($"Statup:* {bootOnStartup}");
                


            }




        }
        public static void loadSettings()
        {       //pokud cesta existuje
            if (File.Exists(jsemCesta + "\\" + "setting.txt"))
            {
                //udělá se stream reader a ten poté přečte celý soubor 
                using (StreamReader st1 = new StreamReader(jsemCesta + "\\" + "setting.txt"))
                {
                    string line;
                    string[] parts = new string[5];
                    int pomoc = 0;
                    while ((line = st1.ReadLine()) != null)
                    {
                        //splitne a druhá čast se uloži
                        parts[pomoc] = line.Split(new string[] { ":*" }, System.StringSplitOptions.None)[1];
                        pomoc++;
                    }
                    folders.sourseFolder = parts[0].Trim();
                    folders.destinacionFolder = parts[1].Trim();
                    folders.timeToSynch = int.Parse(parts[2].Trim());
                    folders.jmenoInstance= parts[3].Trim();
                    folders.bootOnStartup = bool.Parse(parts[4].Trim());

                }
                if (!string.IsNullOrEmpty(folders.sourseFolder))
                {
                    MainWindow.Instance.sourcePath.Content = $"Cesta: : {folders.sourseFolder}";
                }
                if (!string.IsNullOrEmpty(folders.destinacionFolder))
                {
                    MainWindow.Instance.desPath.Content = $"Cesta: : {folders.destinacionFolder}";
                }
               
                
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

