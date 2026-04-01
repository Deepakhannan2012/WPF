using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator {
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window {
      Parser parser = new ();
      public MainWindow () {
         InitializeComponent ();
      }
      void Button_Click (object sender, RoutedEventArgs e) {
         var btn = (Button)e.OriginalSource;
         string key = btn.Content.ToString () ?? "";
         switch (btn.Tag) {
            case "num":
               NumClick (key);
               break;
            case "op":
               OpClick (key);
               break;
            case "backspace":
               BackspaceClick ();
               break;
            case "clear":
               ClearClick ();
               break;
            case "clearEntry":
               DisplayBox.Text = "0"; break;
            case "equals":
               EqualsClick ();
               break;
            case "negate":
               SmallDisplayBox.Text = $"negate({DisplayBox.Text})";
               DisplayBox.Text = parser.ParseAndEvaluate (DisplayBox.Text, Logic.EState.Negate).ToString ();
               break;
            case "inverse":
               SmallDisplayBox.Text = $"1/({DisplayBox.Text})";
               DisplayBox.Text = parser.ParseAndEvaluate (DisplayBox.Text, Logic.EState.Inverse).ToString ();
               break;
            case "sqr":
               SmallDisplayBox.Text = $"sqr({DisplayBox.Text})";
               DisplayBox.Text = parser.ParseAndEvaluate (DisplayBox.Text, Logic.EState.Squared).ToString ();
               break;
            case "sqrt":
               SmallDisplayBox.Text = $"\u221A({DisplayBox.Text})";
               DisplayBox.Text = parser.ParseAndEvaluate (DisplayBox.Text, Logic.EState.Sqrt).ToString ();
               break;
            case "Not_Implemented":
               throw new NotImplementedException ();
         }
      }

      void Key_Down (object sender, KeyEventArgs e) {
         switch (e.Key) {
            case >= Key.D0 and <= Key.D9:
               NumClick ((e.Key - Key.D0).ToString ());
               break;
            case >= Key.NumPad0 and <= Key.NumPad9:
               NumClick ((e.Key - Key.NumPad0).ToString ());
               break;
            case Key.Decimal or Key.OemPeriod:
               NumClick (".");
               break;
            case Key.Back:
               BackspaceClick ();
               break;
            case Key.Add:
               OpClick ("+");
               break;
            case Key.Subtract:
               OpClick ("-");
               break;
            case Key.Multiply:
               OpClick ("\u00D7");
               break;
            case Key.Divide:
               OpClick ("\u00F7");
               break;
            case Key.Enter:
               EqualsClick ();
               break;
            case Key.Delete:
               ClearClick ();
               break;
         }
      }

      void NumClick (string key) {
         if (DisplayBox.Text == "0" || mOpPressed == 1) {
            DisplayBox.Text = key;
         } else DisplayBox.Text += key;
      }

      void OpClick (string key) {
         if (mOpPressed > 0) {
            EqualsClick ();
            SmallDisplayBox.Text = DisplayBox.Text + key;
         } else {
            SmallDisplayBox.Text += DisplayBox.Text + key;
            mOpPressed = 1;
         }
      }

      void BackspaceClick () {
         if (!(DisplayBox.Text == "0")) {
            DisplayBox.Text = DisplayBox.Text[0..^1];
            if (string.IsNullOrEmpty (DisplayBox.Text)) DisplayBox.Text = "0";
         }
      }

      void EqualsClick () {
         SmallDisplayBox.Text += DisplayBox.Text + "=";
         DisplayBox.Text = parser.ParseAndEvaluate (SmallDisplayBox.Text, Logic.EState.Expression).ToString ();
      }

      void ClearClick () {
         SmallDisplayBox.Text = "";
         DisplayBox.Text = "0";
         mOpPressed = 0;
      }

      int mOpPressed = 0;
   }
}