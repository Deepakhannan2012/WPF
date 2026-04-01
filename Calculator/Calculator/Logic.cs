using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator;
   internal class Logic {
      public double Evaluator (double input1, double input2, char op) {
         return op switch {
            '+' => input1 + input2,
            '-' => input1 - input2,
            '\u00D7' => input1 * input2,
            '\u00F7' => input1 / input2,
            _ => throw new NotImplementedException ()
         };
      }

      public double Evaluator (double input, EState func) {
         return func switch {
            EState.Inverse => 1 / input,
            EState.Squared => input * input,
            EState.Sqrt => Math.Sqrt (input),
            EState.Negate => -1 * input,
            _ => throw new NotImplementedException ()
         };
      }

      public enum EState { Expression, Func, Inverse, Squared, Sqrt, Negate }
   }
