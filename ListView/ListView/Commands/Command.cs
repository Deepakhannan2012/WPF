using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListView.Commands;
public class Command : ICommand {
   public event EventHandler? CanExecuteChanged;

   Action<object> _Execute { get; set; }
   Predicate<object> _CanExecute { get; set; }

   public Command (Action<object> ExecuteMethod, Predicate<object> CanExecuteMethod) {
      _Execute = ExecuteMethod;
      _CanExecute = CanExecuteMethod;
   }
   public bool CanExecute (object? parameter) => _CanExecute (parameter);

   public void Execute (object? parameter) => Execute (parameter);
}

