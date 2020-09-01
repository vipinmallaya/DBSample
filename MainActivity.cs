using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DBSample.Model;
using Newtonsoft.Json;
using SQLite;

namespace DBSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private SQLiteAsyncConnection _sqliteConnection;
        private string jsonData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            InitializeDatabase();
            _sqliteConnection.CreateTableAsync<Customer>();
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private async void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            System.Diagnostics.Debug.WriteLine($"Data Sart Time {DateTime.Now.ToString("O")}");
            //GenerateData();
            System.Diagnostics.Debug.WriteLine($"Data End Time {DateTime.Now.ToString("O")}");

            System.Diagnostics.Debug.WriteLine($"DB Sart Time {DateTime.Now.ToString("O")}");
            //await _sqliteConnection.DeleteAllAsync<Customer>();
            //var result = await SaveToDatabase();
            System.Diagnostics.Debug.WriteLine($"DB End Time {DateTime.Now.ToString("O")}");
           // System.Diagnostics.Debug.WriteLine($"Total Number Of Rows Impacted - {result}"); 
        }

        private Task<int> SaveToDatabase()
        {
            
            List<Customer> customers = JsonConvert.DeserializeObject< List<Customer>>(jsonData);
          return  _sqliteConnection.InsertAllAsync(customers); 
        }

        private void GenerateData()
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 1; i <= 20000; i++)
            {
                customers.Add(new Customer
                {
                    Id = i,
                    Address1 = $"Address {i}",
                    Address2 = $"Address {i}",
                    Age = (i%80)+1,
                    City = $"City{i}",
                    FirstName = $"First Name - {i}",
                    LastName = $"Last Name - {i}",
                });
            }
            jsonData = JsonConvert.SerializeObject(customers);
        }

        private SQLiteAsyncConnection InitializeDatabase()
        {
            var path = GetDatabasePath();
            _sqliteConnection = _sqliteConnection ??  new SQLiteAsyncConnection(path);
            return _sqliteConnection;
        }

        private string GetDatabasePath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "MyData.db");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
