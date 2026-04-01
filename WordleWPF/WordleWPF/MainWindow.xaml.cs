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

namespace WordleWPF {
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window {
      public MainWindow () {
         KeyHandler handler = new KeyHandler ();
         InitializeComponent ();
         GridData = new List<List<Data>> ();

         int rows = 6;
         int cols = 5;

         for (int i = 0; i < rows; i++) {
            var row = new List<Data> ();
            for (int j = 0; j < cols; j++) {
               row.Add (new Data ());
            }
            GridData.Add (row);
         }

         DataContext = this;
      }

      void Button_Click (object sender, RoutedEventArgs e) {
         var btn = (Button)e.OriginalSource;
         string key = btn.Content.ToString () ?? "";
         switch (key) {
            case "ENTER":
               EnterPressed ();
               break;
            case "\u232B":
               Backspace ();
               break;
            default:
               LetterPressed (key);
               break;
         }

      }

      void EnterPressed () { }
      void Backspace () { }
      void LetterPressed (string key) {
         GridData[row][col].Value = key;
         row++;
         col++;
      }


      public List<List<Data>> GridData { get; set; }
      int row = 0;
      int col = 0;
      StringBuilder current = new StringBuilder ();
   }
}