using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    public class Expense
    {
        public Expense(int amount, string type, string description, string date)
        {
            Amount = amount;
            Type = type;
            Description = description;
            Date = date;
        }

        public int Amount { get; private set; }

        public string Type { get; private set; }

        public string Description { get; private set; }

        public string Date { get; private set; }
    }
}
