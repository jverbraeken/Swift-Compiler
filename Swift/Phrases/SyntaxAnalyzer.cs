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
        private int cutTokens = 0;
        List<Token> tokens;
        List<LineContext> context;
        private ASTNode node;
        private ASTNode astBase;

        public ASTNode CheckSyntax(List<Token> tokens, List<LineContext> context)
        {
            this.tokens = tokens;
            this.context = context;
            astBase = new ASTNode(Global.ASTType.BASE, context[0]);
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
                astBase.AddNode(EatStatement(tokens.GetRange(0, i), context.GetRange(0, i)));
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
            switch (tokensIn[0].type)
            {
                case Global.DataType.VAR:
                    if (tmpTokens[1].type == Global.DataType.IDENTIFIER)
                    {
                        return EatExpression(tmpTokens, tmpContext);
                    }
                    else
                    {
                        Swift.error("Identifier expected at line " + tmpContext[0].GetLine().ToString() + ", colomn " + tmpContext[1].GetPos().ToString() + ".", 1);
                    }
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
                    node = new ASTNode(Global.ASTType.STRING, contextIn[0]);
                    node.SetName(tokensIn[0].value);
                    return node;
                case Global.DataType.VAR:
                    return EatDeclaration(tokensIn, contextIn);
                default:
                    Swift.error("Syntax error: \"" + tokensIn[0].value + "\" at line " + contextIn[0].GetLine().ToString() + ", colomn " + contextIn[0].GetPos().ToString() + " could not be identified", 1);
                    return null;
            }
        }

        private ASTNode EatFunctionCall(List<Token> tokensIn = null, List<LineContext> contextIn = null)
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

            int i = 2;
            Token token = tmpTokens[i];
            List<ASTNode> args = new List<ASTNode>();
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
                args.Add(EatStatement(arg, argContext));
            }
            node = new ASTNode(Global.ASTType.FUNCTION_CALL, tmpContext[0]);
            node.SetName(tmpTokens[0].value);
            node.SetChildren(args);
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
                            case "+": return new PlusExp(tmpContext[1], new IntegerLiteral(tmpContext[0], tmpTokens[0].value), EatExpression(tmpTokens.GetRange(2, tmpTokens.Count - 2), tmpContext.GetRange(2, tmpContext.Count - 2)));
                        }
                    }
                    else
                        Swift.error("Operator expected at line " + tmpContext[1].GetLine() + ", position " + tmpContext[1].GetPos() + ".", 1);
                }
                else
                    return new IntegerLiteral(tmpContext[0], tmpTokens[0].value);
            }
            return null;
        }

        private ASTNode EatAssignment(List<Token> tokensIn, List<LineContext> contextIn)
        {
            if (!(tokensIn[1].type == Global.DataType.OPERATOR && tokensIn[1].value == "="))
                Swift.error("Assignment operator expected at line " + contextIn[0].GetLine() + ".", 1);
            ASTNode node = new ASTNode(Global.ASTType.ASSIGNMENT, contextIn[0]);
            Exp lhs = new Identifier(contextIn[0], tokensIn[0].value);
            Exp rhs = EatExpression(tokensIn.GetRange(2, tokensIn.Count - 2), contextIn.GetRange(2, tokensIn.Count - 2));
            node.SetExpression1(lhs);
            node.SetExpression2(rhs);
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
            if (tmpTokens[0].type == Global.DataType.VAR)
                node = new ASTNode(Global.ASTType.VAR_DECLARATION, tmpContext[0]);
            else if (tmpTokens[0].type == Global.DataType.LET)
            {
                if (tokensIn.Count <= 2)
                    Swift.error("Constants must be initialized; error occurerred on line " + tmpContext[0].GetLine() + ", column " + tmpContext[0].GetPos() + ".", 1);
                node = new ASTNode(Global.ASTType.CONST_DECLARATION, tmpContext[0]);
            }
            else
                Swift.error("Internal error parsing the variable declaration: " + tmpTokens, 1);
            node.SetName(tmpTokens[1].value);
            astBase.AddNode(node);
            //if (tokensIn.Count > 2)
            //    node.SetChildren();
            return EatAssignment(tmpTokens.GetRange(1, tmpTokens.Count - 1), tmpContext.GetRange(1, tmpContext.Count - 1));
        }
    }
}