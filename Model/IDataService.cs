using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseTracker.Model
{
    public interface IDataService
    {
        void GetData(Action<IEnumerable<Expense>, Exception> callback);

        void AddData(int expenseCost, string type, string description, string date);
    }
}
