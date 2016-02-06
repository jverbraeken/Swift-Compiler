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
    public class Table
    {
        private Dictionary<string, Symbol> dictionary;
        private Table reference; //For scoping a table references its parent.

        /// <summary>
        /// Creates a new Symbol Table
        /// </summary>
        public Table(Table reference)
        {
            dictionary = new Dictionary<string, Symbol>();
            this.reference = reference;
        }

        /// <summary>
        /// Inserts a new symbol in the symbol table
        /// </summary>
        public void insert(Symbol symbol)
        {
            dictionary.Add(symbol.GetName(), symbol);
        }

        public Symbol lookup(string name)
        {
            Symbol value;
            if (dictionary.TryGetValue(name, out value))
                return value;
            else
                return null;
        }

        /// <summary>
        /// Get the "parent" of the table
        /// </summary>
        /// <returns></returns>
        public Table GetReference()
        {
            return reference;
        }
    }
}
