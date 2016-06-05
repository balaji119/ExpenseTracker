using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Utils
    {
        public static IEnumerable<string> GetExpenseTypes()
        {
            return new List<string> { "Clothing", "Food", "Househod", "Travel" };
        }
    }
}
