using Swift.AssTargets;
using Swift.AST_Nodes;
using Swift.Instructions;
using Swift.Instructions.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class IntermediateCodeGenerator : VisitorAdapter
    {
        //List<string> result;
        int teller;
        SymbolVisitor symbolVisitor;
        List<Table> tables;
        Stack<Instruction> postfixStack;
        private Register regRAX = new Register(Global.Registers.RAX);
        private Register regRDX = new Register(Global.Registers.RDX);
        int scope = 1;

        public List<string> GenerateCode(string source, string dest, ASTNode ast, List<Table> tables)
        {
            this.tables = tables;
            //result = new List<string>();

            Add(new File(source));

            //w("file:\"" + source + "\"");
            //w("section:constants");

            //teller = 0;
            //SearchConstants(ast);

            //w("section:var_unitialised");

            //teller = 0;
            //SearchUnitialised(ast);

            //w("section:var_initialised");

            //teller = 0;
            //SearchInitialised(ast);

            Add(new SectionCode());
            Add(new MakeGlobal("__main"));
            Add(new Label("main"));
            Add(new Push(new Register(Global.Registers.BASEPOINTER)));
            Add(new Move(new Register(Global.Registers.STACKPOINTER), new Register(Global.Registers.BASEPOINTER)));
            Add(new Sub(new Constant(tables[1].StackSize), new Register(Global.Registers.STACKPOINTER)));
            Add(new Call("__main"));
            /*w("section:code");
            w("define_main_method");
            w("set_base_pointer");
            w("reserve_stack");
            w("call:%SETUP_C%");*/

            ast.accept(this);

            Add(new Nope());
            Add(new Move(new Register(Global.Registers.BASEPOINTER), new Register(Global.Registers.STACKPOINTER)));
            Add(new Pop(new Register(Global.Registers.BASEPOINTER)));
            Add(new Ret());
            Add(new Comment("Yontu: (x86_64-posix-seh-rev0, Built by Joost Verbraeken) BETA"));
            /*w("get_base_pointer");
            w("return");
            w("comment:\"Yontu: (Joost Verbraeken) BETA\"");*/
            return result;
        }

        private void SearchConstants(ASTNode ast)
        {
            foreach (ASTNode node in ast.GetChildren())
            {
                switch (node.GetType())
                {
                    //case Global.ASTType.STRING:
                    //    w("define_constant_string:" + teller.ToString() + ":" + node.GetName());
                    //    node.SetAssemblyLocation(teller);
                    //    teller++; break;
                }
                SearchConstants(node);
            }
        }

        private void SearchUnitialised(ASTNode ast)
        {

        }

        private void SearchInitialised(ASTNode ast)
        {
            bool waitForAssignment = false;
            foreach (ASTNode node in ast.GetChildren())
            {
                if (waitForAssignment)
                {
                    if (node.GetType() == Global.ASTType.ASSIGNMENT)
                    {
                        Symbol symbol = node.GetScope().lookup(node.GetExpression1().accept(this));
                        switch (symbol.GetType())
                        {

                        }
                    }
                    waitForAssignment = false;
                }
                switch (node.GetType())
                {
                    case Global.ASTType.VAR_DECLARATION:
                        if (node.GetScope().GetReference() == null)
                        {
                            waitForAssignment = true;
                        }
                            break;
                }
                if (!waitForAssignment)
                    SearchInitialised(node);
            }
        }

        private void ExecuteFunctionCall(ASTNode node)
        {
            string name = node.GetName();
            List<ASTNode> args = node.GetChildren();
            Table scope = node.GetScope();
            while (scope != null)
            {
                Symbol reference = scope.lookup(name);
                if (reference != null) //The identifier exists in the current scope
                {
                    if (reference.GetType() == Global.DataType.BUILTIN_FUNC) //The function is builtin
                    {
                        switch (reference.GetName())
                        {
                            case "print": ExecutePrint(node); break;
                        }
                    }
                }
                scope = scope.GetReference();
            }
        }

        private void ExecutePrint(ASTNode node)
        {
            if (node.GetChildren()[0].GetType() == Global.ASTType.STRING)
                w("call:print,constant," + node.GetChildren()[0].AssemblyLocation);
        }

        private void Add(Instruction item)
        {
            postfixStack.Push(item);
        }

















        public override void visit(Assignment n)
        {
            int stackLocation = tables[scope].lookup(n.LHS.Name).StackLocation;
            n.RHS.accept(this);
            Add(new Pop(regRAX));
            Add(new Move(regRAX, new RegisterOffset(Global.Registers.STACKPOINTER, stackLocation)));
        }

        public override void visit(Base n)
        {
            foreach (ASTNode node in n.Children)
                n.accept(this);
        }

        public override void visit(Identifier n)
        {
            int stackLocation = tables[scope].lookup(n.Name).StackLocation;
            Add(new Move(new RegisterOffset(Global.Registers.STACKPOINTER, stackLocation), regRAX));
            Add(new Push(regRAX));
        }

        public override void visit(IntegerLiteral n)
        {
            Add(new Push(new Constant(n.f0)));
        }

        public override void visit(PlusExp n)
        {
            n.e1.accept(this);
            n.e2.accept(this);
            Add(new Pop(regRDX));
            Add(new Pop(regRAX));
            Add(new Add());
            Add(new Push(regRAX));
        }
    }
}
