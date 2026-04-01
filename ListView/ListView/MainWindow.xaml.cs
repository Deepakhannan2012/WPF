using ListView.Data;
using System.Collections.ObjectModel;
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

namespace ListView {
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window {
      public static ObservableCollection<Employee> EmployeeLst = [
      new Employee { FstName = "Tony",
                        LastName ="Stark",
                        DOB = new DateOnly(1970,7,25),
                        EMail = "tony@marvel.com",
                        ContactNum = 9465215475 },
         new Employee { FstName = "Steve",
                        LastName ="Rogers",
                        DOB = new DateOnly(1870,3,12),
                        EMail = "steve@marvel.com",
                        ContactNum = 845123549 },
         new Employee { FstName = "Bruce",
                        LastName ="Banner",
                        DOB = new DateOnly(1978,7,1),
                        EMail = "bruce@marvel.com",
                        ContactNum = 9845712564},
         new Employee { FstName = "Peter",
                        LastName = "Parker",
                        DOB = new DateOnly(2003,12,20),
                        EMail = "peter@marvel.com",
                        ContactNum = 9756325415},
         new Employee { FstName = "Clint",
                        LastName = "Barton",
                        DOB = new DateOnly(1984,2,12),
                        EMail = "clint@marvel.com",
                        ContactNum = 9864514235},
         new Employee { FstName = "Shang",
                        LastName = "Chi",
                        DOB = new DateOnly(1995,7,27),
                        EMail = "shang@marvel.com",
                        ContactNum = 9874563215} ];
      public MainWindow () {
         InitializeComponent ();
         LstView.ItemsSource = EmployeeLst;
      }


   }
}