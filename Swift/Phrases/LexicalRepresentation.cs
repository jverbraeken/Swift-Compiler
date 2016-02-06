using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Phrases
{
    /// <summary>
    /// Represents the analysis obtained by the LexicalAnalyzer
    /// </summary>
    class LexicalRepresentation
    {
        List<Token> tokens;
        List<LineContext> context;

        public LexicalRepresentation(List<Token> tokens, List<LineContext> context)
        {
            this.tokens = tokens;
            this.context = context;
        }
    }
}
