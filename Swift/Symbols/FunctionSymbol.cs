using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Symbols
{
    public class FunctionSymbol : Symbol
    {
        public List<ParameterDeclaration> Parameters { get; set; }
        public TupleType ReturnValue { get; set; }
        public bool EntireReturnOptional { get; set; }

        public FunctionSymbol(string name) : base(name)
        {
            Parameters = new List<ParameterDeclaration>();
            ReturnValue = new TupleType();
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override ASTType accept(TypeVisitor v)
        {
            return v.visit(this);
        }
    }
}
