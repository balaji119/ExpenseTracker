using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public sealed class DatabaseHandler : IDisposable
    {
        private SQLiteConnection _dbConnection;

        private DatabaseHandler()
        {
            var dbfile = "ExpenceTrcker.sqlite";
            if (!File.Exists(dbfile))
                SQLiteConnection.CreateFile(dbfile);
            _dbConnection = new SQLiteConnection("Data Source=ExpenceTrcker.sqlite;Version=3;");
            _dbConnection.Open();
            CreateTable();
        }

        private void CreateTable()
        {
            var command = new SQLiteCommand(
                "create table if not exists expensetable (cost numeric(20), type varchar(20), description varchar(100), date date(20))",
                _dbConnection
                );
            command.ExecuteNonQuery();
        }

        public void AddExpense(int exp, string type, string description, string date)
        {
            var command = new SQLiteCommand(
                String.Format("insert into expensetable (cost, type, description, date) values ({0}, '{1}', '{2}', '{3}')", exp, type, description, date),
                _dbConnection
                );
            command.ExecuteNonQuery();
        }

        public IEnumerable<Expense> GetExpenseList()
        {
            string sql = "select * from expensetable";
            SQLiteCommand command = new SQLiteCommand(sql, _dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Expense> expenses = new List<Expense>();
            while (reader.Read())
                expenses.Add(new Expense(int.Parse(reader["cost"].ToString()),
                    reader["type"].ToString(), reader["description"].ToString(),
                    reader["date"].ToString()));
            return expenses;
        }

        public void Dispose()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
        }

        /// <summary>
        /// Singleton object
        /// </summary>
        public static DatabaseHandler It
        {
            get
            {
                _It = _It ?? new DatabaseHandler();
                return _It;
            }
        }
        private static DatabaseHandler _It;
    }
}
