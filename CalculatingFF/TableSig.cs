using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CalculatingFF
{
   public class TableSig
    {
     
        public double betta { get; set; }
        public double sig2 { get; set; }
        /// <summary>
        /// value table
        /// </summary>
        public double sig { get; set; }
        public double psi { get; set; }
        public double f1 { get; set; }
        public double f2 { get; set; }
    }
}
