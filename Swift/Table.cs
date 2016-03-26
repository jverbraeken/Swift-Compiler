using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Phrases;
using Swift.Symbols;
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
        private Dictionary<Tuple<string, List<ASTType>>, Symbol> dictionary;
        private Table reference; //For scoping a table references its parent.
        private SymbolToTableVisitor symbolToTableVisitor;

        /// <summary>
        /// Creates a new Symbol Table
        /// </summary>
        public Table(Table reference)
        {
            dictionary = new Dictionary<Tuple<string, List<ASTType>>, Symbol>(new TableFunctionComparator());
            this.reference = reference;
            symbolToTableVisitor = new SymbolToTableVisitor(dictionary);
        }

        public void Insert(BuiltinFunctionSymbol n)
        {
            n.StackLocation = GetStackSize() + 1;
            List<ASTType> param2 = new List<ASTType>();
            foreach (ParameterDeclaration decl in n.Parameters)
                param2.Add(decl.Type);
            dictionary.Add(Tuple.Create(n.Name, param2), n);
        }

        public void Insert(ConstantSymbol n)
        {
            n.StackLocation = GetStackSize() + 1;
            dictionary.Add(Tuple.Create<string, List<ASTType>>(n.Name, null), n);
        }

        public void Insert(FunctionSymbol n)
        {
            n.StackLocation = GetStackSize() + 1;
            List<ASTType> param2 = new List<ASTType>();
            foreach (ParameterDeclaration decl in n.Parameters)
                param2.Add(decl.Type);
            dictionary.Add(Tuple.Create(n.Name, param2), n);
        }

        public void Insert(VariableSymbol n)
        {
            n.StackLocation = GetStackSize() + 1;
            dictionary.Add(Tuple.Create<string, List<ASTType>>(n.Name, null), n);
        }

        /// <summary>
        /// Returns the symbol assigned to a name in the symbol table or the first occurence in its parents.
        /// Note: please overload this function when you want to retrieve a function symbol
        /// </summary>
        /// <param name="name">The name of the symbol</param>
        /// <returns></returns>
        public Symbol Lookup(string name)
        {
            Symbol value;
            if (dictionary.TryGetValue(Tuple.Create<string, List<ASTType>>(name, null), out value))
                return value;
            else {
                if (reference == null)
                    return null;
                else
                    return reference.Lookup(name);
            }
        }

        /// <summary>
        /// Returns the function symbol assigned to a name in the symbol table.
        /// Note: please skip param if the symbol you want to retrieve isn't a function symbol
        /// </summary>
        /// <param name="name">The name of the symbol</param>
        /// <param name="param">A list of ParameterCalls of the function</param>
        /// <returns></returns>
        public Symbol Lookup(string name, List<ParameterCall> param)
        {
            Symbol value;
            List<ASTType> param2 = new List<ASTType>();
            foreach (ParameterCall call in param)
                param2.Add(call.Value.accept(new TypeVisitor()));
            if (dictionary.TryGetValue(Tuple.Create(name, param2), out value))
                return value;
            else {
                if (reference == null)
                    return null;
                else
                    return reference.Lookup(name, param2);
            }
        }

        /// <summary>
        /// Returns the function symbol assigned to a name in the symbol table.
        /// Note: this function is called by Lookup(string, List<ParameterCall>), please use instead of this function that one directly
        /// </summary>
        /// <param name="name">The name of the symbol</param>
        /// <param name="param">A list of ASTTypes of the function</param>
        /// <returns></returns>
        public Symbol Lookup(string name, List<ASTType> param)
        {
            Symbol value;
            if (dictionary.TryGetValue(Tuple.Create(name, param), out value))
                return dictionary[new Tuple<string, List<ASTType>>(name, param)];
            else {
                if (reference == null)
                    return null;
                else
                    return reference.Lookup(name, param);
            }
        }

        /// <summary>
        /// Get the "parent" of the table
        /// </summary>
        /// <returns></returns>
        public Table GetReference()
        {
            return reference;
        }

        /// <summary>
        /// Returns the size of the symbol table, or in other words, the amount of variables in this scope
        /// </summary>
        /// <returns></returns>
        public int GetStackSize()
        {
            return dictionary.Count;
        }
    }
}
