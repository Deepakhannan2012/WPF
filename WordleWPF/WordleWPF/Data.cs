using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleWPF {
   public class Data : INotifyPropertyChanged {
      private string _value = "";

      public string Value {
         get => _value;
         set {
            _value = value;
            OnPropertyChanged (nameof (Value));
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged (string name) {
         PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (name));
      }

   }
}
