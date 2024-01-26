using System.Diagnostics;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLite;
using ExpenseTrackerApp.Dane;

namespace ExpenseTrackerApp
{
    public partial class MainPage : ContentPage
    {
        SQLiteAsyncConnection Database;

        
        public MainPage()
        {
            InitializeComponent();
            Database = new SQLiteAsyncConnection(Constants.DatabasePath,Constants.Flags);
            Update();

        }

        public async void Update()
        {
            if(Database == null) {
                await Database.CreateTableAsync<Expense>();
            }
            var ExpensesList = await Database.Table<Expense>().ToListAsync();

            expListView.ItemsSource = ExpensesList;

            sumCategories(ExpensesList);
        }

        public async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            Debug.Write("Przycisk działa");
            var addExpensePage = new AddExpensePage();
            var exp = new Expense();


            addExpensePage.ExpenseAdded += (s, expense) =>
            {
                exp = expense;

                Debug.WriteLine("Działaaaaa"+exp.Price);

            };

            
            await Navigation.PushModalAsync(addExpensePage);
            Update();
            
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem is Expense selectedIten) 
            {
                expListView.SelectedItem = null;

                DeleteExpense(selectedIten.id);
            }
        }


        private async void DeleteExpense(int id)
        {
           await Database.DeleteAsync<Expense>(id);
           Update();
        }


        private void sumCategories(List<Expense> expenses)
        {
            float sumFood=0, sumHome=0, sumTravel=0, sumParty = 0;

            foreach (var expense in expenses)
            {
                if(expense.Category=="FOOD")
                    sumFood+=(float)expense.Price;
                else if (expense.Category == "HOME")
                    sumHome += (float)expense.Price;
                else if (expense.Category == "TRAVEL")
                    sumTravel += (float)expense.Price;
                else
                    sumParty+=(float)expense.Price;
            }

            setChart(sumFood, sumHome, sumTravel, sumParty);
            
        }

        private void setChart(float food, float home, float travel, float party)
        {
            BarFood.HeightRequest = food/10;
            BarHome.HeightRequest = home/10;
            BarTravel.HeightRequest = travel/10;
            BarParty.HeightRequest = party/10;

        }

        private void Button_Refresh(object sender, EventArgs e)
        {
            Update();
        }
    }

}
