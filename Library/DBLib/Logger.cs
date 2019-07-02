using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBLib
{
    /// <summary>
    /// Writes the error messages to log file
    /// </summary>
    public static class Logger
    {
        public static void Log(string message, string serverPath)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(serverPath, "libLog.txt"), true))
            {
                outputFile.WriteLine(message);
            }
        }
        public static void Log(List<string> messages, string serverPath)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(serverPath, "libLog.txt"), true))
            {
                foreach(var massage in messages)
                {
                    outputFile.WriteLine(massage);
                }
            }
        }
    }
}
