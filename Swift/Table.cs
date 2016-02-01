using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    /// <summary>
    /// A symbol table in which all symbols (variables, parameters, functions) of a certain scope are stored
    /// </summary>
    class Table
    {
        Hashtable hashtable;
        Table reference; //For scoping a table references its parent.

        /// <summary>
        /// Creates a new Symbol Table
        /// </summary>
        public Table()
        {
            hashtable = new Hashtable();
        }

        /// <summary>
        /// Inserts a new symbol in the symbol table
        /// </summary>
        public void insert(Symbol symbol)
        {
            Object[] data = new Object[3];
            data[0] = symbol.type;
            data[1] =
        public object value; //for constants
        public int length; //for arrays
        public List<>
            hashtable.Add(symbol.name, data);
        }
    }
}
