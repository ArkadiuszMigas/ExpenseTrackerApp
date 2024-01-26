using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ExpenseTrackerApp
{
    [Table("Expense")]
    public class Expense
    {

        public Expense() { }
        public Expense(string name, float price, DateTime expdate, string category)
        { ExpenseName = name; Price = price; Date = expdate; Category = category; }

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(25), NotNull]
        public string? ExpenseName { get; set; }

        [NotNull]
        public float Price { get; set; }

        [NotNull]
        public DateTime Date { get; set; }

        [MaxLength(25),NotNull]
        public string? Category { get; set; }
    }
}
