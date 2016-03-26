using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class ParameterDeclaration
    {
        public ASTType Type { get; set; }
        public string InternalName { get; set; }
        public string ExternalName { get; set; }
        public Exp DefaultValue { get; set; }
        public bool InOut { get; set; }
        /// <summary>
        /// By default all Swift parameters are constants
        /// </summary>
        public bool NoConstant { get; set; }

        public ParameterDeclaration(ASTType type, string name)
        {
            Type = type;
            InternalName = name;
            ExternalName = null;
            DefaultValue = null;
            InOut = false;
            NoConstant = false;
        }

        public ParameterDeclaration(ASTType type, string name, bool sameInternalExternalName)
        {
            Type = type;
            InternalName = name;
            if (sameInternalExternalName)
                ExternalName = name;
            else
                ExternalName = null;
            DefaultValue = null;
            InOut = false;
            NoConstant = false;
        }

        public ParameterDeclaration(ASTType type, string internalName, string externalName)
        {
            Type = type;
            InternalName = internalName;
            ExternalName = externalName;
            DefaultValue = null;
            InOut = false;
            NoConstant = false;
        }
    }
}
