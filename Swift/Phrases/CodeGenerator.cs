using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class CodeGenerator
    {
        public static System.IO.StreamWriter file;
        public static void MakeAssembly(string dest, List<string> intercode)
        {
            using (file = new System.IO.StreamWriter(dest))
            {
            }
        }

        /// <summary>
        /// Write to File
        /// Writes a line of text to the file with indentation level 1.
        /// </summary>
        /// <param name="l">A single line of text to be written to the file</param>
        public static void w(string l)
        {
            file.WriteLine("    " + l);
        }

        /// <summary>
        /// Write to File No Identation
        /// Writes a line of text to the file with indentation level 0.
        /// </summary>
        /// <param name="l">A single line of text to be written to the file</param>
        public static void wn(string l)
        {
            file.WriteLine(l);
        }
    }
}
