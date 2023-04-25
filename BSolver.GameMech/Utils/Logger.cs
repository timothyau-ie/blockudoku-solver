using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Utils
{
    public class Logger
    {
        private string _filePath;

        public Logger(string filePath)
        {
            _filePath = filePath;
        }
        public void WriteLog(string log)
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "");
            }
            File.AppendAllText(_filePath, string.Format("{0}: {1}{2}", DateTime.Now, log, Environment.NewLine));
        }
    }
}
