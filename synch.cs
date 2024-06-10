using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace folderSynch
{
    enum op
    {
        existuji,
        smazat
    }



    class filesInfo
    {
        public string fileName { get; set; }
        public DateTime date { get; set; }
        public op operace { get; set; }
        public string copyDes { get; set; }
        public bool copy { get; set; }

    }
    static class synch
    {
        public static List<filesInfo> sourceInfo = new List<filesInfo>();
        public static List<filesInfo> desInfo = new List<filesInfo>();
        static public void checkFiles(string folder, bool source, ref List<filesInfo> files)
        {
            
            foreach (var item in Directory.GetFiles(folder))
            {

                if (source)
                {
                    files.Add(new filesInfo
                    {
                        fileName = Path.GetFileName(item),
                        date = File.GetLastWriteTime(item),
                        operace = 0
                    });
                    continue;
                }

                if (!sourceInfo.Any(x => x.fileName == Path.GetFileName(item)))
                {
                    files.Add(new filesInfo
                    {
                        fileName = Path.GetFileName(item),
                        date = File.GetLastWriteTime(item),
                        operace = op.smazat
                    });
                    continue;
                }

                files.Add(new filesInfo
                {
                    fileName = Path.GetFileName(item),
                    date = File.GetLastWriteTime(item),
                    operace = op.existuji
                });

            }
                
                //Thread.Sleep(10);

            
        }

        static public void copyFiles()
        {
            checkExistance();

            foreach (filesInfo item in sourceInfo)
            {
                if (item.copy)
                {
                    File.Copy(folders.sourseFolder + "\\" + item.fileName, item.copyDes + "\\" + item.fileName);
                }

            }
            foreach (filesInfo item in desInfo)
            {
                if (item.operace == op.smazat)
                {
                    File.Delete(folders.destinacionFolder + "\\" + item.fileName);
                }
            }
            sourceInfo.Clear();
            desInfo.Clear();


        }





        static void checkExistance()
        {
            if (desInfo.Count == 0)
            {
                sourceInfo.ForEach(item => { item.copy = true; item.copyDes = folders.destinacionFolder; });
                return;

            }
            foreach (var (item, index) in sourceInfo.Select((value, i) => (value, i)))
            {
                if (!desInfo.Any(a => a.fileName == item.fileName))
                {
                    sourceInfo[index].copy = true;
                    sourceInfo[index].copyDes = folders.destinacionFolder;

                }
            }



        }




    }
}
