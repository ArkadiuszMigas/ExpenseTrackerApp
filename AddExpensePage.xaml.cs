namespace ExpenseTrackerApp;

using ExpenseTrackerApp.Dane;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

public partial class AddExpensePage : ContentPage
{
    public event EventHandler<Expense>? ExpenseAdded;

    public string[] CategoryList = ["FOOD", "HOME", "TRAVEL", "PARTY"];

    SQLiteAsyncConnection Database;

    public AddExpensePage()
    {
        InitializeComponent();
        CategoryPicker.ItemsSource = CategoryList;
    }

    private async void OnSaveExpenseClicked(object sender, EventArgs e)
    {
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var newExpense = new Expense
        {
            ExpenseName = EntryExpenseName.Text,
            Price = float.Parse(EntryPrice.Text),
            Category = (string)CategoryPicker.SelectedItem,
            Date = DatePicker.Date
        };

        Debug.WriteLine(newExpense.ExpenseName);

        ExpenseAdded?.Invoke(this, newExpense);
        await Database.InsertAsync(newExpense);
        await Navigation.PopModalAsync();
        
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }


}


