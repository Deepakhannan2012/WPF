using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic {
   public class WordleLogic {
      public WordleLogic () {
         Initialise ();
      }

      public void ClearEntry () {
         var currentCol = WordArray[Row];
         currentCol[--Col].Item1 = mDot;
         currentCol[Col].Item1 = mCircle;
         if (Col != 4) currentCol[Col + 1].Item1 = mDot;
         return;
      }

      public void ColorWord (string word) {
         var currentCol = WordArray[Row];
         for (int i = 0; i < currentCol.Length; i++) {
            var letter = currentCol[i].Item1;
            AllLetters[letter] = true switch {
               _ when !SecretWord.Contains (letter) => EPos.Missing,
               _ when i == SecretWord.IndexOf (letter) => EPos.Positioned,
               _ => EPos.Misplaced,
            };
            currentCol[i].Item2 = AllLetters[letter];
         }
         if (word == SecretWord || Row == 5) { GameOver = true; return; }
         if (Col == 5) { Row++; Col = 0; WordArray[Row][Col].Item1 = mCircle; }
      }

      void Initialise () {
         SecretWord = SelectWord ();
         for (int i = 0; i < WordArray.Length; i++)
            WordArray[i] = new (char, EPos)[5];
         for (int i = 0; i < WordArray.Length; i++)
            for (int j = 0; j < WordArray[i].Length; j++)
               WordArray[i][j] = (mDot, EPos.Default);
         for (int i = 0; i < 26; i++)
            AllLetters[(char)('A' + i)] = EPos.Default;
      }

      public bool IsValidWord (string word) => mAllWords.Contains (word);

      string SelectWord () {
         var words = File.ReadAllLines ("puzzle-5.txt");
         Random random = new ();
         return words[random.Next (words.Length)];
      }

      public void UpdateLetter (char key) {
         var currentCol = WordArray[Row];
         var letter = char.ToUpper (key);
         if (!char.IsLetter (letter)) return;
         currentCol[Col++].Item1 = letter;
         if (Col != 5) currentCol[Col].Item1 = mCircle;
      }

      public enum EPos { Default, Missing, Positioned, Misplaced }

      public string SecretWord { get; private set; } = "";
      public Dictionary<char, EPos> AllLetters { get; private set; } = [];
      public (char, EPos)[][] WordArray { get; private set; } = new (char, EPos)[6][];
      public int Col { get; private set; }
      public int Row { get; private set; }
      public bool GameOver { get; private set; }

      char mDot = '\u00b7', mCircle = '\u25cc';
      string[] mAllWords = File.ReadAllLines ("dict-5.txt");

   }
}
