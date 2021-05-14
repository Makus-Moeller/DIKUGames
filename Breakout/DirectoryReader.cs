using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout {
    public class DirectoryReader {

        public List<String> Readfiles(string path) {
            List<String> filenames = new List<String>();
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles("*level4*");
            foreach (FileInfo filename in files) {
                filenames.Add(filename.Name);
            }
            return  filenames;
        }
    }
}