namespace ListView.Data;
public class Employee {
   public Employee () {
      FstName = LastName = EMail = string.Empty;
   }
   public int EmpID { get; private set; }
   public string FstName { get; set; }
   public string LastName { get; set; }
   public DateOnly DOB { get; set; }
   public string EMail { get; set; }
   public long ContactNum { get; set; }
}

