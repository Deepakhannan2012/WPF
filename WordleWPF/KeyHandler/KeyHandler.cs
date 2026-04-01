using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHandler
{
   public class KeyHandler {
      WordleLogic wordle = new ();

      public void KeyPressed (ConsoleKeyInfo key) {
         switch (key.Key) {
            case ConsoleKey.Backspace when wordle.Col != 0:
            case ConsoleKey.LeftArrow when wordle.Col != 0:
               wordle.ClearEntry ();
               break;
            case >= ConsoleKey.A and <= ConsoleKey.Z when wordle.Col != 5:
               wordle.UpdateLetter (key.KeyChar);
               break;
            case ConsoleKey.Enter when wordle.Col == 5:
               EnterPressed ();
               break;
         }
      }

      void EnterPressed () {
         Word = new string ([.. wordle.WordArray[wordle.Row].Select (tuple => tuple.Item1)]).ToUpper ();
         IsValidWord = wordle.IsValidWord (Word);
         if (IsValidWord) wordle.ColorWord (Word);
      }

      public Dictionary<char, WordleLogic.EPos> AllLetters => wordle.AllLetters;
      public (char, WordleLogic.EPos)[][] WordArray => wordle.WordArray;
      public string Word { get; private set; } = "";
      public bool IsValidWord { get; set; } = true;
      public bool GameOver => wordle.GameOver;
      public string SecretWord => wordle.SecretWord;
      public int Tries => wordle.Row + 1;
   }
}
