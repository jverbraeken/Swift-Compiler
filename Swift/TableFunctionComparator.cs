using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class TableFunctionComparator : IEqualityComparer<Tuple<string, List<ASTType>>>
    {
        public bool Equals(Tuple<string, List<ASTType>> x, Tuple<string, List<ASTType>> y)
        {
            if (x.Item2 == null)
            {
                if (y.Item2 == null)
                    return x.Item1 == y.Item1;
                else
                    return false;
            }
            // If we made it to here, it's a function
            if (x.Item2.Count != y.Item2.Count)
                return false;
            for (int i = 0; i < x.Item2.Count; i++)
                if (x.Item2[i].GetType() != y.Item2[i].GetType())
                    return false;
            return x.Item1 == y.Item1;
        }

        public int GetHashCode(Tuple<string, List<ASTType>> obj)
        {
            int res = obj.Item1.GetHashCode();
            if (obj.Item2 != null)
                foreach (ASTType type in obj.Item2)
                    res ^= type.GetType().GetHashCode();
            return res;
        }
    }
}
