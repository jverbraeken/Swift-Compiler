using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Phrases
{
    public class ScopeAssignVisitor : VisitorAdapter
    {
        public Table Scope { get; set; }

        public override void visit(Assignment n)
        {
            n.Scope = Scope;
            n.LHS.accept(this);
            n.RHS.accept(this);
        }

        public override void visit(ConstDeclaration n)
        {
            n.Scope = Scope;
            n.RHS.accept(this);
        }

        public override void visit(FunctionCallExp n)
        {
            n.Scope = Scope;
            foreach (ParameterCall call in n.Args)
                call.Value.accept(this);
        }

        public override void visit(IdentifierExp n)
        {
            n.Scope = Scope;
        }

        public override void visit(PlusExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(VarDeclaration n)
        {
            n.Scope = Scope;
        }
    }
}
