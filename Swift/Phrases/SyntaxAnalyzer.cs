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
                    if (tmpTokens[1].type == Global.DataType.OPEN_ROUND_BRACKET)
                    {
                        return EatFunctionCall(tmpTokens, tmpContext);
                    }
                    else {
                        return EatExpression(tmpTokens, tmpContext);
                    }
                case Global.DataType.STRING:
                    node = new ASTNode(Global.ASTType.STRING, tmpContext[0]);
                    node.SetName(tmpTokens[0].value);
                    return node;
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
            node = new ASTNode(Global.ASTType.FUNCTION_CALL, context[0]);
            node.SetName(tmpTokens[0].value);
            node.SetChildren(args);
            return node;
        }

        /// <summary>
        /// Parses an expression
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private ASTNode EatExpression(List<Token> tokensIn = null, List<LineContext> contextIn = null)
        {
            List<Token> tmpTokens;
            List<LineContext> tmpContext;
            return null;
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
