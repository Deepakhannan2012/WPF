using Nexus.Core;
using Nexus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nexus.App {
   /// <summary>
   /// Interaction logic for AddBookDlg.xaml
   /// </summary>
   public partial class AddBookDlg : Window {
      public AddBookDlg (BookVM vm, Hub<Book> h) {
         InitializeComponent ();
         Title = $"{(h.Get (vm.ID) != null ? "Edit" : "Add")} Book";
         BtnOK.Click += (_, _) => DialogResult = true;
         BtnCancel.Click += (_, _) => { DialogResult = false; Close (); };
         DataContext = vm;
      }
   }
}
