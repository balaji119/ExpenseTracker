using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    public class ExpenseDataService : IDataService
    {
        public void AddData(int expenseCost, string type, string description, string date)
        {
            DatabaseHandler.It.AddExpense(expenseCost, type, description, date);
        }

        public void GetData(Action<IEnumerable<Expense>, Exception> callback)
        {
            var expList = DatabaseHandler.It.GetExpenseList();
            callback(expList, null);
        }
    }
}
