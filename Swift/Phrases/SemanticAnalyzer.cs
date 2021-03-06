﻿using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Phrases;
using Swift.Symbols;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class SemanticAnalyzer : VisitorAdapter
    {
        List<Table> tables;
        TypeVisitor typeVisitor = new TypeVisitor();
        ScopeAssignVisitor scopeAssignVisitor = new ScopeAssignVisitor();

        public List<Table> CheckSemantics(Base ast)
        {
            tables = new List<Table>();
            Table swiftTable = CreateBuiltinSymbols();
            tables.Add(swiftTable);
            tables.Add(new Table(tables[0], Global.Scope.MainScope, "Main"));
            ScopeAssignVisitor scopeAssignVisitor = new ScopeAssignVisitor();
            scopeAssignVisitor.Scope = tables[1];

            // First we assign a scope to all nodes
            foreach (ASTNode node in ast.Children)
            {
                node.accept(scopeAssignVisitor);
            }

            // Then we generate the symbol tables and check the semantics
            foreach (ASTNode node in ast.Children)
                node.accept(this);

            return tables;
        }

        public static Table CreateBuiltinSymbols()
        {
            Table swiftTable = new Table(null, Global.Scope.BuiltinScope, "Builtin");
            // print(string)
            BuiltinFunctionSymbol printSymbol = new BuiltinFunctionSymbol("print");
            List<ParameterDeclaration> lst = new List<ParameterDeclaration>();
            ParameterDeclaration par = new ParameterDeclaration(null, new StringType(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(int8)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new Int8Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(int16)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new Int16Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(int32)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new Int32Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(int64)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new Int64Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(uint8)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new UInt8Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(uint16)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new UInt16Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(uint32)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new UInt32Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            // print(uint64)
            printSymbol = new BuiltinFunctionSymbol("print");
            lst = new List<ParameterDeclaration>();
            par = new ParameterDeclaration(null, new UInt64Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            printSymbol.OccupiedParamRegisters = 1;
            swiftTable.Insert(printSymbol);

            return swiftTable;
        }

        /*private void CheckFunction(FunctionCall node)
        {
            Node = node;
            name = node.Name.Name;
            Table scope = node.Scope;
            Symbol reference = scope.Lookup(name, node.Args);
            if (reference != null) //The identifier exists in the current scope
                reference.accept(this);
            else if (scope == null)
                Swift.error("The function you called could not be found, line " + node.Context.Line.ToString() + ", column " + node.Context.Pos.ToString(), 1);
        }*/













        /*public override void visit(FunctionSymbol n)
        {
            if (paramTypes.Count == n.Parameters.Count)
            {
                for (int i = 0; i < paramTypes.Count; i++)
                {
                    if (!(paramTypes[i] == n.Parameters[i].Type))
                        Swift.error("The type of the parameter you supplied when calling \"" + name + "\" at the line " + Node.Context.Line.ToString() + ", column " + Node.Context.Pos.ToString() + " is not the same type as required by the function", 1);
                }
            }
            else
            {
                Swift.error("The number of parameters you supplied when calling \"" + name + "\" at the line " + Node.Context.Line.ToString() + ", column " + Node.Context.Pos.ToString() + " does not match the required number of parameters as defined in the function", 1);
            }
        }*/

        public override void visit(Assignment n)
        {
            string name = n.LHS.Name;
            Table scope = n.Scope;
            while (scope != null)
            {
                Symbol reference = scope.Lookup(name);
                if (reference != null)
                {
                    if (reference is VariableSymbol) {
                        if (n.RHS is IdentifierExp)
                            if (n.Scope.Lookup(((IdentifierExp) n.RHS).ID.Name) is ConstantSymbol)
                                n.RHS = ((ConstantSymbol)n.Scope.Lookup(((IdentifierExp)n.RHS).ID.Name)).Value;
                        ASTType type = n.RHS.accept(new TypeVisitor());
                        if (((VariableSymbol)reference).Type.GetType() != type.GetType())
                            Swift.error(new IncompatibleTypesException(n.Context));
                    }
                    else if (reference is ConstantSymbol) {
                        Swift.error(new ConstantReassignmentException(n.Context));
                    }
                    reference.SetReferenced();
                    break;
                }
                else
                    Swift.error(new AssignmentUnknownVariable(n.Context));
                scope = scope.GetReference();
            }
        }

        public override void visit(ConstDeclaration n)
        {
            Table scope = n.Scope;
            if (n.Type == null)
                n.Type = n.accept(new TypeVisitor());
            ConstantSymbol sym = new ConstantSymbol(n.Name.Name, n.Type, n.RHS);
            scope.Insert(sym);
        }

        public override void visit(FunctionCallExp n)
        {
            n.Scope.Lookup(n.Name.Name, n.Args).SetReferenced();
        }

        public override void visit(VarDeclaration n)
        {
            Table scope = n.Scope;
            VariableSymbol sym = new VariableSymbol(n.Name.Name, n.Type);
            if (n.Type == null)
                sym.Type = n.TypeByAssignment.RHS.accept(new TypeVisitor());
            scope.Insert(sym);
        }


        ////        EXPRESSIONS


        /*public override void visit(StringLiteral n)
        {
            base.visit(n);
        }*/

        [Serializable()]
        public class IncompatibleTypesException : SwiftException
        {
            public IncompatibleTypesException(ILineContext context, string message = "the types of the left-hand and right-hand side of the assignment don't match") : base(context, message) { }
        }

        [Serializable()]
        public class ConstantReassignmentException : SwiftException
        {
            public ConstantReassignmentException(ILineContext context, string message = "the value of constants cannot be changed") : base(context, message) { }
        }

        [Serializable()]
        public class AssignmentUnknownVariable : SwiftException
        {
            public AssignmentUnknownVariable(ILineContext context, string message = "assignment to unknown variable") : base(context, message) { }
        }
    }
}
