using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class SyntaxAnalyzer
    {
        public AST CheckSyntax(List<Token> tokens, List<LineContext> context)
        {
            AST astBase = new AST(context[0]);
            while (tokens.Count > 0)
            {
                astBase.AddNode(EatStatement(tokens, context));
                tokens.RemoveAt(0);
                context.RemoveAt(0);
            }
            return astBase;
        }

        /// <summary>
        /// Parses a statement
        /// </summary>
        /// <param name="token"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private ASTNode EatStatement(List<Token> tokens, List<LineContext> context)
        {
            switch (tokens[0].type)
            {
                case Global.DataType.Identifier:
                    if (tokens[1].type == Global.DataType.Open_round_bracket)
                    {
                        EatFunctionCall(tokens, context);
                    }
                    else {
                        EatExpression(tokens, context);
                    }
            }
        }

        private ASTNode EatFunctionCall(List<Token> tokens, List<LineContext> context)
        {
            int i = 0;
            Token token = tokens[0];
            List<Exp> args = new List<Exp>();
            while (token.type != Global.DataType.Close_round_bracket)
            {
                List<Token> arg = new List<Token>();
                while (token.type != Global.DataType.Comma || token.type != Global.DataType.Close_round_bracket)
                {
                    arg.Add(token);
                    token = tokens[++i];
                }
                EatArgument(arg);
            }
            return new ASTFunctionCall(context[0], args);
        }

        /// <summary>
        /// Parses an expression
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private ASTNode EatExpression(List<Token> tokens, List<LineContext> context)
        {

        }
    }
}
