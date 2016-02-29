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
        public static string MakeAssembly(string dest, List<string> intercode)
        {
            string file_origin = "";
            using (file = new System.IO.StreamWriter(dest))
            {
                int pos;
                string opcode;
                string code = "";
                foreach(string str in intercode)
                {
                    pos = str.IndexOf(':');
                    if (pos == -1)
                        opcode = str;
                    else
                    {
                        opcode = str.Substring(0, pos);
                        code = str.Substring(pos + 1);
                    }
                    switch (opcode)
                    {
                        case "call":
                            if (code[0] == '%')
                            {
                                if (code == "%SETUP_C%")
                                    w("call\t___main");
                                break;
                            }
                            pos = code.IndexOf(',');
                            switch (code.Substring(0, pos))
                            {
                                case "print":
                                    code = code.Substring(pos + 1);
                                    pos = code.IndexOf(',');
                                    if (code.Substring(0, pos) == "constant")
                                        w("movl\t$A" + code.Substring(pos + 1) + ", (%esp)");
                                    w("call\t_puts");
                                    break;
                            }
                            break;
                        case "comment":
                            w(".ident\t" + code);
                            break;
                        case "define_constant_string":
                            pos = code.IndexOf(":");
                            wn("A" + code.Substring(0, pos) + ":");
                            code = code.Substring(pos + 1);
                            w(".asciz " + code);
                            break;
                        case "define_main_method":
                            w(".globl\t_main");
                            w(".def\t_main;\t.scl\t2;\t.type\t32;\t\t.endef");
                            wn("_main:");
                            break;
                        case "file":
                            w(".file\t" + code);
                            file_origin = code;
                            break;
                        case "get_base_pointer":
                            w("leave");
                            break;
                        case "reserve_stack":
                            w("andl\t$-16, %esp");
                            w("subl\t$16, %esp");
                            break;
                        case "return":
                            w("ret");
                            break;
                        case "section":
                            switch (code)
                            {
                                case "code": w(".text"); break;
                                case "constants": w(".section\t.rdata,\"dr\""); break;
                                case "var_initialised": w(".section\t.data"); break;
                                case "var_unitialised": w(".section\t.comm"); break;
                            }
                            break;
                        case "set_base_pointer":
                            w("pushl\t%ebp");
                            w("movl\t%esp, %ebp");
                            break;
                    }
                }
            }
            return "Compilation successful. The compilation of the source \"" + file_origin + "\" is contained in \"" + dest + "\"";
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
