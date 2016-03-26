using Swift.AST_Nodes;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class SyntaxAnalyzer
    {
        List<Token> tokens;
        List<LineContext> context;
        private ASTNode node;
        private Base astBase;

        public Base CheckSyntax(List<Token> tokens, List<LineContext> context)
        {
            this.tokens = tokens;
            this.context = context;
            astBase = new Base(context[0]);
            EatStatements();
            return astBase;
        }

        private void EatStatements()
        {
            int i;
            while (true)
            {
                i = 0;
                while (tokens[i].type != Global.DataType.ENDSTATEMENT)
                    i++;
                ASTNode tmp = EatStatement(tokens.GetRange(0, i), context.GetRange(0, i));
                if (tmp != null)
                    astBase.Children.Add(tmp);
                tokens.RemoveRange(0, i + 1);
                if (tokens.Count == 0)
                    break;
            }
        }

        /// <summary>
        /// Parses a statement
        /// </summary>
        /// <param name="token"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private ASTNode EatStatement(List<Token> tokensIn, List<LineContext> contextIn)
        {
            if (tokensIn.Count == 0)
                return null;
            switch (tokensIn[0].type)
            {
                case Global.DataType.IDENTIFIER:
                    if (tokensIn[1].type == Global.DataType.OPEN_ROUND_BRACKET)
                    {
                        return EatFunctionCall(tokensIn, contextIn);
                    }
                    else {
                        return EatAssignment(tokensIn, contextIn);
                    }
                case Global.DataType.LET:
                    return EatDeclaration(tokensIn, contextIn);
                case Global.DataType.STRING:
                    EatExpression(tokensIn, contextIn);
                    return node;
                case Global.DataType.VAR:
                    return EatDeclaration(tokensIn, contextIn);
                default:
                    Swift.error("Syntax error: \"" + tokensIn[0].value + "\" at line " + contextIn[0].GetLine().ToString() + ", colomn " + contextIn[0].GetPos().ToString() + " could not be identified", 1);
                    return null;
            }
        }

        private ASTNode EatFunctionCall(List<Token> tokensIn, List<LineContext> contextIn)
        {
            List<Token> tmpTokens;
            List<LineContext> tmpContext;
            tmpTokens = tokensIn;
            tmpContext = contextIn;

            int i = 2;
            Token token = tmpTokens[i];
            List<ParameterCall> args = new List<ParameterCall>();
            while (token.type != Global.DataType.CLOSE_ROUND_BRACKET)
            {
                List<Token> arg = new List<Token>();
                List<LineContext> argContext = new List<LineContext>();
                while (token.type != Global.DataType.COMMA && token.type != Global.DataType.CLOSE_ROUND_BRACKET)
                {
                    arg.Add(token);
                    argContext.Add(tmpContext[i]);
                    token = tmpTokens[++i];
                }
                args.Add(EatFunctionArgument(arg, argContext));
            }
            FunctionCallExp node = new FunctionCallExp(tmpContext[0]);
            node.Name = new Identifier(tmpTokens[0].value);
            node.Args.AddRange(args);
            return node;
        }

        private ParameterCall EatFunctionArgument(List<Token> tokensIn, List<LineContext> contextIn)
        {
            List<Token> tmpTokens;
            List<LineContext> tmpContext;
            tmpTokens = tokensIn;
            tmpContext = contextIn;

            ParameterCall node;
            if (tmpTokens.Count > 1 && tmpTokens[1].type == Global.DataType.COLON)
                node = new ParameterCall(EatExpression(tmpTokens.GetRange(2, tmpTokens.Count - 2), tmpContext.GetRange(2, tmpContext.Count - 2)), tmpTokens[0].value);
            else
                node = new ParameterCall(EatExpression(tmpTokens, tmpContext));
            return node;
        }

        /// <summary>
        /// Parses an expression
        /// </summary>
        /// <param name="tokens">A list of tokens containing only the whole expression</param>
        /// <param name="context">A list of LineContext corresponding to the tokens supplied</param>
        /// <returns></returns>
        /*private ASTNode EatExpression(List<Token> tokensIn, List<LineContext> contextIn)
        {
            int pos = 0;
            Global.DataType op;
            while (true)
            {
                if (i >= tokensIn.Count)
                    Swift.error("Operator expected at line " + contextIn[0].GetLine() + ".", 1);
                if (tokensIn[i].type == Global.DataType.OPERATOR)
                {
                    op = tokensIn[0].type;
                    break;
                }
            }
            Exp lhs = EatLHS(tokensIn.GetRange(0, pos), contextIn);
            Exp rhs = EatRHS(tokensIn.GetRange(pos + 1, tokensIn.Count - ))
            ASTNode  node = new ASTNode(Global.ASTType.BINARY_EXPRESSION, contextIn[0]);
            node.se
        }*/

        private Exp EatExpression(List<Token> tokensIn, List<LineContext> contextIn)
        {
            List<Token> tmpTokens;
            List<LineContext> tmpContext;
            tmpTokens = tokensIn;
            tmpContext = contextIn;
            if (tmpTokens[0].type == Global.DataType.OPEN_ROUND_BRACKET)
            {
                int level = 1;
                int i = 1;
                while (true)
                {
                    if (i >= tmpTokens.Count)
                        Swift.error("\")\" expected in the expression starting at line " + tmpContext[0].GetLine() + ".", 1);
                    if (tmpTokens[i].type == Global.DataType.OPEN_ROUND_BRACKET)
                        level++;
                    if (tmpTokens[i].type == Global.DataType.CLOSE_ROUND_BRACKET)
                        level--;
                    if (level == 0)
                        break;
                    i++;
                }
                if (i >= tmpTokens.Count - 1) //Remove the brackets that do nothing appararently
                    return EatExpression(tmpTokens.GetRange(1, tmpTokens.Count - 2), tmpContext.GetRange(1, tmpContext.Count - 2));
            }
            else
            {
                if (tmpTokens.Count > 1)
                {
                    if (tmpTokens[1].type == Global.DataType.OPERATOR)
                    {
                        switch (tmpTokens[1].value)
                        {
                            case "+": return new PlusExp(tmpContext[1], EatExpression(new List<Token>() { tmpTokens[0] }, new List<LineContext>() { tmpContext[0] }), EatExpression(tmpTokens.GetRange(2, tmpTokens.Count - 2), tmpContext.GetRange(2, tmpContext.Count - 2)));
                        }
                    }
                    else
                        Swift.error("Operator expected at line " + tmpContext[1].GetLine() + ", position " + tmpContext[1].GetPos() + ".", 1);
                }
                else {
                    switch (tmpTokens[0].type)
                    {
                        case Global.DataType.INT: return new IntegerLiteral(tmpContext[0], tmpTokens[0].value);
                        case Global.DataType.STRING: return new StringLiteral(tmpContext[0], tmpTokens[0].value);
                        case Global.DataType.IDENTIFIER: return new IdentifierExp(tmpContext[0], new Identifier(tmpTokens[0].value));
                        default: throw new UnknownTermException("The type of the term in the expression is not recognized: " + tmpTokens[0].value);
                    }
                }
            }
            return null;
        }

        private Assignment EatAssignment(List<Token> tokensIn, List<LineContext> contextIn)
        {
            if (!(tokensIn[1].type == Global.DataType.OPERATOR && tokensIn[1].value == "="))
                Swift.error("Assignment operator expected at line " + contextIn[0].GetLine() + ".", 1);
            Assignment node = new Assignment(contextIn[0]);
            Identifier lhs = new Identifier(tokensIn[0].value);
            Exp rhs = EatExpression(tokensIn.GetRange(2, tokensIn.Count - 2), contextIn.GetRange(2, tokensIn.Count - 2));
            node.LHS = lhs;
            node.RHS = rhs;
            return node;
        }

        /*private Exp EatLHS(List<Token> tokensIn = null, List<LineContext> contextIn = null)
        {
            int i = 0;
            while (true)
            {
                if (i >= tokensIn.Count)
                    Swift.error("")
                if (tokensIn[i].type == Global.DataType.OPERATOR)
                {

                }
            }
            switch (tokensIn[0].type) {
                case Global.DataType.
            }

        private Exp EatRHS(List<Token> tokensIn = null, List<LineContext> contextIn = null)
        {
            List<Token> tmpTokens;
            List<LineContext> tmpContext;
            if (tokensIn == null)
            {
                tmpTokens = tokens;
                tmpContext = context;
            }
            else
            {
                tmpTokens = tokensIn;
                tmpContext = contextIn;
            }
            if (tmpTokens[0].type == Global.DataType.OPEN_ROUND_BRACKET)
            {
                int level = 1;
                int i = 1;
                while (true)
                {
                    if (i >= tmpTokens.Count)
                        Swift.error("\")\" expected in the expression starting at line " + tmpContext[0].GetLine() + ".", 1);
                    if (tmpTokens[i].type == Global.DataType.OPEN_ROUND_BRACKET)
                        level++;
                    if (tmpTokens[i].type == Global.DataType.CLOSE_ROUND_BRACKET)
                        level--;
                    if (level == 0)
                        break;
                    i++;
                }
                if (i >= tmpTokens.Count - 1) //Remove the brackets that do nothing appararently
                    return EatRHS(tmpTokens.GetRange(1, tmpTokens.Count - 2), tmpContext.GetRange(1, tmpContext.Count - 2));
            }
            else
            {
                if (tmpTokens.Count > 1)
                {
                    if (tmpTokens[1].type == Global.DataType.OPERATOR)
                    {
                        switch (tmpTokens[1].value)
                        {
                            case "+": return new PlusExp(tmpContext[1], new IntegerLiteral(tmpContext[0], tmpTokens[0].value), EatRHS(tmpTokens.GetRange(2, tmpTokens.Count - 2), tmpContext.GetRange(2, tmpContext.Count - 2)));
                        }
                    }
                    else
                        Swift.error("Operator expected at line " + tmpContext[1].GetLine() + ", position " + tmpContext[i].GetPos() + ".");
                }
                else
                    return new IntegerLiteral(tmpContext[0], tmpTokens[0].value);
            }
            return null;
        }*/

        /// <summary>
        /// Parses a declaration (eg, var a, let b)
        /// </summary>
        /// <param name="tokens">A list of tokens containing only the whole declaration</param>
        /// <param name="context">A list of LineContext corresponding to the tokens supplied</param>
        /// <returns></returns>
        private ASTNode EatDeclaration(List<Token> tokensIn, List<LineContext> contextIn)
        {
            List<Token> tmpTokens;
            List<LineContext> tmpContext;
            tmpTokens = tokensIn;
            tmpContext = contextIn;
            if (tmpTokens[1].type != Global.DataType.IDENTIFIER)
            {
                Swift.error("Identifier expected at line " + tmpContext[0].GetLine().ToString() + ", colomn " + tmpContext[1].GetPos().ToString() + ".", 1);
            }
            if (tmpTokens[0].type == Global.DataType.VAR)
            {
                VarDeclaration node = new VarDeclaration(tmpContext[0]);
                node.Name = new Identifier(tmpTokens[1].value);
                astBase.Children.Add(node);
                if (tmpTokens.Count >= 4)
                {
                    if (tmpTokens[2].type == Global.DataType.OPERATOR && tmpTokens[2].value == "=")
                    {
                        astBase.Children.Add(EatAssignment(tmpTokens.GetRange(1, tmpTokens.Count - 1), tmpContext.GetRange(1, tmpContext.Count - 1)));
                    }
                    else
                    {
                        Swift.error("Assignment expected after a declaration without a type specification on line " + tmpContext[2].GetLine() + ", column " + tmpContext[2].GetPos(), -1);
                    }
                }
                else
                {
                    Swift.error("Assignment expected after a declaration without a type specification on line " + tmpContext[2].GetLine() + ", column " + tmpContext[2].GetPos(), -1);

                }
                return node;
            }
            else if (tmpTokens[0].type == Global.DataType.LET)
            {
                if (tokensIn.Count <= 2)
                    Swift.error("Constants must be initialized; error occurerred on line " + tmpContext[0].GetLine() + ", column " + tmpContext[0].GetPos() + ".", 1);
                if (tmpTokens.Count >= 4)
                {
                    if (tmpTokens[2].type == Global.DataType.OPERATOR && tmpTokens[2].value == "=")
                    {
                        ConstDeclaration node = new ConstDeclaration(tmpContext[0], EatExpression(tmpTokens.GetRange(3, tmpTokens.Count - 3), tmpContext.GetRange(3, tmpContext.Count - 3)));
                        node.Name = new Identifier(tmpTokens[1].value);
                        astBase.Children.Add(node);
                    }
                    else
                    {
                        Swift.error("Constants must be initialized; error occurerred on line " + tmpContext[0].GetLine() + ", column " + tmpContext[0].GetPos() + ".", 1);
                    }
                }
                return node;
            }
            else {
                Swift.error("Internal error parsing the variable declaration: " + tmpTokens, 1);
                return null;
            }
            //if (tokensIn.Count > 2)
            //    node.SetChildren();
            //return EatAssignment(tmpTokens.GetRange(1, tmpTokens.Count - 1), tmpContext.GetRange(1, tmpContext.Count - 1));
        }



        [Serializable()]
        private class UnknownTermException : System.Exception
        {
            public UnknownTermException() : base() { }
            public UnknownTermException(string message) : base(message) { }
            public UnknownTermException(string message, System.Exception inner) : base(message, inner) { }
            protected UnknownTermException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        }
    }
}