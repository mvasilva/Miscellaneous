using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Miscellaneous
{
    public class FileManager
    {

        public static int ClearFilesByExtension(string folderPath, List<string> extension)
        {

            List<string[]> filesList = new List<string[]>();


            extension.ForEach(e => {

                filesList.Add(Directory.GetFiles(folderPath, string.Format("*.{0}", e)));
            
            });

            int filesDeleted = 0;

            foreach (string[] files in filesList)
            {

                Parallel.ForEach(files, filePath =>
                {

                    FileInfo file = new FileInfo(filePath);

                    TimeSpan diff = DateTime.Now.Subtract(file.LastWriteTime);


                    if (diff.TotalHours >= 1 && file.Exists)
                    {
                        try
                        {
                            file.Delete();

                            filesDeleted++;
                        }
                        catch (Exception)
                        {


                        }

                    }


                });

            }


            string[] directorys = Directory.GetDirectories(folderPath);

            if (directorys.Length > 0)
            {

                Parallel.ForEach(directorys, directoryPath => { filesDeleted += ClearFilesByExtension(directoryPath, extension); });

            }


            return filesDeleted;

        }
    }
}
