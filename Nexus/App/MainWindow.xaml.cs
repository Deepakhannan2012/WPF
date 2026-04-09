using Nexus.Core;
using Nexus.Data;
using Nexus.UI;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Nexus.App;

/// <summary>Interaction logic for MainWindow.xaml</summary>
public partial class MainWindow : Window {
   #region Constructor -----------------------------------------------
   public MainWindow () {
      InitializeComponent ();
      IDB<User> udb = DB.Get<User> ();
      IDB<Book> bdb = DB.Get<Book> ();
      mUHub = new (udb);
      mBHub = new (bdb);
      //mBHub = new
      foreach (var u in mUHub.All) Users.Add (new (u, mUHub));   // Create UserVM collection
      foreach (var b in mBHub.All) Books.Add (new (b, mBHub));   // Create BookVM collection
      Init ();
   }
   #endregion

   #region Properties ------------------------------------------------
   // The currently selected user/book in the ListView
   UserVM SelectedUser => UserLst.SelectedItem as UserVM;
   BookVM SelectedBook => BookLst.SelectedItem as BookVM;

   // The UserVM and BookVM collection bound to listview
   ObservableCollection<UserVM> Users { get; set; } = [];
   ObservableCollection<BookVM> Books { get; set; } = [];
   #endregion

   #region Implementation --------------------------------------------
   void Init () {
      UCols = ["ID", "First Name", "Last Name", "Age", "Email", "Phone"];
      BCols = ["ID", "Title", "Author Name", "Price", "Publish Date", "Publisher"];
      InitGrid (UserLst, UCols, Users);
      InitGrid (BookLst, BCols, Books);
      void InitGrid (ListView lv, string[] cols, object source) {
         GridView gv = new ();
         for (int i = 0; i < cols.Length; i++) {
            GridViewColumn gc = new () { Header = cols[i], DisplayMemberBinding = new Binding (cols[i].Replace (" ", "")), Width = 100 };
            gv.Columns.Add (gc);
         }
         lv.View = gv;
         lv.ItemsSource = (System.Collections.IEnumerable)source;
      }

      // Commands
      CommandBindings.Add (new CommandBinding (Commands.Add, (_, _) => DoAddEdit ()));
      CommandBindings.Add (new CommandBinding (Commands.Edit, (_, _) => DoAddEdit (SelectedUser), CanExecute));
      CommandBindings.Add (new CommandBinding (Commands.Delete, (_, _) => DoRemove (), CanExecute));
   }

   void DoAddEdit (object obj = null) {
      bool iNew = obj == null;
      switch (true) {
         case true when UserTab.IsSelected:
            var su = (UserVM)obj;
            var u = iNew ? mUHub.Create () : su.Clone ();
            UserVM wvm = new (u, mUHub);
            AddUserDlg udlg = new (wvm, mUHub) { Owner = this };
            if (udlg.ShowDialog () == true) {
               if (iNew) {
                  wvm.Save ();
                  Users.Add (wvm);
               } else {
                  su.UpdateFrom (u);
                  su.Save ();
                  UserLst.Items.Refresh ();
               }
            }
            break;
         case true when BookTab.IsSelected:
            var sb = (BookVM)obj;
            var b = iNew ? mBHub.Create () : sb.Clone ();
            BookVM bvm = new (b, mBHub);
            AddBookDlg bdlg = new (bvm, mBHub) { Owner = this };
            if (bdlg.ShowDialog () == true) {
               if (iNew) {
                  bvm.Save ();
                  Books.Add (bvm);
               } else {
                  sb.UpdateFrom (b);
                  sb.Save ();
                  BookLst.Items.Refresh ();
               }
            }
            break;
      }
   }

   void DoRemove () {
      switch (true) {
         case true when UserTab.IsSelected:
            if (SelectedUser is null) return;
            if (MessageBox.Show ($"Are you sure you want to delete {SelectedUser.FirstName} {SelectedUser.LastName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
               SelectedUser.Delete ();
               Users.Remove (SelectedUser);
            }
            break;
         case true when BookTab.IsSelected:
            if (SelectedBook is null) return;
            if (MessageBox.Show ($"Are you sure you want to delete {SelectedBook.Title}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
               SelectedBook.Delete ();
               Books.Remove (SelectedBook);
            }
            break;
      }
   }

   void CanExecute (object sender, CanExecuteRoutedEventArgs e) {
      switch (true) {
         case true when UserTab.IsSelected:
            e.CanExecute = SelectedUser != null;
            break;
         case true when BookTab.IsSelected:
            e.CanExecute = SelectedBook != null;
            break;
      }
   }
   #endregion

   #region Private Data ----------------------------------------------
   readonly Hub<User> mUHub;
   readonly Hub<Book> mBHub;
   string[] UCols, BCols;
   ListView lv;
   TabItem tab;
   #endregion
}
