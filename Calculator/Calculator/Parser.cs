using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator;
   class Parser {
   Logic logic = new ();
   public double ParseAndEvaluate (string input, Logic.EState state) {
      if (state is Logic.EState.Expression) {
         string input1 = "", input2 = "";
         char op = '+';
         foreach (char c in input) {
            switch (c) {
               case (>= '0' and <= '9') or '.' when !mOpFound: input1 += c; break;
               case (>= '0' and <= '9') or '.' when mOpFound: input2 += c; break;
               case '=': break;
               default: op = c; mOpFound = true; break;
            }
         }
         mOpFound = false;
         return logic.Evaluator (double.Parse (input1), double.Parse (input2), op);
      }
      return logic.Evaluator (double.Parse (input), state);
   }
   bool mOpFound = false;
}

