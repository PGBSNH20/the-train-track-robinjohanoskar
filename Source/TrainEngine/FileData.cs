using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TrainEngine
{
    // Note: split into a separate class for each data file type? ("train.txt", "passangers.txt" ...etc).
    public class FileData
    {
        public string FileName;
        public List<string[]> FileLines { get; } = new List<string[]>();

        public FileData(string filePath, char separator)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] columns = line.Split(separator).Select(a => a.Trim()).ToArray();
                FileLines.Add(columns);
            }
        }
    }
}
