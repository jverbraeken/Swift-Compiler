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

        public ASTNode CheckSyntax(List<Token> tokens, List<LineContext> context)
        {
            this.tokens = tokens;
            this.context = context;
            ASTNode astBase = new ASTNode(Global.ASTType.BASE, context[0]);
            while (tokens.Count > 0)
            {
                astBase.AddNode(EatStatement());
                CutTokens();
            }
            return astBase;
        }

        /// <summary>
        /// Parses a statement
        /// </summary>
        /// <param name="token"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private ASTNode EatStatement(List<Token> tokensIn = null, List<LineContext> contextIn = null)
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
            switch (tmpTokens[0].type)
            {
                case Global.DataType.IDENTIFIER:
                    if (tmpTokens[1].type == Global.DataType.OPEN_ROUND_BRACKET)
                    {
                        return EatFunctionCall(tmpTokens, tmpContext);
                    }
                    else {
                        return EatExpression(tmpTokens, tmpContext);
                    }
                case Global.DataType.LET:
                    return EatDeclaration(new List<Token>() { tmpTokens[0], tmpTokens[1] }, new List<LineContext>() { tmpContext[0], tmpContext[1] });
                case Global.DataType.STRING:
                    node = new ASTNode(Global.ASTType.STRING, tmpContext[0]);
                    node.SetName(tmpTokens[0].value);
                    return node;
                case Global.DataType.VAR:
                    return EatDeclaration(new List<Token>() { tmpTokens[0], tmpTokens[1] }, new List<LineContext>() { tmpContext[0], tmpContext[1] });
                default:
                    Swift.error("Syntax error: \"" + tmpTokens[0].value + "\" at line " + tmpContext[0].GetLine().ToString() + ", colomn " + tmpContext[0].GetPos().ToString() + " could not be identified", 1);
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
                cutTokens = i + 1;
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
        private ASTNode EatExpression(List<Token> tokensIn = null, List<LineContext> contextIn = null)
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
            return null;
        }

        /// <summary>
        /// Parses a declaration (eg, var a, let b)
        /// </summary>
        /// <param name="tokens">A list of tokens containing only the whole declaration</param>
        /// <param name="context">A list of LineContext corresponding to the tokens supplied</param>
        /// <returns></returns>
        private ASTNode EatDeclaration(List<Token> tokensIn = null, List<LineContext> contextIn = null)
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
            if (tmpTokens[0].type == Global.DataType.VAR)
                node = new ASTNode(Global.ASTType.VAR_DECLARATION, tmpContext[0]);
            else if (tmpTokens[0].type == Global.DataType.LET)
                node = new ASTNode(Global.ASTType.CONST_DECLARATION, tmpContext[0]);
            else
                Swift.error("Internal error parsing the variable declaration: " + tmpTokens, 1);
            node.SetName(tmpTokens[1].value);
            cutTokens += 2;
            return node;
        }

        private void CutTokens()
        {
            for (int i = 0; i < cutTokens; i++)
            {
                tokens.RemoveAt(0);
                context.RemoveAt(0);
            }
            cutTokens = 0;
        }
    }
}
