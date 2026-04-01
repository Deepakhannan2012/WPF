using static System.Console;
namespace Wordle {
   public class WordleUI {
      KeyHandler keyHandler = new ();

      public void Run () {
         OutputEncoding = System.Text.Encoding.UTF8;
         CursorVisible = false;
         DisplayBoard ();
         while (!keyHandler.GameOver) {
            ConsoleKeyInfo key = ReadKey (true);
            keyHandler.KeyPressed (key);
            DisplayBoard ();
         }
         PrintResult ();
      }

      void DisplayBoard () {
         CursorTop = 0;
         foreach (var row in keyHandler.WordArray) {
            CursorLeft = 53;
            foreach (var (letter, pos) in row) {
               ForegroundColor = LetterCol (pos);
               Write ($"{letter}  ");
               ResetColor ();
            }
            WriteLine ("\n");
         }
         CursorLeft = 45;
         PrintLine ();
         CursorLeft = 45;
         foreach (var (letter, color) in keyHandler.AllLetters) {
            ForegroundColor = LetterCol (color);
            Write ($"{letter}    ");
            ResetColor ();
            // Displays 7 letters of the alphabet in each row
            if ((letter - 'A' + 1) % 7 == 0 || letter == 'Z') { WriteLine (); CursorLeft = 45; }
         }
         PrintLine ();
         CursorLeft = 50;
         if (!keyHandler.IsValidWord) {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine ($"{keyHandler.Word} is not a word");
            ResetColor ();
            keyHandler.IsValidWord = true;
         }
         // The empty space replaces the already print error message if it exists
         else WriteLine ("                   ");

         // Prints a horizontal line
         void PrintLine () => WriteLine ("───────────────────────────────");
      }

      ConsoleColor LetterCol (WordleLogic.EPos pos)
         => pos switch {
            WordleLogic.EPos.Missing => ConsoleColor.DarkGray,
            WordleLogic.EPos.Positioned => ConsoleColor.Green,
            WordleLogic.EPos.Misplaced => ConsoleColor.Blue,
            _ => ForegroundColor,
         };

      void PrintResult () {
         string result;
         (result, ForegroundColor, CursorLeft) = (keyHandler.Word == keyHandler.SecretWord)
                                               ? ($"You found the word in {keyHandler.Tries} tries", ConsoleColor.Green, 45)
                                               : ($"Sorry - the word was {keyHandler.SecretWord}", ConsoleColor.Yellow, 47);
         WriteLine (result);
         ResetColor ();
         CursorLeft = 49;
         WriteLine ("Press any key to quit");
      }
   }
}
