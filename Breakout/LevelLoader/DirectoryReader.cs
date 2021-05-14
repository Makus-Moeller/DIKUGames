using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout {
    /// <summary>
    /// Reads available files from directory
    /// </summary>
    public class DirectoryReader {

        /// <summary>
        /// Reads files in a path. and puts them in a list
        /// </summary>
        /// <param name="path">Location of directory</param>
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