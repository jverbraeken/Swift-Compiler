using Swift.AssTargets;
using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Instructions;
using Swift.Instructions.Directives;
using Swift.Phrases;
using Swift.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class IntermediateCodeGenerator : VisitorAdapter
    {
        public static int AsciiToInt(string ascii)
        {
            int result = 0;
            for (int i = ascii.Count()-1; i >= 0; i--)
            {
                result = 256 * result + (ascii[i]);
            }
            return result;
        }

        //List<string> result;
        private List<Table> tables;
        private List<Instruction> postfixList = new List<Instruction>();
        private Register regRAX = new Register(Global.Registers.ACCUMULATOR);
        private Register regRDX = new Register(Global.Registers.DATA);
        private Register regBase = new Register(Global.Registers.STACKBASEPOINTER);
        private Register regStack = new Register(Global.Registers.STACKPOINTER);
        private int scope = 1;
        /// <summary>
        /// The key is the text, the value is the label
        /// </summary>
        private Dictionary<string, string> stringTable = new Dictionary<string, string>();
        private TypeVisitor typeVisitor = new TypeVisitor();
        private string tmpStr;
        private Stack<AssTarget> stack = new Stack<AssTarget>();
        /// <summary>
        /// Indicates the nth parameter of a functioncall
        /// </summary>
        private int parameterNum;
            
        public List<Module> GenerateCode(string source, string dest, Base ast, List<Table> tables)
        {
            //tables[1].Compress(); // Doesn't work in a situation like a=5; b=a; print(b)

            this.tables = tables;
            //result = new List<string>();

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
            Add(new MakeGlobal("main"));
            Add(new Label("main"));
            Add(new Push(new Register(Global.Registers.STACKBASEPOINTER)));
            Add(new Move(new Register(Global.Registers.STACKPOINTER), regBase));
            Add(new Sub(new ByteConstant(4), regStack));
            if (tables[1].GetStackSize() > 0)
                Add(new Sub(new ByteConstant(tables[1].GetStackSize()), regStack));
            Add(new Call("__main"));
            /*w("section:code");
            w("define_main_method");
            w("set_base_pointer");
            w("reserve_stack");
            w("call:%SETUP_C%");*/

            stack.Clear();
            ast.accept(this);

            Add(new Nope());
            Add(new Move(new Register(Global.Registers.STACKBASEPOINTER), new Register(Global.Registers.STACKPOINTER)));
            Add(new Pop(new Register(Global.Registers.STACKBASEPOINTER)));
            Add(new Ret());
            /*w("get_base_pointer");
            w("return");
            w("comment:\"Yontu: (Joost Verbraeken) BETA\"");*/
            return new List<Module>() { new Module(postfixList, stringTable) };
        }

        /*private void SearchConstants(ASTNode ast)
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
        }*/

        private void Add(Instruction item)
        {
            postfixList.Add(item);
        }

















        public override void visit(Assignment n)
        {
            try {
                int stackLocation = tables[scope].Lookup(n.LHS.Name).StackLocation;
                n.RHS.accept(this);
                //Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, extraStackSize++), regRAX));
                Add(new Move(stack.Pop(), new RegisterOffset(Global.Registers.STACKBASEPOINTER, -stackLocation)));
            } catch (NoSuchKeyException e)
            {
                // Do nothing, emit the declaration of the constant, normally because the constant was unreferenced and thus unused
            }
        }

        public override void visit(Base n)
        {
            foreach (ASTNode node in n.Children)
                node.accept(this);
        }

        public override void visit(ConstDeclaration n)
        {
            try {
                int stackLocation = tables[scope].Lookup(n.Name.Name).StackLocation;
                n.RHS.accept(this);
                //Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, extraStackSize++), regRAX));
                Add(new Move(stack.Pop(), new RegisterOffset(Global.Registers.STACKBASEPOINTER, -stackLocation)));
            } catch (NoSuchKeyException e)
            {
                // Do nothing, emit the declaration of the constant, normally because the constant was unreferenced and thus unused
            }
        }

        public override void visit(FunctionCallExp n)
        {
            parameterNum = 0;
            foreach (ParameterCall call in n.Args)
                call.Value.accept(this);
            int count = n.Args.Count;
            bool builtin = (tables[0].Lookup(n.Name.Name, n.Args) is BuiltinFunctionSymbol);
            int occupiedParamRegisters = 0;
            if (builtin)
                occupiedParamRegisters = ((BuiltinFunctionSymbol) tables[scope].Lookup(n.Name.Name, n.Args)).OccupiedParamRegisters;
            if (!(n.Args[0].Value is StringLiteral))
                for (int i = 0; i < count; i++)
                    Add(new Move(stack.Pop(), new ParamRegister(count - 1 + occupiedParamRegisters - i)));
            if (builtin)
                executeBuiltinFunction(n);
        }

        public override void visit(Identifier n)
        {
            int stackLocation = tables[scope].Lookup(n.Name).StackLocation;
            stack.Push(new RegisterOffset(Global.Registers.STACKBASEPOINTER, -stackLocation));
        }

        public override void visit(IdentifierExp n)
        {
            n.ID.accept(this);
        }

        public override void visit(Int64Literal n)
        {
            stack.Push(new IntegerConstant(int.Parse(n.Value)));
            //Add(new Move(new IntegerConstant(int.Parse(n.Value)), regRAX));
            //Add(new Move(new IntegerConstant(int.Parse(n.Value)), new RegisterOffset(Global.Registers.STACKBASEPOINTER, --extraStackSize)));
        }

        //      EXPRESSIONS
        
        public override void visit(MinusExp n)
        {
            n.e1.accept(this);
            n.e2.accept(this);
            Add(new Move(stack.Pop(), regRDX));
            Add(new Move(stack.Pop(), regRAX));
            Add(new Sub(regRDX, regRAX));
            stack.Push(regRAX);
        }

        public override void visit(MultiplicationExp n)
        {
            n.e1.accept(this);
            n.e2.accept(this);
            Add(new Move(stack.Pop(), regRDX));
            Add(new Move(stack.Pop(), regRAX));
            Add(new Mult(regRDX, regRAX));
            stack.Push(regRAX);
        }

        public override void visit(PlusExp n)
        {
            n.e1.accept(this);
            n.e2.accept(this);
            Add(new Move(stack.Pop(), regRDX));
            Add(new Move(stack.Pop(), regRAX));
            Add(new Add(regRDX, regRAX));
            stack.Push(regRAX);
        }

        public override void visit(StringLiteral n)
        {
            if (!stringTable.ContainsKey(n.Text))
                stringTable.Add(n.Text, ".LC" + stringTable.Count);
            Add(new StringAsParameter(stringTable[n.Text], parameterNum++));
        }

        public override void visit(VarDeclaration n)
        {
            return;
        }





        private void executeBuiltinFunction(FunctionCallExp n)
        {
            switch (n.Name.Name)
            {
                case "print":
                    if (n.Args[0].Value.accept(typeVisitor) is Int8Type)
                    {
                        if (!stringTable.ContainsKey("%o"))
                            stringTable.Add("%o", ".LC" + stringTable.Count);
                        Add(new Lea(new RegisterOffset(Global.Registers.INSTRUCTIONPOINTER, stringTable["%o"]), new ParamRegister(0)));
                    }
                    if (n.Args[0].Value.accept(typeVisitor) is Int64Type)
                    {
                        if (!stringTable.ContainsKey("%d"))
                            stringTable.Add("%d", ".LC" + stringTable.Count);
                        Add(new Lea(new RegisterOffset(Global.Registers.INSTRUCTIONPOINTER, stringTable["%d"]), new ParamRegister(0)));
                    }
                    else if (n.Args[0].Value.accept(typeVisitor) is StringType)
                    {
                        /*if (!stringTable.ContainsKey("%s"))
                            stringTable.Add("%s", ".LC" + stringTable.Count);
                        n.Args[0].Value.accept(this);
                        Add(new Lea(new RegisterOffset(Global.Registers.INSTRUCTIONPOINTER, stringTable["%s"]), new ParamRegister(0)));*/
                    }
                    Add(new Call("printf")); break;
            }
        }
    }
}
