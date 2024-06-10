using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;




namespace folderSynch
{
    static class folders
    {
        public static string sourseFolder = null;
        public static string destinacionFolder = null;
        


    }
    static class folderWork
    {
        


        static public bool selecFolder(ref string s) 
        {
            
            var op = new OpenFolderDialog();

            if( op.ShowDialog().Value == true )
            {
                s = op.FolderName;
                return false;
            }
            return true;
        
        }
    }
}
